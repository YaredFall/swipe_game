using UnityEngine.UI;
using System;
using UnityEngine;
using GeneralEnums;
using TMPro;

public class PlayModePlate : MonoBehaviour
{
    #region Public Variables
    public Image Plate; 
    #endregion

    #region Serialized Private Variables
    [SerializeField] private GameplayMode _gameplayMode = GameplayMode.None;
    [Space]
    [SerializeField] private TextMeshProUGUI _modeName = null;
    [SerializeField] private UnlockedPanel _unlockedPanel = null;
    [SerializeField] public LockedPanel _lockedPanel = null;
    #endregion

    #region Private Variables
    private GameplayModeData _modeData = null;

    #endregion

    #region Unity Methods
    private void Start()
    {
        if (_unlockedPanel == null)
            Debug.LogError($"Unlocked Panel variable was not set on PlayModePlate {this.gameObject.name}");
        if (_lockedPanel == null)
            Debug.LogError($"Locked Panel variable was not set on PlayModePlate {this.gameObject.name}");
        if (_gameplayMode == GameplayMode.None)
            Debug.LogError($"Gameplay mode variable was not set on PlayModePlate {this.gameObject.name}");
        if (_modeName == null)
            Debug.LogError($"Mode Name variable was not set on PlayModePlate {this.gameObject.name}");


    }

    #endregion

    #region Public Methods
    public void Init(GameplayMode mode)
    {
        _gameplayMode = mode;

        _modeData = GameController.Instance.pData.GetGamePlayModeData(_gameplayMode);

        if (_modeData == null)
        {
            Debug.LogError("Mode Data was not found");
        }

        _modeName.text = Enum.GetName(typeof(GameplayMode), _gameplayMode);

        if (IsUnlocked())
        {
            _unlockedPanel.Enable();
            _lockedPanel.Disable();

            _unlockedPanel.SetHighscore(_modeData.Highscore);
            _unlockedPanel.SetTotalScore(_modeData.TotalScore);

            _unlockedPanel.PlayButton.onClick.AddListener(() => GameController.Instance.ChangeGameMode(_gameplayMode));
        }
        else
        {
            _unlockedPanel.Disable();
            _lockedPanel.Enable();

            if (PlaymodesCost.GetCost(_gameplayMode) != -1)
                _lockedPanel.SetCost(PlaymodesCost.GetCost(_gameplayMode));
            else
                Debug.LogError("Gameplay Mod Cost was not found!");

            _lockedPanel.UnlockButton.onClick.AddListener(() => TryToUnlock());
        }
    }
    #endregion

    #region Private Methods
    private bool IsUnlocked()
    {
        return GameController.Instance.pData.GetGamePlayModeData(_gameplayMode).Unlocked;
    }

    private void TryToUnlock()
    {
        if (GameController.Instance.pData.GeneralData.Coins >= PlaymodesCost.GetCost(_gameplayMode))
        {
            Debug.Log(GameController.Instance.pData.GeneralData.Coins + ">=" + PlaymodesCost.GetCost(_gameplayMode));
            GameController.Instance.pData.GeneralData.SpendCoins(PlaymodesCost.GetCost(_gameplayMode));
            FindObjectOfType<Coins>().RefreshCoins(-PlaymodesCost.GetCost(_gameplayMode));

            GameController.Instance.pData.GetGamePlayModeData(_gameplayMode).Unlock();
            FindObjectOfType<ModeWheel>().ReinitCurrent();
        }
        else
        {
            _lockedPanel.PrintNotEnought();
        }
    }
    #endregion
}
