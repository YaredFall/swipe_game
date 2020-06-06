using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossOut : MonoBehaviour
{
    #region Public Variables

    #endregion

    #region Serialized Private Variables
    [SerializeField] private bool IsCrossOut = false;
    [SerializeField] private string _animName = "";
    #endregion

    #region Private Variables

    #endregion

    #region Unity Methods
    private void OnEnable()
    {
        if (IsCrossOut)
            GetComponent<Animator>().Play(_animName);
    }
    #endregion

    #region Public Methods

    #endregion

    #region Private Methods

    #endregion
}
