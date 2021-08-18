using System;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayGamesConnector
{

	//public  Text playerScore;
	//private static string leaderboardID = "CgkIj4jx5YUNEAIQBg";
	//private static string achievementID = "CgkIj4jx5YUNEAIQAg";
	private static string leaderboardID = "CgkI08WD3dYdEAIQAQ";
	private static string achievementID = "CgkI08WD3dYdEAIQAg";

	public static PlayGamesPlatform platform;
	public static bool connectionSuccess;
	public static string connectionSuccessMessage;

	public static bool _AddScoreToLeaderboard_Success = false;
	public static void AddScoreToLeaderboard()
	{
		_AddScoreToLeaderboard_Success = false;
		Debug.Log("trying to add score to leaderboard");
		if (Social.Active.localUser.authenticated)
		{
			long bestDistance = (long)(PlayerPrefs.GetFloat("bestDistance") * 100);
			Social.ReportScore(bestDistance, leaderboardID, success => { PlayGamesConnector._AddScoreToLeaderboard_Success = true; Debug.Log("Success Status" + success); });
		}
		return;
	}

	public static bool _ShowLeaderboard_Success = false;
	public static void ShowLeaderboard()
	{
		Debug.Log("trying to Display the Leaderboard");
		_ShowLeaderboard_Success = false;
		if (Social.Active.localUser.authenticated)
		{
			platform.ShowLeaderboardUI();
		}

	}
	public static void connect()
	{
		Debug.Log("trying to connect to leaderboard");
		if (platform == null)
		{
			PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
			PlayGamesPlatform.InitializeInstance(config);
			PlayGamesPlatform.DebugLogEnabled = true;
			platform = PlayGamesPlatform.Activate();
			PlayGamesPlatform.Instance.Authenticate(SignInInteractivity.CanPromptOnce, (result) => {
				if (result == SignInStatus.Success)
				{
					connectionSuccessMessage = "Hi " + Social.localUser.userName + " (" + Social.localUser.id + ")";
				}
				else
				{
					connectionSuccessMessage = "*** Failed to authenticate with " + result;
				}

			});
		}

		Social.Active.localUser.Authenticate(success =>
		{
			if (success)
			{
				PlayGamesConnector.connectionSuccess = true;
			}
			else
			{
				PlayGamesConnector.connectionSuccess = false;
			}
		});


	}

}