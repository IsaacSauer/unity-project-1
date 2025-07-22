using UnityEngine;
using UnityEngine.UI;

namespace Game
{
	public class DifficultySelectUI : MonoBehaviour
	{
		[SerializeField] private Button _easyButton, _hardButton;

		void Start()
		{
			_easyButton.onClick.AddListener(() => SelectDifficulty(DifficultyLevel.Easy));
			_hardButton.onClick.AddListener(() => SelectDifficulty(DifficultyLevel.Hard));
		}

		void SelectDifficulty(DifficultyLevel difficulty)
		{
			EscapeRoomGameManager.Instance.SetDifficulty(difficulty);
			EscapeRoomGameManager.Instance.StartGame();
		}
	}
}