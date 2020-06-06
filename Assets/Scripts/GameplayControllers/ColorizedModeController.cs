using GeneralEnums;
using System;
using UnityEngine;
using TMPro;
using Random = UnityEngine.Random;

public class ColorizedModeController : GameplayController
{
    #region Public Variables

    #endregion

    #region Serialized Private Variables
    [SerializeField] private TextMeshProUGUI _colorText = null;
    #endregion

    #region Private Variables
    private ColorizedArrow _activeArrow = null;
    #endregion

    #region Unity Methods

    #endregion

    #region Public Methods

    #endregion

    #region Private Methods
    protected override bool IsSwipeCorrect(Direction swipeDir)
    {
        return (Directions.Same(_activeArrow.Direction, swipeDir) && Enum.GetName(typeof(ColorName), _activeArrow.ColorName) == _colorText.text)
            || (Directions.Opposite(_activeArrow.Direction, swipeDir) && Enum.GetName(typeof(ColorName), _activeArrow.ColorName) != _colorText.text);
    }

    protected override void SpawnArrow()
    {
        if (_arrowPrefab != null)
        {
            _activeArrow = Instantiate((ColorizedArrow)_arrowPrefab, _baseArrowPos, Quaternion.identity);
            _activeArrow.Init();
            Invoke("SetColorText", 0.01f);
        }
        else
        {
            Debug.LogError("Arrow Prefab is not set!");
        }
    }

    protected override void AnswerSwipe(Direction swipeDir)
    {
        if (GameController.Instance.CurrentGameState == GameState.Gameplay)
        {
            if (IsSwipeCorrect(swipeDir))
            {
                IncreaseScore();
                if (Enum.GetName(typeof(ColorName), _activeArrow.ColorName) == _colorText.text)
                    _activeArrow.MoveAway(true);
                else if (Enum.GetName(typeof(ColorName), _activeArrow.ColorName) != _colorText.text)
                    _activeArrow.MoveAway(false);
                MoneySistem();

                SpawnArrow();
            }
            else
            {
                _activeArrow.Kaboom();
                ShowDeathsreen();
            }
        }
    }

    private void SetColorText()
    {
        _colorText.color = Colors.GetColor(_activeArrow.ColorName);
        if (Random.Range(0, 2) == 0)
            _colorText.text = Enum.GetName(typeof(ColorName), _activeArrow.ColorName);
        else
            _colorText.text = Enum.GetName(typeof(ColorName), (ColorName)Random.Range(0, Enum.GetNames(typeof(ColorName)).Length));
    }

    protected override void MoneySistem()
    {
        if (_score != 0 && _score % _coinCooldown == 0)
        {
            GameController.Instance.pData.GeneralData.AddCoin();
            GameController.Instance.SaveData();
        }
    }
    #endregion
}
