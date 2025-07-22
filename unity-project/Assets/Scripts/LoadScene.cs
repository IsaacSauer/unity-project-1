using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
	public class LoadScene : MonoBehaviour
	{
		[SerializeField] private SceneAsset _startScene;

		private void Start()
		{
			Invoker.InvokeDelayed(() => SceneManager.LoadScene(_startScene.name), 1f);
		}
	}
}