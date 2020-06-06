using System.Collections;
using GeneralEnums;
using UnityEngine;

public static class PlaymodesCost
{

    public static int GetCost(GameplayMode mode)
    {
        switch (mode)
        {
            case GameplayMode.Base:
                return 0;
            case GameplayMode.Colorized:
                return 1;
            default:
                return -1;
        }
    }
}
