using UnityEditor;
using UnityEngine;
using System.Linq;

[CustomPropertyDrawer(typeof(SceneReference))]
public class SceneReferenceDrawer : PropertyDrawer
{
	private static string[] _buildScenes;

	private void UpdateBuildScenes()
	{
		_buildScenes = EditorBuildSettings.scenes
			.Where(scene => scene.enabled)
			.Select(scene => scene.path)
			.ToArray();
	}

	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
#if UNITY_EDITOR
		if (_buildScenes == null)
			UpdateBuildScenes();

		EditorGUI.BeginProperty(position, label, property);

		var sceneAssetProp = property.FindPropertyRelative("_sceneAsset");
		var scenePathProp = property.FindPropertyRelative("_scenePath");

		Rect fieldRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);

		if (sceneAssetProp != null)
		{
			EditorGUI.BeginChangeCheck();
			var sceneAsset = (SceneAsset)sceneAssetProp.objectReferenceValue;
			var newSceneAsset =
				(SceneAsset)EditorGUI.ObjectField(fieldRect, label, sceneAsset, typeof(SceneAsset), false);
			if (EditorGUI.EndChangeCheck())
			{
				sceneAssetProp.objectReferenceValue = newSceneAsset;
				if (newSceneAsset != null)
				{
					scenePathProp.stringValue = AssetDatabase.GetAssetPath(newSceneAsset);
				}
				else
				{
					scenePathProp.stringValue = string.Empty;
				}
			}
		}
		else
		{
			// sceneAsset property doesn't exist - just draw label and path string
			string scenePath = scenePathProp != null ? scenePathProp.stringValue : "No scene path";
			EditorGUI.LabelField(fieldRect, label.text, scenePath);
		}

		// Draw help box under field
		Rect warningRect = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight + 2, position.width,
			EditorGUIUtility.singleLineHeight);

		string scenePathCheck = scenePathProp != null ? scenePathProp.stringValue : "";

		if (!string.IsNullOrEmpty(scenePathCheck))
		{
			bool inBuild = _buildScenes.Contains(scenePathCheck);
			if (!inBuild)
			{
				EditorGUI.HelpBox(warningRect, "Scene is not in Build Settings!", MessageType.Warning);
			}
		}
		else
		{
			EditorGUI.HelpBox(warningRect, "No Scene assigned", MessageType.Info);
		}

		EditorGUI.EndProperty();
#endif
	}

	public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
	{
#if UNITY_EDITOR
		// Height for field + help box
		return EditorGUIUtility.singleLineHeight * 2 + 4;
#else
        return base.GetPropertyHeight(property, label);
#endif
	}
}