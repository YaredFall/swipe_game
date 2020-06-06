using System;
using TMPro;
using UnityEngine;
using GeneralEnums;
using Random = UnityEngine.Random;

public class BaseModeController : GameplayController
{
    #region Public Variables

    #endregion

    #region Serialized Private Variables

    #endregion

    #region Private Variables
    private BaseishArrow _activeArrow = null;
    #endregion

    #region Unity Methods

    #endregion

    #region Public Methods

    #endregion

    #region Private Methods
    protected override bool IsSwipeCorrect(Direction swipeDir)
    {
        return _activeArrow.Type == BaseishArrow.ArrowType.Filled ? Directions.Same(_activeArrow.Direction, swipeDir)
            : Directions.Opposite(_activeArrow.Direction, swipeDir);
    }

    protected override void SpawnArrow()
    {
        if (_arrowPrefab != null)
        {
            _activeArrow = Instantiate((BaseishArrow)_arrowPrefab, _baseArrowPos, Quaternion.identity);
            _activeArrow.Init(ArrowMode.Base);
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
                _activeArrow.MoveAway();
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
