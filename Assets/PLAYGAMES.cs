

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
    //string leaderboardID = "CgkIj4jx5YUNEAIQBg";
    //string achievementID = "CgkIj4jx5YUNEAIQAg";
    string leaderboardID = "CgkI08WD3dYdEAIQAg";
    string achievementID = "CgkI08WD3dYdEAIQAQ";
    public static PlayGamesPlatform platform;

    void Start()
    {
        
        if (platform == null)
        {
            PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
            PlayGamesPlatform.InitializeInstance(config);
            PlayGamesPlatform.DebugLogEnabled = true;
            platform = PlayGamesPlatform.Activate();
            PlayGamesPlatform.Instance.Authenticate(SignInInteractivity.CanPromptOnce, (result) => {
                print("instance");
            });
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
    public void Login() {
        this.Start();
    }
    public void AddScoreToLeaderboard()
    {
        if (Social.Active.localUser.authenticated)
        {
            
            long bestDistance = (long)(PlayerPrefs.GetFloat("bestDistance")*100);
            Social.ReportScore(bestDistance, leaderboardID, success => { Debug.Log("PlayGames > Successful"); });
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