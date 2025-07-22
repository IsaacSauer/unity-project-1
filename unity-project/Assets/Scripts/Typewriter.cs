using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
	public class Typewriter : MonoBehaviour
	{
		#region Events

		public event Action OnEnd;

		#endregion

		[SerializeField] private TMP_Text _textComponent;
		[SerializeField] private float _typingSpeed = 0.05f;
		[SerializeField] private float _delayBetweenSentences = 1.5f;
		[SerializeField] private Button _continueButton;
		[SerializeField] private AudioSource _typingSound; // Audio source for character sound
		[SerializeField] private AudioClip[] _typingClips;
		[TextArea] [SerializeField] private string _fullText;

		private string[] _sentences;
		private int _currentSentenceIndex = 0;
		private bool _isTyping = false;
		private bool _waitForContinue = false;

		public void InitializeText(string text)
		{
			_sentences = text.Split(new[] { '.', '!', '?' }, System.StringSplitOptions.RemoveEmptyEntries);
			StartCoroutine(TypeSentence(_sentences[_currentSentenceIndex].Trim()));
		}

		private void Start()
		{
			if (_continueButton != null)
			{
				_continueButton.gameObject.SetActive(false);
				_continueButton.onClick.AddListener(ContinueToNextSentence);
			}

			if (string.IsNullOrEmpty(_fullText) == false)
			{
				InitializeText(_fullText);
			}
		}

		private IEnumerator TypeSentence(string sentence)
		{
			_isTyping = true;
			_textComponent.text = "";

			foreach (char letter in sentence)
			{
				_textComponent.text += letter;

				var typingClipsLength = _typingClips.Length;
				if (_typingSound != null && !char.IsWhiteSpace(letter) && typingClipsLength != 0)
				{
					_typingSound.clip = _typingClips[UnityEngine.Random.Range(0, typingClipsLength)];
					_typingSound.Play();
				}

				yield return new WaitForSeconds(_typingSpeed);
			}

			_isTyping = false;

			if (_continueButton != null)
			{
				_waitForContinue = true;
				_continueButton.gameObject.SetActive(true);
			}
			else
			{
				yield return new WaitForSeconds(_delayBetweenSentences);
				ContinueToNextSentence();
			}
		}

		private void ContinueToNextSentence()
		{
			if (_waitForContinue)
			{
				_waitForContinue = false;
				_continueButton.gameObject.SetActive(false);
			}

			_currentSentenceIndex++;

			if (_currentSentenceIndex < _sentences.Length)
			{
				StartCoroutine(TypeSentence(_sentences[_currentSentenceIndex].Trim()));
			}
			else
			{
				Debug.Log("All sentences completed.");
				OnEnd?.Invoke();
			}
		}
	}
}