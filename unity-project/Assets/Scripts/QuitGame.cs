using UnityEngine;
using UnityEngine.UI;

namespace Game
{
	[RequireComponent(typeof(Button))]
	public class QuitGame : MonoBehaviour
	{
		private void Awake()
		{
			GetComponent<Button>().onClick.AddListener((() =>
			{
#if UNITY_EDITOR
				UnityEditor.EditorApplication.isPlaying = false;
#else
				Application.Quit();
#endif
			}));
		}
	}
}