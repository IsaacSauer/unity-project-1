using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

namespace Game
{
	[InitializeOnLoad]
	public static class AutoLoadBootScene
	{
		private const string BootScenePath = "Assets/Scenes/Boot.unity";
		private static string previousScenePath = "";

		static AutoLoadBootScene()
		{
			EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
		}

		private static void OnPlayModeStateChanged(PlayModeStateChange state)
		{
			switch (state)
			{
				case PlayModeStateChange.ExitingEditMode:
					// Save current scene path
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
					// After play mode ends, return to previous scene
					if (!string.IsNullOrEmpty(previousScenePath) && File.Exists(previousScenePath))
					{
						EditorSceneManager.OpenScene(previousScenePath);
					}
					break;
			}
		}
	}
}