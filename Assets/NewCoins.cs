using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(Animation))]
public class NewCoins : MonoBehaviour
{
    #region Public Variables

    #endregion

    #region Serialized Private Variables
    [SerializeField] private TextMeshProUGUI _newCoinsText = null;
    #endregion

    #region Private Variables

    #endregion

    #region Unity Methods
    private void OnEnable()
    {
        GeneralData.OnAddCoins += StartAnim;
    }
    private void OnDisable()
    {
        GeneralData.OnAddCoins -= StartAnim;
    }
    #endregion

    #region Public Methods

    #endregion

    #region Private Methods
    private void StartAnim(int coins)
    {
        _newCoinsText.text = $"+{coins}";
        GetComponent<Animation>().Play();
    }
    #endregion
}
