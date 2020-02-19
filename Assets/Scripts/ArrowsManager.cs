using UnityEngine;

public class ArrowsManager : MonoBehaviour
{
    [System.Serializable]
    public struct ArrowSkin
    {
        public Sprite Blank;
        public Sprite Filled;
    }
    
    public ArrowSkin[] ArrowSkins;

    public GameObject BlankArrowPrefab;
    public GameObject FilledArrowPrefab;

    private ParticleSystem[] pss;
    private ParticleSystem ps;
    private GameObject ActiveArrow;
    private GameManager gm;

    public bool arrowLeft, arrowRight, arrowUp, arrowDown;
    private Vector3 targetPos;

    [HideInInspector]
    public bool arrowActivated;

    public Vector3 defaultArrowPos = new Vector3(0f, -1.5f);
    public float speed = 2f;

    private MobileInput MI;

    private Sprite blankSprite;
    private Sprite filledSprite;

    private Animator anim;

    private string TrueArrow = "TrueArrow";
    private string FalseArrow = "FalseArrow";

    public bool IsMoving;

    private void Awake()
    {
        gm = FindObjectOfType<GameManager>();
        blankSprite = ArrowSkins[gm.pData.currentSkin].Blank;
        filledSprite = ArrowSkins[gm.pData.currentSkin].Filled;
        arrowLeft = arrowRight = arrowUp = arrowDown = false;
        MI = MobileInput.Instance;
    }

    private void Start()
    {
        MI = MobileInput.Instance;
    }

    public void SpawnArrowMenu()
    {
        //if (!gm.InGame())
        //return;
        int arrowtype = Random.Range(1, 11);
        ActiveArrow = Instantiate(FilledArrowPrefab, defaultArrowPos, Quaternion.identity);
        //ActiveArrow.GetComponent<SpriteRenderer>().sprite = filledSprite;
        if (arrowtype <= 5)
        {
            arrowRight = true;
        }
        else
        {
            ActiveArrow.transform.rotation = Quaternion.Euler(0f, 0f, 180f);
            arrowLeft = true;
        }
        ActiveArrow.tag = TrueArrow;
        ActiveArrow.GetComponent<SpriteRenderer>().sprite = filledSprite;
        anim = ActiveArrow.GetComponent<Animator>();
        anim.Play("ArrowFadeIn");
        ActiveArrow.name = "ActiveArrow";
        ActiveArrow.SetActive(true);

        arrowActivated = true;
    }

    public void SpawnArrowTut(int progress = 0)
    {
        if (!gm.InGame())
            return;
        int arrowtype = 1;
        if (progress <= 5)
            arrowtype = Random.Range(1, 6);
        else if (progress >5 && progress <=10)
            arrowtype = Random.Range(6, 11);
        else
            arrowtype = Random.Range(1, 11);

        if (arrowtype <= 5)
        {
            ActiveArrow = Instantiate(FilledArrowPrefab, defaultArrowPos, Quaternion.identity);
            ActiveArrow.tag = TrueArrow;
            ActiveArrow.GetComponent<SpriteRenderer>().sprite = filledSprite;
        }
        else
        {
            ActiveArrow = Instantiate(BlankArrowPrefab, defaultArrowPos, Quaternion.identity);
            ActiveArrow.tag = FalseArrow;
            ActiveArrow.GetComponent<SpriteRenderer>().sprite = blankSprite;

        }

        arrowtype = Random.Range(1, 21);
        if (arrowtype <= 5)
        {
            arrowRight = true;
        }
        else if (5 < arrowtype && arrowtype <= 10)
        {
            ActiveArrow.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
            arrowUp = true;
        }
        else if (10 < arrowtype && arrowtype <= 15)
        {
            ActiveArrow.transform.rotation = Quaternion.Euler(0f, 0f, 180f);
            arrowLeft = true;
        }
        else if (15 < arrowtype && arrowtype <= 20)
        {
            ActiveArrow.transform.rotation = Quaternion.Euler(0f, 0f, 270f);
            arrowDown = true;
        }
        anim = ActiveArrow.GetComponent<Animator>();
        anim.Play("ArrowFadeIn");
        ActiveArrow.name = "ActiveArrow";
        ActiveArrow.SetActive(true);
        arrowActivated = true;
    }

