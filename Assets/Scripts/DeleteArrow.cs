
using UnityEngine;

public class DeleteArrow : MonoBehaviour
{
    private GameManager gm;
    private ArrowsManager am;

    private void Awake()
    {
        gm = FindObjectOfType<GameManager>();
        am = FindObjectOfType<ArrowsManager>();
    }

    public void DeleteActive()
    {
        GameObject fading = GameObject.FindGameObjectWithTag("FadingArrow");
        if (fading != null)
            Destroy(fading);  
    }
}
