using UnityEngine;
using System.Collections;
using TMPro;

public class BaseTutorial : MonoBehaviour
{
    private ArrowsManager arrowsManager;
    private GameManager gameManager;
    private MobileInput MI;

    public float WaitTime = 1f;

    private int Progress = 0;

    private TextMeshProUGUI Hint;
    private TextMeshProUGUI Message;

    private TextMeshProUGUI[] GUIs;

    private Animator hintAnim;
    private Animator messageAnim;

    private float tapTime = 0f;
    public float timer = 3.5f;

    private bool Hintenabled = true;
    private bool Messageenabled = true;

    private void Awake()
    {
        arrowsManager = FindObjectOfType<ArrowsManager>();
        gameManager = FindObjectOfType<GameManager>();
        if (gameManager = null)
            Debug.Log("GameManager was not found");
        MI = MobileInput.Instance;
        Hint = FindTMPByTag("Hint");
        Message = FindTMPByTag("Message");
    }

    private TextMeshProUGUI FindTMPByTag(string tag)
    {
        GUIs = FindObjectsOfType<TextMeshProUGUI>();
        foreach (TextMeshProUGUI UI in GUIs)
            if (UI.tag == tag)
                return UI;
        return null;
    }

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        hintAnim = Hint.GetComponent<Animator>();
        messageAnim = Message.GetComponent<Animator>();
        HideMessage();
        hintAnim.Play("TintBlinking");
    }

    private void Update()
    {

        if (arrowsManager.arrowActivated)
            if (arrowsManager.SwipeIsCorrect() && (MI.SwipeRight || MI.SwipeLeft || MI.SwipeUp || MI.SwipeDown))
            {
                arrowsManager.RemoveActive();
                Progress++;
                tapTime = Time.time;
                ShowMessage();
                if (Hintenabled)
                {
                    HideHint();
                }
                if (Progress < 20)
                    arrowsManager.SpawnArrowTut(Progress);
                else
                {
                    gameManager.pData.passedBaseTutorial = true;
                    gameManager.ChangeMode(gameManager.MENU, 3f);
                } 
            }
            else if (!arrowsManager.SwipeIsCorrect() && (MI.SwipeRight || MI.SwipeLeft || MI.SwipeUp || MI.SwipeDown))
            {
                ShowHint();
            }
    }

    private void LateUpdate()
    {
        if (Time.time - tapTime > timer)
        {
            ShowHint();
        }
        if (Time.time - mesTime > timer)
        {
            HideMessage();
        }
    }

    private float mesTime = 0f;
    private void ShowMessage()
    {
        if (!Messageenabled)
        {
            if (Progress == 5)
            {
                Message.text = "Nice!";
                Messageenabled = true;
                mesTime = Time.time;
                messageAnim.Play("TintAnim");
            }
            if (Progress == 10)
            {
                Message.text = "Good!";
                Messageenabled = true;
                mesTime = Time.time;
                messageAnim.Play("TintAnim");
            }
            if (Progress == 15)
            {
                Message.text = "Awesome!";
                Messageenabled = true;
                mesTime = Time.time;
                messageAnim.Play("TintAnim");
            }
            if (Progress == 20)
            {
                Message.text = "Now you ready for real game!";
                Messageenabled = true;
                mesTime = Time.time;
                messageAnim.Play("TintAnim");
            }
        }
    
    }
    private void HideMessage()
    {
        if (Messageenabled)
        {
            Messageenabled = false;
            messageAnim.Play("TintFastFade");
        }
    }

    private void ShowHint()
    {
        if (!Hintenabled)
        {
            if (Progress <= 5)
                Hint.text = "Swipe in the direction of the arrow";
            else if (Progress > 5 && Progress <= 10)
                Hint.text = "Now swipe in the opposite direction of the arrow";
            else if (Progress > 10 && Progress <= 19)
                Hint.text = "Now do it yourself!";
            else
                return;
            Hintenabled = true;
            hintAnim.Play("TintAnim");
            hintAnim.SetTrigger("Blink");
            Debug.Log("Hint shown");
        }
    }
    private void HideHint()
    {
        if (Hintenabled)
        {
            Hintenabled = false;
            hintAnim.Play("TintFastFade");
            Debug.Log("Hint hid");
        }
    }

    public void InitBTut()
    {
        arrowsManager.SpawnArrowTut(Progress);
    }

}

