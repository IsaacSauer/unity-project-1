using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[System.Serializable]
public class SceneReference
{
#if UNITY_EDITOR
	[SerializeField] private SceneAsset _sceneAsset;
#endif

	[SerializeField, HideInInspector] private string _scenePath = string.Empty;

	public string ScenePath => _scenePath;
	public string SceneName => System.IO.Path.GetFileNameWithoutExtension(_scenePath);

#if UNITY_EDITOR
	public void UpdateScenePath()
	{
		if (_sceneAsset != null)
		{
			_scenePath = AssetDatabase.GetAssetPath(_sceneAsset);
		}
		else
		{
			_scenePath = string.Empty;
		}
	}
#endif
}