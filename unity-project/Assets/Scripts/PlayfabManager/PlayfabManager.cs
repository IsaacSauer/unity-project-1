using System;
using PlayFab;
using PlayFab.ClientModels;
using System.Collections.Generic;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class PlayfabManager : MonoBehaviour
{
	#region Singleton

	private static PlayfabManager _instance;

	private void Awake()
	{
		if (_instance == null)
		{
			SetupCertificateValidation();
			_instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else if (_instance != this)
		{
			Destroy(gameObject);
		}
	}

	#endregion Singleton

	#region Editor Fields

	private static string _statisticName = "Score";

	#endregion

	#region Properties

	public static string CurrentLoggedInUser => _currentLoggedInUser;
	public static bool IsLoggedIn => string.IsNullOrEmpty(_currentLoggedInUser) == false;

	#endregion

	private static string _currentLoggedInUser;

	private void SetupCertificateValidation()
	{
		PlayFab.Internal.PlayFabWebRequest.CustomCertValidationHook = CertificateValidationCallback;
	}

	private bool CertificateValidationCallback(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
	{
#if UNITY_EDITOR
		// In the Unity editor, accept all certificates for convenience
		Debug.Log("Skipping cert validation in editor.");
		return true;
#else
        // In production, reject if there are SSL policy errors
        if (sslPolicyErrors == SslPolicyErrors.None)
        {
            return true;
        }
        else
        {
            Debug.LogError($"Certificate error: {sslPolicyErrors}");
            return false;
        }
#endif
	}

	public static void Login(string username, Action<LoginResult> callback)
	{
		if (_currentLoggedInUser == username)
		{
			callback?.Invoke(new LoginResult());
			return;
		}

		var request = new LoginWithCustomIDRequest
		{
			CustomId = username,
			CreateAccount = true
		};

		PlayFabClientAPI.LoginWithCustomID(request, result =>
		{
			_currentLoggedInUser = username;
			callback?.Invoke(result);
		}, OnError);
	}

	private static void OnError(PlayFabError error)
	{
		Debug.Log("Error while logging in/creating account!");
		Debug.Log(error.GenerateErrorReport());
	}

	public static void SendLeaderboard(int score, string username)
	{
		Login(username, _ => { SendLeaderboardInternal(score, username); });
	}

	private static void SendLeaderboardInternal(int score, string name)
	{
		if (_currentLoggedInUser == string.Empty) return;

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
		if (_currentLoggedInUser == string.Empty) return;

		var request = new GetLeaderboardRequest
		{
			StatisticName = _statisticName,
			StartPosition = 0,
			MaxResultsCount = 100
		};
		PlayFabClientAPI.GetLeaderboard(request, resultCallback, OnError);
	}
}