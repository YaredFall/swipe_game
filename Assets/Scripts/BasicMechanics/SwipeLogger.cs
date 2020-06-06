using System.Collections;
using GeneralEnums;
using UnityEngine;

public class SwipeLogger : MonoBehaviour
{
    #region Public Variables

    #endregion

    #region Serialized Private Variables

    #endregion

    #region Private Variables

    #endregion

    #region Unity Methods
    private void Awake()
    {
        InputHandler.OnSwipe += LogSwipe;
    }
    #endregion

    #region Public Methods

    #endregion

    #region Private Methods
    private void LogSwipe(Direction dir)
    {
        Debug.Log("Detected swipe in direction: " + dir);
    }
    #endregion
}
