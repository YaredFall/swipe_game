using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Coins : MonoBehaviour
{
    #region Public Variables

    #endregion

    #region Serialized Private Variables
    [SerializeField] private TextMeshProUGUI _coinsText = null;
    #endregion

    #region Private Variables

    #endregion

    #region Unity Methods
    private void OnEnable()
    {
        GeneralData.OnAddCoins += RefreshCoins;
    }
    private void OnDisable()
    {
        GeneralData.OnAddCoins -= RefreshCoins;
    }
    private void Start()
    {
        _coinsText.text = GameController.Instance.pData.GeneralData.Coins.ToString();
    }
    #endregion

    #region Public Methods

    #endregion

    #region Private Methods
    public void RefreshCoins(int coins)
    {
        _coinsText.text = (int.Parse(_coinsText.text) + coins).ToString();
    }
    #endregion
}
