using System;
using GeneralEnums;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    #region Public Variables

    #endregion

    #region Serialized Private Variables
    [SerializeField] private float _deadzone = 100f;
    #endregion

    #region Private Variables
    private Vector2 _startTouchPos, _swipeDelta;
    private Direction _swipeDir;
    #endregion

    #region Unity Methods
    void Update()
    {
        #region Standalone Input
        if (Input.GetMouseButtonDown(0))
            _startTouchPos = Input.mousePosition;
        else if (Input.GetMouseButtonUp(0))
            _startTouchPos = _swipeDelta = Vector2.zero;
        #endregion

        #region Mobile Input
        if (Input.touches.Length != 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
                _startTouchPos = Input.mousePosition;
            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
                _startTouchPos = _swipeDelta = Vector2.zero;
        }
        #endregion

        _swipeDelta = Vector2.zero;
        if (_startTouchPos != Vector2.zero)
        {
            if (Input.touches.Length != 0)
                _swipeDelta = Input.touches[0].position - _startTouchPos;
            else if (Input.GetMouseButton(0))
                _swipeDelta = (Vector2)Input.mousePosition - _startTouchPos;
        }

        if (_swipeDelta.magnitude > _deadzone)
        {
            float x = _swipeDelta.x;
            float y = _swipeDelta.y;

            if (Math.Abs(x) > Math.Abs(y))
            {
                if (x < 0)
                    _swipeDir = Direction.Left;
                else
                    _swipeDir = Direction.Right;
            }
            else
            {
                if (y < 0)
                    _swipeDir = Direction.Down;
                else
                    _swipeDir = Direction.Up;
            }

            OnSwipe?.Invoke(_swipeDir);

            _startTouchPos = _swipeDelta = Vector2.zero;

        }

    }
    #endregion

    #region Public Methods
    public static event Action<Direction> OnSwipe = delegate { };
    #endregion

    #region Private Methods

    #endregion
}
