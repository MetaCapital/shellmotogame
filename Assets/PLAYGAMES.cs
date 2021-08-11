

using System;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
//using System.Collections.Generic;
using UnityEngine.UI;



public class PLAYGAMES : MonoBehaviour
{
    public Text playerScore;
    //public  Text playerScore;
    string leaderboardID = "CgkIj4jx5YUNEAIQAQ";
    string achievementID = "CgkIj4jx5YUNEAIQAg";
    public static PlayGamesPlatform platform;

    void Start()
    {
        PlayGamesPlatform.Instance.Authenticate(SignInInteractivity.CanPromptOnce, (result) => {
           print("instance");
        });
        if (platform == null)
        {
            PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
            PlayGamesPlatform.InitializeInstance(config);
            PlayGamesPlatform.DebugLogEnabled = true;
            platform = PlayGamesPlatform.Activate();
        }

        Social.Active.localUser.Authenticate(success =>
        {
            if (success)
            {
                Debug.Log("Logged in successfully");
            }
            else
            {
                Debug.Log("Login Failed");
            }
        });
        UnlockAchievement();
    }

    public void AddScoreToLeaderboard()
    {
        if (Social.Active.localUser.authenticated)
        {
            
            int coin = PlayerPrefs.GetInt("coins");


            Social.ReportScore(coin, leaderboardID, success => { });
        }
    }

    public void ShowLeaderboard()
    {
        if (Social.Active.localUser.authenticated)
        {
            platform.ShowLeaderboardUI();
        }
    }

    public void ShowAchievements()
    {
        if (Social.Active.localUser.authenticated)
        {
            platform.ShowAchievementsUI();
        }
    }

    public void UnlockAchievement()
    {
        if (Social.Active.localUser.authenticated)
        {
            Social.ReportProgress(achievementID, 100f, success => { });
        }
    }
}