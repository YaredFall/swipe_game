using System.Collections;
using UnityEngine.SceneManagement;
using System;
using GeneralEnums;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    #region Public Variables
    [HideInInspector] public PlayerData pData = null;
    

    public static GameController Instance;
    #endregion

    #region Serialized Private Variables

    #endregion

    #region Private Variables

    #endregion

    public GameState CurrentGameState { get; set; }
    public GameplayMode CurrentGameplayMode { get; set; }
    public GameplayMode LastPlayedMode { get; private set; }


    #region Unity Methods
    private void Awake()
    {
        MakeSingleton();

        LoadData();

        //
        LastPlayedMode = GameplayMode.Base;
    }

    #endregion

    #region Public Methods
    public void SaveData()
    {
        Debug.Log("Player data was saved");
        SaveSystem.SaveData(pData);
    }
    public void LoadData()
    {
        if (SaveSystem.LoadData() != null)
        {
            pData = SaveSystem.LoadData();
            Debug.Log("Player data was loaded");
        }
        else
        {
            ResetData();
        }
    }
    public void ResetData()
    {
        pData = new PlayerData();
        SaveSystem.SaveData(pData);
        Debug.Log("Player data was reset!");
    }


    public void ChangeGameMode(string mode)
    {
        foreach (GameMode gamemode in Enum.GetValues(typeof(GameMode)))
        {
            if (mode == Enum.GetName(typeof(GameMode), gamemode))
                ChangeGameMode(gamemode, 1f);
        }
    }
    public void ChangeGameMode(GameMode mode)
    {
        SceneManager.LoadScene(Enum.GetName(typeof(GameMode), mode));
    }
    public void ChangeGameMode(GameplayMode mode)
    {
        SceneManager.LoadScene(Enum.GetName(typeof(GameplayMode), mode));
        CurrentGameplayMode = mode;
        LastPlayedMode = mode;
    }
    public void ChangeGameMode(GameMode mode, float time)
    {
        StartCoroutine(ChangeGameModeAfterTime(mode, time));
    }
    public void ChangeGameMode(GameplayMode mode, float time)
    {
        StartCoroutine(ChangeGameModeAfterTime(mode, time));
    }
    private IEnumerator ChangeGameModeAfterTime(GameMode mode, float time)
    {
        yield return new WaitForSeconds(time);
        ChangeGameMode(mode);
    }
    private IEnumerator ChangeGameModeAfterTime(GameplayMode mode, float time)
    {
        yield return new WaitForSeconds(time);
        ChangeGameMode(mode);
        CurrentGameplayMode = mode;
        LastPlayedMode = mode;
    }

    public static TextMeshProUGUI FindTMPByTag(string tag)
    {
        TextMeshProUGUI[] GUIs = FindObjectsOfType<TextMeshProUGUI>();
        foreach (TextMeshProUGUI UI in GUIs)
            if (UI.CompareTag(tag))
                return UI;
        return null;
    }

    #endregion

    #region Private Methods
    private void MakeSingleton()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    #endregion
}
