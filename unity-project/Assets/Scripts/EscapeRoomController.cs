using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
	public class EscapeRoomController : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI _storyText;
		[SerializeField] private GameObject _puzzle1Panel, _puzzle2Panel, _puzzle3Panel;
		[SerializeField] private Button _nextPuzzleButton;

		private int _puzzleIndex = 0;

		private void Start()
		{
			ShowStoryIntro();
			_nextPuzzleButton.onClick.AddListener(NextPuzzle);
		}

		private void ShowStoryIntro()
		{
			string intro = $"Welcome {EscapeRoomGameManager.Instance.PlayerName}!\n" +
			               $"Difficulty: {EscapeRoomGameManager.Instance.Difficulty}\n" +
			               "You awaken in a mysterious room filled with ancient artifacts and cryptic symbols. " +
			               "Your goal: Solve the puzzles and escape before time runs out!\n\n" +
			               "A note reads: 'Only those who understand the lore of the room can escape.'";
			_storyText.text = intro;
			_puzzle1Panel.SetActive(false);
			_puzzle2Panel.SetActive(false);
			_puzzle3Panel.SetActive(false);
			_nextPuzzleButton.gameObject.SetActive(true);
		}

		private void NextPuzzle()
		{
			_nextPuzzleButton.gameObject.SetActive(false);
			switch (_puzzleIndex)
			{
				case 0:
					_puzzle1Panel.SetActive(true);
					break;
				case 1:
					_puzzle2Panel.SetActive(true);
					break;
				case 2:
					_puzzle3Panel.SetActive(true);
					break;
				default:
					_storyText.text = "Congratulations! You have escaped the room and uncovered its ancient secrets!";
					break;
			}

			_puzzleIndex++;
		}

		public void OnPuzzleSolved(int puzzleNumber)
		{
			_storyText.text = $"Puzzle {puzzleNumber + 1} solved! The room reveals another secret...";
			_puzzle1Panel.SetActive(false);
			_puzzle2Panel.SetActive(false);
			_puzzle3Panel.SetActive(false);
			_nextPuzzleButton.gameObject.SetActive(true);
		}
	}
}