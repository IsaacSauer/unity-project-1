using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Game
{
	public class Homepage : MonoBehaviour
	{
		#region Editor Fields

		[SerializeField] private TextMeshProUGUI _title;
		[SerializeField] private TMP_InputField _nameInput;
		[SerializeField] private TMP_InputField _scoreTestInput;
		[SerializeField] private Button _loginButton;
		[SerializeField] private Button _sendButton;
		[SerializeField] private Button _receiveButton;

		#endregion

		#region Fields

		private string _name;
		private int _score;

		#endregion

		#region Methods

		private void Awake()
		{
			_loginButton.onClick.AddListener(Login);
			_sendButton.onClick.AddListener(Send);
			_receiveButton.onClick.AddListener(Receive);
			_nameInput.onValueChanged.AddListener(UpdateName);
			_scoreTestInput.onValueChanged.AddListener(UpdateScore);
			SetEnabled(false);
		}

		private void Login()
		{
			if (PlayfabManager.IsLoggedIn == false)
			{
				if (string.IsNullOrEmpty(_name) == false)
				{
					PlayfabManager.Login(_name, result =>
					{
						var titleText = $"Logged in as: <color=green>{_name}</color>";
						Debug.Log(titleText);
						_title.text = titleText;
						SetEnabled(true);
						_nameInput.gameObject.SetActive(false);
						_loginButton.gameObject.SetActive(false);
					});
				}
			}
			else
			{
				SetEnabled(true);
				_nameInput.gameObject.SetActive(false);
				_loginButton.gameObject.SetActive(false);
			}
		}

		private void SetEnabled(bool setEnabled)
		{
			_scoreTestInput.gameObject.SetActive(setEnabled);
			_sendButton.gameObject.SetActive(setEnabled);
			_receiveButton.gameObject.SetActive(setEnabled);
		}

		private void Send()
		{
			if (string.IsNullOrEmpty(_name) == false)
			{
				PlayfabManager.SendLeaderboard(_score, _name);
			}
		}

		private void Receive()
		{
			PlayfabManager.GetLeaderboard(result =>
			{
				foreach (var playerLeaderboardEntry in result.Leaderboard)
				{
					Debug.Log(
						$"Name: {playerLeaderboardEntry.DisplayName}, Score: {playerLeaderboardEntry.StatValue}");
				}
			});
		}

		private void UpdateName(string newName)
		{
			_name = newName;
		}

		private void UpdateScore(string score)
		{
			if (int.TryParse(score, out _score) == false)
			{
				Debug.Log("Failed to parse score");
			}
		}

		#endregion
	}
}