using UnityEngine;


[System.Serializable]
public class PlayerData
{
	#region public data variables
	public bool passedBaseTutorial = false;
	public int coins = 0;
	public int currentSkin = 0;
	public int[] unlockedSkinIds = { 0 };
	#endregion

	public PlayerData()
	{
		passedBaseTutorial = false;
		coins = 0;
		currentSkin = 0;
		unlockedSkinIds = new int[] { 0 };
	}

}
