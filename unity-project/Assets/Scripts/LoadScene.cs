#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
	public class LoadScene : MonoBehaviour
	{
		[SerializeField] private string _sceneName;

#if UNITY_EDITOR
		[SerializeField] private SceneAsset _startScene;
		private void OnValidate()
		{
			_sceneName = _startScene.name;
		}
#endif

		private void Start()
		{
			Invoker.InvokeDelayed(() => SceneManager.LoadScene(_sceneName), 1f);
		}
	}
}