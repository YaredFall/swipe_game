using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swipeLog : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (MobileInput.Instance.SwipeLeft)
            Debug.Log("Left swipe!");
        if (MobileInput.Instance.SwipeRight)
            Debug.Log("Right swipe!");
        if (MobileInput.Instance.SwipeUp)
            Debug.Log("Up swipe!");
        if (MobileInput.Instance.SwipeDown)
            Debug.Log("Down swipe!");
    }
}
