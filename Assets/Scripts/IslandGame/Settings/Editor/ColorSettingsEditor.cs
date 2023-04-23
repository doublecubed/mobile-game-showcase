// ------------------------
// Onur Ereren - April 2023
// ------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace IslandGame.Settings
{

	[CustomEditor(typeof(ColorSettings))]
	public class ColorSettingsEditor : Editor
	{
		private SerializedProperty _colorValue0;
		private SerializedProperty _colorValue1;
		private SerializedProperty _colorValue2;
		private SerializedProperty _colorValue3;
		private SerializedProperty _colorValue4;
		private SerializedProperty _colorValue5;
		private SerializedProperty _colorValue6;
		private SerializedProperty _colorValue7;
		private SerializedProperty _colorValue8;
		private SerializedProperty _colorValue9;
		
		private void OnEnable()
		{
			_colorValue0 = serializedObject.FindProperty("color0");
			_colorValue1 = serializedObject.FindProperty("color1");
			_colorValue2 = serializedObject.FindProperty("color2");
			_colorValue3 = serializedObject.FindProperty("color3");
			_colorValue4 = serializedObject.FindProperty("color4");
			_colorValue5 = serializedObject.FindProperty("color5");
			_colorValue6 = serializedObject.FindProperty("color6");
			_colorValue7 = serializedObject.FindProperty("color7");
			_colorValue8 = serializedObject.FindProperty("color8");
			_colorValue9 = serializedObject.FindProperty("color9");
		}

		public override void OnInspectorGUI()
		{
			serializedObject.Update();
			
			EditorGUILayout.PropertyField(_colorValue0);
			EditorGUILayout.PropertyField(_colorValue1);
			EditorGUILayout.PropertyField(_colorValue2);
			EditorGUILayout.PropertyField(_colorValue3);
			EditorGUILayout.PropertyField(_colorValue4);
			EditorGUILayout.PropertyField(_colorValue5);
			EditorGUILayout.PropertyField(_colorValue6);
			EditorGUILayout.PropertyField(_colorValue7);
			EditorGUILayout.PropertyField(_colorValue8);
			EditorGUILayout.PropertyField(_colorValue9);
			
			ColorSettings settings = (ColorSettings)target;
			
			serializedObject.ApplyModifiedProperties();
		}
		
		
	}
}