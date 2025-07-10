using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Game
{
    public class Homepage : MonoBehaviour
    {
        #region Editor Fields

        [SerializeField] private TMP_InputField _nameInput;
        [SerializeField] private TMP_InputField _scoreTestInput;
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
            _sendButton.onClick.AddListener(Send);
            _receiveButton.onClick.AddListener(Receive);
            _nameInput.onValueChanged.AddListener(UpdateName);
            _scoreTestInput.onValueChanged.AddListener(UpdateScore);
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
                    Debug.Log($"Name: {playerLeaderboardEntry.DisplayName}, Score: {playerLeaderboardEntry.StatValue}");
                }
            });
        }

        private void UpdateName(string newName)
        {
            _name = newName;
        }

        private void UpdateScore(string score)
        {
            if(int.TryParse(score, out _score) == false)
            {
                Debug.Log("Failed to parse score");
            }
        }
        
        #endregion
    }
}
