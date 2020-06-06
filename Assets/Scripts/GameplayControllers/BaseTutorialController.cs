using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using GeneralEnums;

public class BaseTutorialController : MonoBehaviour
{
    #region Public Variables

    #endregion

    #region Serialized Private Variables
    [SerializeField] private BaseishArrow _arrowPrefab = null;
    [SerializeField] private Vector2 _baseArrowPos = new Vector2(0f, 1.5f);
    [Space]
    [SerializeField] private TextMeshProUGUI _hint = null;
    [SerializeField] private TextMeshProUGUI _message = null;
    [Space]
    [SerializeField] private float _hintDelay = 1.5f;
    [SerializeField] private float _messageDelay = 3f;
    #endregion

    #region Private Variables
    private float _tapTime = 0f;

    private int _progress = 0;

    private bool _hintEnabled = true;
    private bool _messageEnabled = true;

    private Animator _hintAnim = null;
    private Animator _messageAnim = null;

    private float _messageTime = 0f;

    private BaseishArrow _activeArrow = null;
    #endregion

    #region Unity Methods
    private void OnEnable()
    {
        InputHandler.OnSwipe += AnswerSwipe;
    }

    private void OnDisable()
    {
        InputHandler.OnSwipe -= AnswerSwipe;
    }

    private void Awake()
    {
        _hintAnim = _hint.GetComponent<Animator>();
        _messageAnim = _message.GetComponent<Animator>();
    }

    private void Start()
    {
        GameController.Instance.CurrentGameState = GameState.Gameplay;
        SpawnArrow();
    }
    private void LateUpdate()
    {
        if (Time.time - _tapTime > _hintDelay)
        {
            ShowHint();
        }
        if (Time.time - _messageTime > _messageDelay)
        {
            HideMessage();
        }
    }
    #endregion

    #region Public Methods

    #endregion

    #region Private Methods
    private void SpawnArrow()
    {
        if (_arrowPrefab != null)
        {
            _activeArrow = Instantiate(_arrowPrefab, _baseArrowPos, Quaternion.identity);
            SetArrowMode();
        }
        else
        {
            Debug.LogError("Arrow Prefab is not set!");
        }
    }

    private void SetArrowMode()
    {
        if (_progress < 5)
            _activeArrow.Init(ArrowMode.FilledOnly);
        else if (_progress >= 5 && _progress < 10)
            _activeArrow.Init(ArrowMode.BlankOnly);
        else if (_progress >= 10 && _progress < 26)
            _activeArrow.Init(ArrowMode.Base);
        else
            return;
    }

    private void AnswerSwipe(Direction swipeDir)
    {
        if (GameController.Instance.CurrentGameState == GameState.Gameplay)
        {
            if (IsSwipeCorrect(swipeDir))
            {
                _progress++;

                _tapTime = Time.time;

                if (_progress > 0 && _progress % 5 == 0)
                    ShowMessage();

                HideHint();

                _activeArrow.MoveAway();
                if (_progress < 20)
                {
                    SpawnArrow();
                    Debug.Log(_activeArrow.Type);
                }
                else
                {
                    GameController.Instance.pData.GeneralData.PassedTutorial = true;
                    GameController.Instance.SaveData();
                    GameController.Instance.ChangeGameMode(GameMode.MainMenu, _messageDelay);
                }

            }
            else
            {
                ShowHint();
            }
        }
    }

    private bool IsSwipeCorrect(Direction swipeDir)
    {
        return _activeArrow.Type == BaseishArrow.ArrowType.Filled ? Directions.Same(_activeArrow.Direction, swipeDir)
            : Directions.Opposite(_activeArrow.Direction, swipeDir);
    }

    private void ShowHint()
    {
        if (!_hintEnabled)
        {
            SetHint();
            _hintEnabled = true;
            _hintAnim.Play("TintAnim");
            _hintAnim.SetTrigger("Blink");
        }
    }

    private void SetHint()
    {
        if (_progress <= 5)
            _hint.text = "Swipe in the direction of the arrow";
        else if (_progress > 5 && _progress <= 10)
            _hint.text = "Now swipe in the opposite direction of the arrow";
        else if (_progress > 10 && _progress < 25)
            _hint.text = "Now do it yourself!";
        else
            return;
    }

    private void HideHint()
    {
        if (_hintEnabled)
        {
            _hintEnabled = false;
            _hintAnim.Play("TintFastFade");
        }
    }

    private void ShowMessage()
    {
        if (!_messageEnabled)
        {
            SetMessage();
            _messageEnabled = true;
            _messageTime = Time.time;
            _messageAnim.Play("TintAnim");
        }

    }

    private void SetMessage()
    {
        switch (_progress)
        {
            case 5:
                _message.text = "Not bad!";
                break;
            case 10:
                _message.text = "Good!";
                break;
            case 15:
                _message.text = "Awesome!";
                break;
            case 25:
                _message.text = "Now you ready for real game!";
                break;
            default:
                break;
        }
    }

    private void HideMessage()
    {
        if (_messageEnabled)
        {
            _messageEnabled = false;
            _messageAnim.Play("TintFastFade");
        }
    }
    #endregion
}
