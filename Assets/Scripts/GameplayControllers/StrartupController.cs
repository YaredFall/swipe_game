using GeneralEnums;
using UnityEngine;

public class StrartupController : MonoBehaviour
{
    #region Public Variables
    public bool DebugTutPassedOverride;
    #endregion

    #region Serialized Private Variables

    #endregion

    #region Private Variables

    #endregion

    #region Unity Methods
    private void Start()
    {
        if (DebugTutPassedOverride)
            GameController.Instance.pData.GeneralData.PassedTutorial = true;
        TryLoadBaseTut();
    }
    #endregion

    #region Public Methods

    #endregion

    #region Private Methods
    private void TryLoadBaseTut()
    {
        if (!GameController.Instance.pData.GeneralData.PassedTutorial)
            GameController.Instance.ChangeGameMode(GameplayMode.BaseTutorial);
        else
            GameController.Instance.ChangeGameMode(GameMode.MainMenu);
    }
    #endregion
}
