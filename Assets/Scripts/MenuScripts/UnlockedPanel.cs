using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UnlockedPanel : MonoBehaviour
{
    #region Public Variables
    public Button PlayButton = null;
    #endregion

    #region Serialized Private Variables
    [SerializeField] private TextMeshProUGUI _highscore = null;
    [SerializeField] private TextMeshProUGUI _totalscore = null;

    #endregion

    #region Private Variables

    #endregion

    #region Unity Methods
    private void Start()
    {
        if (_highscore == null || _totalscore == null)
            Debug.LogError("Highscore or Total score variable was not set on Unlocked Panel!");
        if (PlayButton== null)
            Debug.LogError("Play Button variable was not set on Unlocked Panel!");
    }
    #endregion

    #region Public Methods
    public void SetHighscore(int score)
    {
        _highscore.text = score.ToString();
    }

    public void SetTotalScore(int score)
    {
        _totalscore.text = score.ToString();
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
    #endregion

    #region Private Methods
    
    #endregion
}
