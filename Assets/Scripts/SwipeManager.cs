using UnityEngine;

public class SwipeManager : MonoBehaviour
{
    public GameManager gameManager;

    private void Awake()
    {
        //SwipeDetector.OnSwipe += SwipeDetector_OnSwipe;
    }

    //private void SwipeDetector_OnSwipe(SwipeData data)
    //{
    //    Debug.Log("Swipe in Direction: " + data.Direction);
    //}
}
