using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
	public class Suspect : MonoBehaviour
	{
		public event Action OnExit;

		[SerializeField] private Typewriter _typewriter;
		[SerializeField, TextArea] private string _storyText;
		[SerializeField] private Button _startButton;
		[SerializeField] private TextMeshProUGUI _startButtonText;
		[SerializeField] private Button _closeButton;

		private void Awake()
		{
			_startButton.onClick.AddListener(StartSuspect);
			_closeButton.onClick.AddListener(Hide);
		}

		private void StartSuspect()
		{
			_closeButton.gameObject.SetActive(false);
			_typewriter.PlayText(_storyText, () =>
			{
				_closeButton.gameObject.SetActive(true);
				_startButtonText.text = "Replay";
			});
		}

		public void Reveal()
		{
			gameObject.SetActive(true);
		}

		public void Hide()
		{
			OnExit?.Invoke();
		}
	}
}