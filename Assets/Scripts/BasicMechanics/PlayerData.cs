using UnityEngine;
using GeneralEnums;
using System;


[System.Serializable]
public class PlayerData
{
	public GeneralData GeneralData;

	public GameplayModeData BaseModeData;
	public GameplayModeData ColorizedModeData;

	public GameplayModeData GetGamePlayModeData(GameplayMode gameplayMode)
	{
		switch (gameplayMode)
		{
			case GameplayMode.Base:
				return BaseModeData;
			case GameplayMode.Colorized:
				return ColorizedModeData;
			default:
				return null;
		}
	}

	public PlayerData()
	{
		GeneralData = new GeneralData();

		BaseModeData = new GameplayModeData(true, 0, 0);
		ColorizedModeData = new GameplayModeData(false, 0, 0);
	}
}

[System.Serializable]
public class GeneralData
{
	public bool PassedTutorial = false;
	public int CurrentSkinId = 0;
	public int[] UnlockedSkinIds = { 0 };


	public int Coins { get; private set; } = 0;

	public void AddCoin()
	{
		Coins += 1;
		OnAddCoins.Invoke(1);
	}

	public void SpendCoins(int coins)
	{
		Coins -= coins;
	}

	public void AddCoins(int coins)
	{
		Coins += coins;
		OnAddCoins.Invoke(coins);
	}

	public static event Action<int> OnAddCoins = delegate { };

	public GeneralData()
	{
		PassedTutorial = false;

		Coins = 0;

		CurrentSkinId = 0;
		UnlockedSkinIds = new int[] { 0 };
	}
}

[System.Serializable]
public class GameplayModeData
{
	private bool _unlocked;

	public int Highscore;
	public int TotalScore;

	public bool Unlocked { get => _unlocked; }

	public GameplayModeData(bool unlocked, int highscore, int totalscore)
	{
		_unlocked = unlocked;
		Highscore = highscore;
		TotalScore = totalscore;
	}

	public void Unlock()
	{
		_unlocked = true;
	}
}

