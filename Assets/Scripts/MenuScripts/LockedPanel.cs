using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using GeneralEnums;

public class LockedPanel : MonoBehaviour
{
    #region Public Variables
    public Button UnlockButton = null;
    #endregion

    #region Serialized Private Variables
    [SerializeField] private TextMeshProUGUI _unlockButtonText = null;

    #endregion

    #region Private Variables

    #endregion

    #region Unity Methods
    private void Start()
    {
        if (_unlockButtonText == null)
            Debug.LogError("Unlock Button Text variable was not set on Locked Panel!");
        if (UnlockButton == null)
            Debug.LogError("Unlock Button variable was not set on Locked Panel!");
    }
    #endregion

    #region Public Methods
    public void SetCost(int cost)
    {
        _unlockButtonText.text = $"UNLOCK FOR {cost} ARROW COINS";
    }

    public void Disable()
    {
        if (this.gameObject.activeSelf)
            this.gameObject.SetActive(false);
    }

    public void Enable()
    {
        if (!this.gameObject.activeSelf)
            this.gameObject.SetActive(true);
    }

    public void PrintNotEnought()
    {
        Debug.Log("Not enought coins");
    }
    #endregion

    #region Private Methods

    #endregion
}
