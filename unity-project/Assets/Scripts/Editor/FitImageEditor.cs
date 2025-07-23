using UnityEditor;
using UnityEditor.UI;
using UnityEngine;

[CustomEditor(typeof(FitImage))]
public class FitImageEditor : ImageEditor
{
	SerializedProperty fitMode;

	protected override void OnEnable()
	{
		base.OnEnable();
		fitMode = serializedObject.FindProperty("fitMode");
	}

	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();
		serializedObject.Update();

		EditorGUILayout.Space();
		EditorGUILayout.PropertyField(fitMode, new GUIContent("Fit Mode"));

		serializedObject.ApplyModifiedProperties();
	}
}