using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game
{
	public class IntroductionController : MonoBehaviour
	{
		[SerializeField] private Button _nextButton;
		[SerializeField] private SceneReference _sceneToLoad;
		[SerializeField, TextArea] private string _introText;
		[SerializeField] private Typewriter _typewriter;

		private void Awake()
		{
			_nextButton.onClick.AddListener(LoadScene);
			_nextButton.gameObject.SetActive(false);
			_typewriter.PlayText(_introText, () => _nextButton.gameObject.SetActive(true));
		}

		private void LoadScene()
		{
			SceneManager.LoadScene(_sceneToLoad.SceneName);
		}
	}
}