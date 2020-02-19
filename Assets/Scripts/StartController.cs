using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartController : MonoBehaviour
{
    private GameManager gameManager;
    public bool testTutPassed;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        gameManager.LoadData();
        gameManager.pData.passedBaseTutorial = testTutPassed;
        if (!gameManager.pData.passedBaseTutorial)
            gameManager.ChangeMode(gameManager.BTUT, 0f);
        else
            gameManager.ChangeMode(gameManager.MENU, 0f);
    }

}
