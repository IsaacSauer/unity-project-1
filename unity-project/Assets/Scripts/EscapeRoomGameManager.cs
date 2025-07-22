using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
	public enum DifficultyLevel
	{
		Easy,
		Hard
	}

	public class EscapeRoomGameManager : Singleton<EscapeRoomGameManager>
	{
		public string PlayerName { get; private set; }
		public DifficultyLevel Difficulty { get; private set; }

		public void SetPlayerName(string name)
		{
			PlayerName = name;
		}

		public void SetDifficulty(DifficultyLevel difficulty)
		{
			Difficulty = difficulty;
		}

		public void StartGame()
		{
			SceneManager.LoadScene("EscapeRoomScene");
		}
	}
}