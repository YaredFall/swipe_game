using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class MainMenuController : MonoBehaviour
{
    public Button BaseButton;
    public Button ColorizedButton;
    public TextMeshProUGUI NotYet;

    public bool NotInGame = true;

    public Animator toBaseAnim;
    public Animator toColorizedAnim;

    private ArrowsManager arrowsManager;
    private GameManager gameManager;
    private MobileInput MI;

    public float WaitTime = 1f;

    private void Awake()
    {
        
        
        if (gameManager = null)
            Debug.Log("GameManager was not found");
        MI = MobileInput.Instance;
        
    }

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void toBase()
    {
        gameManager.FirstTry = true;
        GameObject.Find("CrossToBase").GetComponent<Canvas>().sortingOrder = 5;
        toBaseAnim.Play("BaseIn");
        gameManager.ChangeMode(gameManager.BASE, 1f);
    }

    public void toColorized()
    {
        GameObject.Find("CrossToColorized").GetComponent<Canvas>().sortingOrder = 5;
        toColorizedAnim.Play("ColorizedIn");
        gameManager.ChangeMode(gameManager.COLORIZED, 1f);
    }

    public void toSettings()
    {
        //gameManager.ChangeMode(gameManager.BASE, WaitTime);
    }

    public void toShop()
    {
        //gameManager.ChangeMode(gameManager.BASE, WaitTime);
    }

    public void InitMenu()
    {
        arrowsManager = FindObjectOfType<ArrowsManager>();
        gameManager = FindObjectOfType<GameManager>();
        if (NotInGame)
        {
            NotYet.text = "Not yet in game!";
            ColorizedButton.enabled = false;
        }
        else
        {
            NotYet.gameObject.SetActive(false);
            GameObject.Find("ColorizedArrowImage").GetComponent<Image>().sprite = arrowsManager.ArrowSkins[gameManager.pData.currentSkin].Blank;
            ColorizedButton.enabled = true;
        }
        GameObject.Find("BaseArrowImage").GetComponent<Image>().sprite = arrowsManager.ArrowSkins[gameManager.pData.currentSkin].Filled;
    }

    public void TestLog()
    {
        Debug.Log("Button was pushed");

    }

}
