using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game
{
	public class IntroductionController : MonoBehaviour
	{
		[SerializeField] private Button _nextButton;
		[SerializeField] private SceneReference _sceneToLoad;

		private void Awake()
		{
			_nextButton.onClick.AddListener(LoadScene);
		}

		private void LoadScene()
		{
			SceneManager.LoadScene(_sceneToLoad.SceneName);
		}
	}
}