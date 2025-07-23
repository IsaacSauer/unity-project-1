#if UNITY_EDITOR

using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
	public static class AutoLoadBootScene
	{
		private const string BootScenePath = "Assets/Scenes/Boot.unity";
		private static string previousScenePath = "";

		[InitializeOnLoadMethod]
		private static void Initialize()
		{
			EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
		}

		private static void OnPlayModeStateChanged(PlayModeStateChange state)
		{
			switch (state)
			{
				case PlayModeStateChange.ExitingEditMode:
					previousScenePath = SceneManager.GetActiveScene().path;

					if (previousScenePath != BootScenePath)
					{
						if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
						{
							EditorSceneManager.OpenScene(BootScenePath);
						}
						else
						{
							EditorApplication.isPlaying = false;
						}
					}
					break;

				case PlayModeStateChange.EnteredEditMode:
					if (!string.IsNullOrEmpty(previousScenePath) && File.Exists(previousScenePath))
					{
						EditorSceneManager.OpenScene(previousScenePath);
					}
					break;
			}
		}
	}
}

#endif