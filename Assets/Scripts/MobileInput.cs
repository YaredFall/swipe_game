using System.Collections;
using System;
using UnityEngine;

public class MobileInput : MonoBehaviour
{
    public float Deadzone = 100f;

    public static MobileInput Instance { set; get;  }

    private Vector2 startTouch, swipeDelta;

    private bool swipeLeft, swipeRight, swipeUp, swipeDown;

    public bool SwipeLeft { get { return swipeLeft; } }
    public bool SwipeRight { get { return swipeRight; } }
    public bool SwipeUp { get { return swipeUp; } }
    public bool SwipeDown { get { return swipeDown; } }

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        swipeLeft = swipeRight = swipeUp = swipeDown = false;
    }

    // Update is called once per frame
    void Update()
    {
        swipeLeft = swipeRight = swipeUp = swipeDown = false;

        #region Standalone Input
        if (Input.GetMouseButtonDown(0))
            startTouch = Input.mousePosition;
        else if (Input.GetMouseButtonUp(0))
            startTouch = swipeDelta = Vector2.zero;
        #endregion

        #region Mobile Input
        if (Input.touches.Length != 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
                startTouch = Input.mousePosition;
            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
                startTouch = swipeDelta = Vector2.zero;
        }
        #endregion

        swipeDelta = Vector2.zero;
        if (startTouch != Vector2.zero)
        {
            if (Input.touches.Length != 0)
                swipeDelta = Input.touches[0].position - startTouch;
            else if (Input.GetMouseButton(0))
                swipeDelta = (Vector2)Input.mousePosition - startTouch;
        }

        if (swipeDelta.magnitude > Deadzone)
        {
            float x = swipeDelta.x;
            float y = swipeDelta.y;

            if (Math.Abs(x) > Math.Abs(y))
            {
                if (x < 0)
                {
                    swipeLeft = true;
                }

                else
                {
                    swipeRight = true;
                }
            }
            else
            {
                if (y < 0)
                {
                    swipeDown = true;
                }
                else
                {
                    swipeUp = true;
                }
            }

            startTouch = swipeDelta = Vector2.zero;

        }

    }

}
