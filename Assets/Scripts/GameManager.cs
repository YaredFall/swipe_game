using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //private Animator transition;
    public float transitionTime = 1f;

    [HideInInspector]
    public PlayerData pData = null;

    public static GameManager instance;

    [HideInInspector]
    public string CurrentGameMode;

    private MainMenuController MenuController;
    private BaseGameController BGController;

    public string MENU = "MainMenu";
    public string BASE = "BaseGame";
    public string BTUT = "BaseTutorial";
    public string DEATH = "Death";
    public string COLORIZED = "ColorizedGame";

    public bool FirstTry = true;
    private void Awake()
    {
        MakeSingleton();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void MakeSingleton()
    {
        if (instance != null)
            Destroy(gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == MENU)
            SetMenu();
        if (scene.name == BASE)
        {
            SetBaseGame();
        }
        if (scene.name == BTUT)
            SetBTut();
        Debug.Log(scene.name + " was loaded!");
        
    }

    private void SetMenu()
    {
        MenuController = FindObjectOfType<MainMenuController>();
        MenuController.InitMenu();
    }
    private void SetBaseGame()
    {
        BGController = FindObjectOfType<BaseGameController>();
        BGController.InitGame();
    }
    private void SetBTut()
    {
        BaseTutorial BTut = FindObjectOfType<BaseTutorial>();
        BTut.InitBTut();
    }

    public void ChangeMode(string mode, float time)
    {
        StartCoroutine(ChangeModeAfterTime(mode, time));
    }
    private IEnumerator ChangeModeAfterTime(string mode, float time)
    {
        yield return new WaitForSeconds(time);
        CurrentGameMode = mode;
        Debug.Log("GameMode was set on " + CurrentGameMode);
        if (mode == MENU)
        {
            SceneManager.LoadScene(mode);
        }
        else if (mode == BASE)
        {
            SceneManager.LoadScene(mode);
        }
        else if (mode == BTUT)
        {
            SceneManager.LoadScene(mode);
        }
        else if (mode == DEATH)
        {
            Debug.Log("Recap is supposed to be shown");
        }
        else if (mode == COLORIZED)
        {
            SceneManager.LoadScene(mode);
        }

    }



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
    }
    public void ResetData()
    {
        pData = new PlayerData();
        SaveSystem.SaveData(pData);
        Debug.Log("Player data was reset!");
    }



    public bool InGame()
    {
        string cgMode = CurrentGameMode;
        if (cgMode == BASE || cgMode == COLORIZED || cgMode == BTUT)
            return true;
        else
            return false;
    }





}
