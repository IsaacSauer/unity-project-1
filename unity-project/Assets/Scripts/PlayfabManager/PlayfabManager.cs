using System;
using PlayFab;
using PlayFab.ClientModels;
using System.Collections.Generic;
using UnityEngine;

public class PlayfabManager : MonoBehaviour
{
    #region Singleton

    private static PlayfabManager _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }

        Login();
    }

    #endregion Singleton

    #region Editor Fields

    private static string _statisticName = "Score";

    #endregion
    
    private static bool _loggedIn = false;

    private void Login()
    {
        if (_loggedIn) return;

        var request = new LoginWithCustomIDRequest
        {
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true
        };
        PlayFabClientAPI.LoginWithCustomID(request, OnSuccess, OnError);
    }

    private void OnSuccess(LoginResult result)
    {
        Debug.Log("Successful login/acount create!");
        _loggedIn = true;
    }

    private static void OnError(PlayFabError error)
    {
        Debug.Log("Error while logging in/creating account!");
        Debug.Log(error.GenerateErrorReport());
        _loggedIn = false;
    }

    public static void SendLeaderboard(int score, string name)
    {
        if (_loggedIn == false) return;

        PlayFabClientAPI.UpdateUserTitleDisplayName(new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = name
        }, result =>
        {
            Debug.Log("The player's display name is now: " + result.DisplayName);
            var request = new UpdatePlayerStatisticsRequest
            {
                Statistics = new List<StatisticUpdate>
                {
                    new StatisticUpdate
                    {
                        StatisticName = _statisticName,
                        Value = score
                    }
                }
            };
            PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdate, OnError);
        }, error => Debug.LogError(error.GenerateErrorReport()));
    }

    private static void OnLeaderboardUpdate(UpdatePlayerStatisticsResult result)
    {
        Debug.Log("Successfull leaderboard sent");
    }

    public static void GetLeaderboard(Action<GetLeaderboardResult> resultCallback = null)
    {
        if (_loggedIn == false) return;

        var request = new GetLeaderboardRequest
        {
            StatisticName = _statisticName,
            StartPosition = 0,
            MaxResultsCount = 100
        };
        PlayFabClientAPI.GetLeaderboard(request, resultCallback, OnError);
    }
}