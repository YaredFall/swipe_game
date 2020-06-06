using System.Collections;
using GeneralEnums;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    #region Public Variables

    #endregion

    #region Serialized Private Variables

    [SerializeField] private float _transitionTime = 1f;
    #endregion

    #region Private Variables
    #endregion

    #region Unity Methods

    #endregion

    #region Public Methods

    #endregion
    private void ToBase()
    {
        GameController.Instance.ChangeGameMode(GameplayMode.Base, _transitionTime);
    }
    #region Private Methods

    #endregion
}
