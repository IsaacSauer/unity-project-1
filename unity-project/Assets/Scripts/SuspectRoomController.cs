using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
	public class SuspectRoomController : MonoBehaviour
	{
		[Serializable]
		public class SuspectWithButton
		{
			public Button Button;
			public Suspect Suspect;
		}

		[SerializeField] private SuspectWithButton[] _suspects;

		[SerializeField] private Button _nextPuzzleButton;
		[SerializeField] private Typewriter _typewriter;

		[SerializeField, TextArea] private string _introText;

		private int _puzzleIndex = 0;

		private void Awake()
		{
			for (var i = 0; i < _suspects.Length; i++)
			{
				var index = i;
				_suspects[i].Button.onClick.AddListener(() => RevealSuspect(index));
				_suspects[i].Suspect.OnExit += ExitSuspect;
			}
		}

		private void Start()
		{
			HideAllSuspects();
			ShowStoryIntro();
		}

		private void ShowStoryIntro()
		{
			string intro = $"Welcome {EscapeRoomGameManager.Instance.PlayerName}!;" +
			               $"Difficulty: {EscapeRoomGameManager.Instance.Difficulty}.;" +
			               _introText;

			_typewriter.PlayText(intro, RevealAllSuspects);
		}

		private void HideAllSuspects()
		{
			foreach (var suspect in _suspects)
			{
				suspect.Suspect.gameObject.SetActive(false);
				suspect.Button.gameObject.SetActive(false);
			}
		}
		private void RevealAllSuspects()
		{
			foreach (var suspect in _suspects)
			{
				suspect.Button.gameObject.SetActive(true);
			}
		}

		private void RevealSuspect(int index)
		{
			HideAllSuspects();

			_suspects[index].Suspect.Reveal();
		}

		private void ExitSuspect()
		{
			HideAllSuspects();
			RevealAllSuspects();
		}
	}
}