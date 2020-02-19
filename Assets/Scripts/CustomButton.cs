using UnityEngine.UI;
using UnityEngine;

public class CustomButton : MonoBehaviour
{
    void Start()
    {
        this.GetComponent<Image>().alphaHitTestMinimumThreshold = 0.5f;
    }
}
