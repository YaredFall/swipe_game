using TMPro;
using GeneralEnums;
using System;
using UnityEngine;

public abstract class GameplayController : MonoBehaviour
{
    #region Public Variables

    #endregion

    #region Serialized Private Variables
    [SerializeField] protected Canvas _interface = null;
    [SerializeField] protected Canvas _recap = null;
    [Space]
    [SerializeField] protected Arrow _arrowPrefab = null;
    [SerializeField] protected Vector3 _baseArrowPos = new Vector3(0f, -1.5f);
    #endregion

    #region Private Variables
    protected TextMeshProUGUI _scoreText;
    protected int _score = 0;

    protected int _coinCooldown = 5;
    #endregion

    #region Unity Methods
    protected void Awake()
    {
        _scoreText = GameController.FindTMPByTag("Score");

        if (_interface == null)
            Debug.LogError("Interface Canvas is not set!");
        if (_recap == null)
            Debug.LogError("Recap Canvas is not set!");
        _recap.enabled = false;
        _interface.enabled = true;

        RefreshScore();
    }

    protected void OnEnable()
    {
        InputHandler.OnSwipe += AnswerSwipe;
    }

    protected void OnDisable()
    {
        InputHandler.OnSwipe -= AnswerSwipe;
    }

    protected void Start()
    {
        GameController.Instance.CurrentGameState = GameState.Gameplay;
        SpawnArrow();
    }
    #endregion

    #region Public Methods
    public void Restart()
    {
        ToggleRecap();
        ToggleInterface();
        _score = 0;
        RefreshScore();

        SpawnArrow();
        GameController.Instance.CurrentGameState = GameState.Gameplay;
    }

    public void ToMainMenu()
    {
        GameController.Instance.ChangeGameMode(GameMode.MainMenu);
    }

    #endregion

    #region Private Methods
    protected abstract bool IsSwipeCorrect(Direction swipeDir);

    protected abstract void SpawnArrow();

    protected abstract void AnswerSwipe(Direction swipeDir);

    protected abstract void MoneySistem();

    protected void ShowDeathsreen()
    {
        GameController.Instance.CurrentGameState = GameState.Deathscreen;

        GameController.Instance.pData.GetGamePlayModeData(GameController.Instance.CurrentGameplayMode).TotalScore += _score;

        if (_score > GameController.Instance.pData.GetGamePlayModeData(GameController.Instance.CurrentGameplayMode).Highscore)
            GameController.Instance.pData.BaseModeData.Highscore = _score;
        RefreshHighscore();

        GameController.Instance.SaveData();

        _recap.GetComponent<Animator>().Play("RecapFadeIn");

        ToggleInterface();
        ToggleRecap();
    }

    protected void ToggleInterface()
    {
        _interface.enabled = !_interface.enabled;
    }
    protected void ToggleRecap()
    {
        _recap.enabled = !_recap.enabled;
    }

    protected void IncreaseScore()
    {
        _score += 1;
        RefreshScore();
    }

    protected void RefreshScore()
    {
        _scoreText.text = $"Score: {_score}";
    }

    protected void RefreshHighscore()
    {
        GameController.FindTMPByTag("ScoreRecap").text = $"Your score: {_score}";
        GameController.FindTMPByTag("HighscoreRecap").text = $"Highscore: {GameController.Instance.pData.GetGamePlayModeData(GameplayMode.Base).Highscore}";
    }
    #endregion
}
