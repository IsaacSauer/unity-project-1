#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.UI;
using UnityEngine;

[CustomEditor(typeof(FitImage))]
public class FitImageEditor : ImageEditor
{
	private SerializedProperty _fitMode;

	protected override void OnEnable()
	{
		base.OnEnable();
		_fitMode = serializedObject.FindProperty("_fitMode");
	}

	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();
		serializedObject.Update();

		EditorGUILayout.Space();
		EditorGUILayout.PropertyField(_fitMode, new GUIContent("Fit Mode"));

		serializedObject.ApplyModifiedProperties();
	}
}
#endif