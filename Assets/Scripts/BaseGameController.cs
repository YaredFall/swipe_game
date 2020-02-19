using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BaseGameController : MonoBehaviour
{
    public Canvas Recap;
    public Canvas Interface;

    private ArrowsManager arrowsManager;
    private GameManager gameManager;
    public float WaitTime = 1f;

    private int Score = 0;
    
    private TextMeshProUGUI ScoreText;

    private TextMeshProUGUI[] GUIs;

    private MobileInput MI;

    public Animator ToBaseAnim;

    private TextMeshProUGUI FindTMPByTag(string tag)
    {
        GUIs = FindObjectsOfType<TextMeshProUGUI>();
        foreach (TextMeshProUGUI UI in GUIs)
            if (UI.tag == tag)
                return UI;
        return null;
    }



        private void Awake()
    {
        //SwipeDetector.OnSwipe += SwipeDetector_OnSwipe;
        if (gameManager = null)
            Debug.Log("GameManager was not found");

        ScoreText = FindTMPByTag("Score");
        //Recap.enabled = false;

        MI = MobileInput.Instance;
    }

    private void Start()
    {
        //gameManager = FindObjectOfType<GameManager>();
        arrowsManager = FindObjectOfType<ArrowsManager>();
    }

    private void LateUpdate()
    {
        ScoreText.text = "Score: " + Score.ToString();
        
    }

    private void Update()
    {
        
        if (arrowsManager.arrowActivated && gameManager.InGame() && (MI.SwipeRight || MI.SwipeLeft || MI.SwipeUp || MI.SwipeDown))
            if (arrowsManager.SwipeIsCorrect())
            {
                arrowsManager.RemoveActive();
                Score++;
                arrowsManager.SpawnArrowBase();
            }
            else
            {
                arrowsManager.Death();
                GameOver();
            }
    }

    public void InitGame()
    {
        arrowsManager = FindObjectOfType<ArrowsManager>();
        gameManager = FindObjectOfType<GameManager>();
        GameObject.Find("BaseArrowImage").GetComponent<Image>().sprite = arrowsManager.ArrowSkins[gameManager.pData.currentSkin].Filled;
        if (gameManager.FirstTry)
        {
            GameObject.Find("CrossToBase").SetActive(true);
            ToBaseAnim.Play("BaseOut");
        }
        else
        {
            GameObject.Find("CrossToBase").SetActive(false);
        }
        arrowsManager.SpawnArrowBase();
        Recap.enabled = false;
        Interface.enabled = true;
        Debug.Log("Game (base mode) Init-ed");
    }

    public void GameOver()
    {
        gameManager.ChangeMode(gameManager.DEATH, 0f);
        ShowRecap();
    }
    private void ShowRecap()
    {
        TextMeshProUGUI ScoreRecap = FindTMPByTag("ScoreRecap");
        ScoreRecap.text = "Your score: " + Score.ToString();
        Interface.enabled = false;
        Recap.enabled = true;
        Animator anim = Recap.GetComponent<Animator>();
        anim.Play("RecapFadeIn");
    }
    public void Retry()
    {
        gameManager.FirstTry = false;
        gameManager.ChangeMode(gameManager.BASE, 0);
    }
    public void ToMenu()
    {
        gameManager.ChangeMode(gameManager.MENU, 0);
    }



    //public void OnDisable()
    //{
    //    SwipeDetector.OnSwipe -= SwipeDetector_OnSwipe;
    //}
}
