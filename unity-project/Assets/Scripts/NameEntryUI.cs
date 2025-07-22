using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game
{
	public class NameEntryUI : MonoBehaviour
	{
		[SerializeField] private TMP_InputField _nameInput;
		[SerializeField] private Button _continueButton;

		private void Start()
		{
			_continueButton.onClick.AddListener(OnContinue);
		}

		private void OnContinue()
		{
			string playerName = _nameInput.text.Trim();
			if (string.IsNullOrEmpty(playerName) == false)
			{
				PlayfabManager.Login(playerName, result =>
				{
					var titleText = $"Logged in as: <color=green>{playerName}</color>";
					Debug.Log(titleText);
					_nameInput.gameObject.SetActive(false);

					EscapeRoomGameManager.Instance.SetPlayerName(playerName);
					SceneManager.LoadScene("DifficultySelectScene");
				});
			}
		}
	}
}