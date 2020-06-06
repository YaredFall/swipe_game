using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GeneralEnums;

public class ModeWheel : MonoBehaviour
{
    #region Public Variables

    #endregion

    #region Serialized Private Variables
    [SerializeField] private Vector2 _defaultPlatePos = new Vector2(0, 0);
    [SerializeField] private int _spacing = 610;
    [SerializeField] private PlayModePlate _playmodePlatePrefab = null;
    [SerializeField] private GameplayMode[] _playModes = null;
    [Space]
    [SerializeField] private Button _nextButton = null;
    [SerializeField] private Button _prevButton = null;
    #endregion

    #region Private Variables

    private PlayModePlate _currentPlate = null;
    private PlayModePlate _nextPlate = null;
    private PlayModePlate _prevPlate = null;

	private int Current { get; set; }
	private int Next { get => Current + 1 < _playModes.Length ? Current + 1 : 0; }
    private int Prev { get => Current - 1 >= 0 ? Current -1 : _playModes.Length - 1; }
    #endregion

    #region Unity Methods

    private void Awake()
    {
        if (_playmodePlatePrefab == null)
            Debug.LogError("Playmode prefab was not set on Mode Wheel!");

        if (_playModes == null || _playModes.Length == 0)
            Debug.LogError("Play Modes were not set on Mode Wheel!");

        if (_nextButton == null)
            Debug.LogError("Next Button was not set on Mode Wheel!");
        if (_prevButton == null)
            Debug.LogError("Prev Button was not set on Mode Wheel!");
    }

    private void Start()
    {

        if (GameController.Instance.LastPlayedMode != GameplayMode.None)
            Current = LastPlayed(GameController.Instance.LastPlayedMode) ?? 0;

        for (int i = 0; i < _playModes.Length - 1; i++)
        {
            if (_playModes[i] == GameController.Instance.LastPlayedMode)
            {
                Current = i;
                break;
            }
        }

        Init3Plate();
        if (_playModes.Length <= 1)
        {
            _nextButton.interactable = false;
            _prevButton.interactable = false;
        }

    }
    #endregion

    #region Public Methods
    public void ToNext()
    {
        Destroy(_prevPlate.gameObject);
        _currentPlate.Plate.rectTransform.anchoredPosition += Vector2.left * _spacing;
        _prevPlate = _currentPlate;
        _currentPlate = _nextPlate;
        _currentPlate.Plate.rectTransform.anchoredPosition += Vector2.left * _spacing;
        Current = Next;
        InitNext();
    }

    public void ToPrev()
    {
        Destroy(_nextPlate.gameObject);
        _currentPlate.Plate.rectTransform.anchoredPosition += Vector2.right * _spacing;
        _nextPlate = _currentPlate;
        _currentPlate = _prevPlate;
        _currentPlate.Plate.rectTransform.anchoredPosition += Vector2.right * _spacing;
        Current = Prev;
        InitPrev();
    }

    public void ReinitCurrent()
    {
        _currentPlate.Init(_playModes[Current]);
    }
    #endregion

    #region Private Methods
    private void Init3Plate()
    {
        InitCurrent();
        InitNext();
        InitPrev();
    }

    private void InitCurrent()
    {
        if (_playModes[Current] != GameplayMode.None)
        {
            _currentPlate = Instantiate(_playmodePlatePrefab, Vector2.zero, Quaternion.identity, transform);
            _currentPlate.Plate.rectTransform.anchoredPosition = _defaultPlatePos;
            _currentPlate.Init(_playModes[Current]);
        }
        else
        {
            var _playModesList = _playModes.ToList();
            _playModesList.RemoveAt(Current);
            _playModes = _playModesList.ToArray();
        }
    }

    private void InitNext()
    {
        if (_playModes[Next] != GameplayMode.None)
        {
            Vector2 nextPos = _defaultPlatePos + Vector2.right * _spacing;
            _nextPlate = Instantiate(_playmodePlatePrefab, Vector2.zero, Quaternion.identity, transform);
            _nextPlate.Plate.rectTransform.anchoredPosition = nextPos;
            _nextPlate.Init(_playModes[Next]);
        }
        else
        {
            var _playModesList = _playModes.ToList();
            _playModesList.RemoveAt(Next);
            _playModes = _playModesList.ToArray();
        }
    }

    private void InitPrev()
    {
        if (_playModes[Prev] != GameplayMode.None)
        {
            Vector2 prevPos = _defaultPlatePos + Vector2.left * _spacing;
            _prevPlate = Instantiate(_playmodePlatePrefab, Vector2.zero, Quaternion.identity, transform);
            _prevPlate.Plate.rectTransform.anchoredPosition = prevPos;
            _prevPlate.Init(_playModes[Prev]);
        }
        else
        {
            var _playModesList = _playModes.ToList();
            _playModesList.RemoveAt(Prev);
            _playModes = _playModesList.ToArray();
        }
    }

    private int? LastPlayed(GameplayMode mode)
    {
        for (int i = 0; i < _playModes.Length; i++)
        {
            if (mode == _playModes[i])
            {
                return i;
            }
        }
        return null;
    }
    #endregion
}