    public void SpawnArrowBase()
    {
        if (!gm.InGame())
            return;
        int arrowtype = Random.Range(1, 11);
        if (arrowtype <= 5)
        {
            ActiveArrow = Instantiate(FilledArrowPrefab, defaultArrowPos, Quaternion.identity);
            ActiveArrow.tag = TrueArrow;
            ActiveArrow.GetComponent<SpriteRenderer>().sprite = filledSprite;
        }
        else
        {
            ActiveArrow = Instantiate(BlankArrowPrefab, defaultArrowPos, Quaternion.identity);
            ActiveArrow.tag = FalseArrow;
            ActiveArrow.GetComponent<SpriteRenderer>().sprite = blankSprite;

        }

        arrowtype = Random.Range(1, 21);
        if (arrowtype <= 5)
        {
            arrowRight = true;
        }
        else if (5 < arrowtype && arrowtype <= 10)
        {
            ActiveArrow.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
            arrowUp = true;
        }
        else if (10 < arrowtype && arrowtype <= 15)
        {
            ActiveArrow.transform.rotation = Quaternion.Euler(0f, 0f, 180f);
            arrowLeft = true;
        }
        else if (15 < arrowtype && arrowtype <= 20)
        {
            ActiveArrow.transform.rotation = Quaternion.Euler(0f, 0f, 270f);
            arrowDown = true;
        }
        anim = ActiveArrow.GetComponent<Animator>();
        anim.Play("ArrowFadeIn");
        ActiveArrow.name = "ActiveArrow";
        ActiveArrow.SetActive(true);
        arrowActivated = true;
    }

    public bool SwipeIsCorrect()
    {
        //if truearrow and the swipe direction and arrow direction are the same
        if (ActiveArrow.CompareTag(TrueArrow) && SameSwipeAndArrowDir())
        {
            return true;
        }
        //or if falsearrow and the swipe direction and arrow direction are opposite
        else if (ActiveArrow.CompareTag(FalseArrow) && OppositeSwipeAndArrowDir())
        {
            return true;
        }
        else
            return false;
    }

    private bool OppositeSwipeAndArrowDir()
    {
        if (arrowLeft && MI.SwipeRight || arrowRight && MI.SwipeLeft || arrowUp && MI.SwipeDown || arrowDown && MI.SwipeUp)
            return true;
        else
            return false;
    }

    private bool SameSwipeAndArrowDir()
    {
        if (arrowLeft && MI.SwipeLeft || arrowRight && MI.SwipeRight || arrowUp && MI.SwipeUp || arrowDown && MI.SwipeDown)
            return true;
        else
            return false;
    }

    private Vector3 TargetPosition(GameObject arrow)
    {
        Vector3 targetPosition = defaultArrowPos;
        if (arrow.name == "TrueFading")
        {
            if (arrow.transform.rotation == Quaternion.Euler(0f,0f,0f))
                targetPosition.x = defaultArrowPos.x + 4f;
            else if (arrow.transform.rotation == Quaternion.Euler(0f, 0f, -180f))
                targetPosition.x = defaultArrowPos.x - 4f;
            else if (arrow.transform.rotation == Quaternion.Euler(0f, 0f, 90f))
                targetPosition.y = defaultArrowPos.y + 4f;
            else if (arrow.transform.rotation == Quaternion.Euler(0f, 0f, -90f))
                targetPosition.y = defaultArrowPos.y - 4f;
        }
        else if (arrow.name == "FalseFading")
        {
            if (arrow.transform.rotation == Quaternion.Euler(0f, 0f, 0f))
                targetPosition.x = defaultArrowPos.x - 4f;
            else if (arrow.transform.rotation == Quaternion.Euler(0f, 0f, -180f))
                targetPosition.x = defaultArrowPos.x + 4f;
            else if (arrow.transform.rotation == Quaternion.Euler(0f, 0f, 90f))
                targetPosition.y = defaultArrowPos.y - 4f;
            else if (arrow.transform.rotation == Quaternion.Euler(0f, 0f, -90f))
                targetPosition.y = defaultArrowPos.y + 4f;
        }
        return targetPosition;
    }

    public void Death()
    {
        anim.Play("ArrowFadeOut");
        arrowLeft = arrowRight = arrowUp = arrowDown = false;
    }

    public void RemoveActive()
    {
        ParticleSystem ps = ActiveArrow.GetComponent<ParticleSystem>();
        if (ActiveArrow.tag == TrueArrow)
        {
            ActiveArrow.name = "TrueFading";
            ps.textureSheetAnimation.SetSprite(0, filledSprite);
            ps.Play();
        } 
        else if (ActiveArrow.tag == FalseArrow)
        {
            ActiveArrow.name = "FalseFading";
            ps.textureSheetAnimation.SetSprite(0, blankSprite);
            ps.Play();
        }
        ActiveArrow.tag = "FadingArrow";
        arrowLeft = arrowRight = arrowUp = arrowDown = false;
        arrowActivated = false;
        IsMoving = true;
        anim.Play("ArrowFadeOut");
    }

    private void Move(GameObject arrow)
    {
        arrow.transform.position = Vector3.Lerp(arrow.transform.position, TargetPosition(arrow), speed * Time.deltaTime);
    }
    private void Update()
    {
        //if (gm.CurrentGameMode != gm.DEATH)
        {
            GameObject[] FadingArrows = GameObject.FindGameObjectsWithTag("FadingArrow");
            if (FadingArrows.Length != 0)
                for (int i = 0; i < FadingArrows.Length; i++)
                {
                    Move(FadingArrows[i]);
                }
        }
        
    }
}
