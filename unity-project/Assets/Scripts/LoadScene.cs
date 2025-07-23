using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
	public class LoadScene : MonoBehaviour
	{
		[SerializeField] private SceneReference _sceneToLoad;

		private void Start()
		{
			Invoker.InvokeDelayed(() => SceneManager.LoadScene(_sceneToLoad.SceneName), 1f);
		}
	}
}