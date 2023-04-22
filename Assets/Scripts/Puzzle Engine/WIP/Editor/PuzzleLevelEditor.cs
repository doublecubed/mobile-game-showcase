// ------------------------
// Onur Ereren - April 2023
// ------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using UnityEditor.Graphs;

namespace IslandGame.PuzzleEngine
{
	[CustomEditor(typeof(PuzzleLevel))]
	[CanEditMultipleObjects]
	public class PuzzleLevelEditor : Editor
	{
		private SerializedProperty _levelRowSize;
		private SerializedProperty _numberOfNodes;

		private void OnEnable()
		{
			_levelRowSize = serializedObject.FindProperty("_rowSize");
			_numberOfNodes = serializedObject.FindProperty("_numberOfNodes");
		}

		public override void OnInspectorGUI()
		{
			PuzzleLevel levelScript = (PuzzleLevel)target;
			int storedRowValue = _levelRowSize.intValue;
			int storedNodeValue = _numberOfNodes.intValue;

			serializedObject.Update();

			EditorGUILayout.BeginHorizontal();

				EditorGUILayout.PropertyField(_numberOfNodes);

			EditorGUILayout.EndHorizontal();


			EditorGUILayout.BeginHorizontal();

				EditorGUILayout.PropertyField(_levelRowSize);
			
			EditorGUILayout.EndHorizontal();

			#region SETTING LEVEL PARAMETERS

			if (_levelRowSize.intValue < 1)
			{
				_levelRowSize.intValue = storedRowValue;
			}

			if (_numberOfNodes.intValue < 1)
			{
				_numberOfNodes.intValue = storedNodeValue;
			}

			int?[,] modifiedConfiguration = levelScript.ResizeLevel(_numberOfNodes.intValue, _levelRowSize.intValue);
			
			
			#endregion

			#region VISUALISATION

			for (int i = 0; i < _numberOfNodes.intValue; i++)
			{
				GUILayout.BeginHorizontal();

				for (int j = 0; j < _levelRowSize.intValue ; j++)
				{
					
					
					if (modifiedConfiguration[i, j] != null)
					{
						EditorGUILayout.TextField(modifiedConfiguration[i, j].ToString());
					}
					else
					{
						EditorGUILayout.TextField("N");
					}
				}
				
				
				GUILayout.EndHorizontal();
			}
			
			
			#endregion
			
			serializedObject.ApplyModifiedProperties();
		}

		/*
		#region VARIABLES
		
		private SerializedProperty _levelRowSize;
		private SerializedProperty _numberOfNodes;
		private SerializedProperty _numberOfLockedNodes;
		private SerializedProperty _rowConfiguration;

		private const int DIV_PADDING = 10;
		
		private const int NODE_PIXEL_WIDTH = 100;
		private const int NODE_PIXEL_GAP = 10;

		private const float ROW_GAP_RATIO = 0.5f;   // The amount of gap between the rows, specified in multiples of row width

		private Color nodeBgColor = new Color(1f, 1f, 1f, 1f);
		private Color neutralRowColor = new Color(1f, 0f, 0.5f, 1f);
		
		#endregion
		
		private void OnEnable()
		{
			_levelRowSize = serializedObject.FindProperty("_rowSize");
			_numberOfNodes = serializedObject.FindProperty("_numberOfNodes");
			_numberOfLockedNodes = serializedObject.FindProperty("_numberOfLockedNodes");
			_rowConfiguration = serializedObject.FindProperty("_rowConfiguration");
		}

		public override void OnInspectorGUI()
		{
			PuzzleLevel levelScript = (PuzzleLevel)target;

			int storedRowValue = _levelRowSize.intValue;
			int storedNodeValue = _numberOfNodes.intValue;
			
			serializedObject.Update();

			#region PARAMETERS DECLARATION
			
			EditorGUILayout.BeginHorizontal();

				EditorGUILayout.PropertyField(_numberOfNodes);

			EditorGUILayout.EndHorizontal();


			EditorGUILayout.BeginHorizontal();

				EditorGUILayout.PropertyField(_levelRowSize);
			
			EditorGUILayout.EndHorizontal();

			#endregion
			
			#region SETTING LEVEL PARAMETERS

			if (_levelRowSize.intValue < 1)
			{
				_levelRowSize.intValue = storedRowValue;
			}

			if (_numberOfNodes.intValue < 1)
			{
				_numberOfNodes.intValue = storedNodeValue;
			}

			int?[,] modifiedConfiguration = levelScript.ResizeLevel(_numberOfNodes.intValue, _levelRowSize.intValue);
			
			#endregion
		
			#region LEVEL VISUALISATION
			
			EditorGUILayout.BeginHorizontal();

				#region LEFT VERTICAL SECTION
			
				EditorGUILayout.BeginVertical();

					Rect sectionRectLeft = GUILayoutUtility.GetRect(GUIContent.none, GUIStyle.none, GUILayout.ExpandWidth(true));
					EditorGUI.DrawRect(sectionRectLeft, Color.green);
				
					int numberOfNodesOnLeft = (_numberOfNodes.intValue / 2) + (_numberOfNodes.intValue % 2);

					for (int i = 0; i < numberOfNodesOnLeft; i++)
					{
						DrawRect(sectionRectLeft.x + DIV_PADDING, NodeYPosition(sectionRectLeft.y, i), NODE_PIXEL_WIDTH, NODE_PIXEL_WIDTH, nodeBgColor);

						float rowWidth = RowWidth(NODE_PIXEL_WIDTH, _levelRowSize.intValue, ROW_GAP_RATIO);
						
						for (int j = 0; j < _levelRowSize.intValue; j++)
						{
							float rowXPosition = RowXPosition(sectionRectLeft.x + DIV_PADDING + rowWidth * ROW_GAP_RATIO, ROW_GAP_RATIO, rowWidth, j);
							//DrawRect(rowXPosition, NodeYPosition(sectionRectLeft.y,i) + DIV_PADDING, rowWidth,NODE_PIXEL_WIDTH - (2*DIV_PADDING), neutralRowColor);

							Rect buttonRect = PrepareRect(rowXPosition, NodeYPosition(sectionRectLeft.y, i) + DIV_PADDING, rowWidth, NODE_PIXEL_WIDTH - (2 * DIV_PADDING));

							
							GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);
							buttonStyle.padding = new RectOffset(0,0,0,0);
							buttonStyle.margin = new RectOffset(0, 0, 0, 0);
							buttonStyle.normal.background = MakeTexture(neutralRowColor);
							if (GUI.Button(buttonRect, MakeTexture(neutralRowColor), buttonStyle)) Debug.Log("Button clicked");
							//if (GUILayout.Button("",buttonStyle)) Debug.Log("this one is clicked");
							
						}
					}
					
					
					
				EditorGUILayout.EndVertical();
			
				#endregion
				
				#region MIDDLE VERTICAL SECTION
				
				EditorGUILayout.BeginVertical();
				
					Rect sectionRectMiddle = GUILayoutUtility.GetRect(GUIContent.none, GUIStyle.none, GUILayout.ExpandWidth(true));
					EditorGUI.DrawRect(sectionRectMiddle, Color.red);
				
				EditorGUILayout.EndVertical();
			
				#endregion
				
				#region RIGHT VERTICAL SECTION
				
				EditorGUILayout.BeginVertical();
				
					
				
					int numberOfNodesOnRight = (_numberOfNodes.intValue / 2);

					Rect sectionRectRight = GUILayoutUtility.GetRect(GUIContent.none, GUIStyle.none, GUILayout.ExpandWidth(true));
					EditorGUI.DrawRect(sectionRectRight, Color.cyan);
					
					float nodeXPosition = sectionRectRight.x + sectionRectRight.width - (NODE_PIXEL_WIDTH + DIV_PADDING);
					
					for (int i = 0; i < numberOfNodesOnRight; i++)
					{
						DrawRect(nodeXPosition, NodeYPosition(sectionRectRight.y, i), NODE_PIXEL_WIDTH, NODE_PIXEL_WIDTH, nodeBgColor);
						
						float rowWidth = RowWidth(NODE_PIXEL_WIDTH, _levelRowSize.intValue, ROW_GAP_RATIO);
						
						for (int j = 0; j < _levelRowSize.intValue; j++)
						{
							float rowXPosition = RowXPosition(sectionRectRight.x + sectionRectRight.width - (NODE_PIXEL_WIDTH + DIV_PADDING) + (rowWidth * ROW_GAP_RATIO), ROW_GAP_RATIO, rowWidth, j);
							DrawRect(rowXPosition, NodeYPosition(sectionRectRight.y,i) + DIV_PADDING, rowWidth,NODE_PIXEL_WIDTH - (2*DIV_PADDING), neutralRowColor);
						}
					}
					
				EditorGUILayout.EndVertical();
			
				#endregion
				
			EditorGUILayout.EndHorizontal();


			
			#endregion

			serializedObject.ApplyModifiedProperties();
		}

		#region METHODS
		
		private void DrawNode(float xPosition, float yPosition)
		{
			Rect nodeToDraw = new Rect();
			nodeToDraw.x = xPosition;
			nodeToDraw.y = yPosition;
			nodeToDraw.width = NODE_PIXEL_WIDTH;
			nodeToDraw.height = nodeToDraw.width;
			
			EditorGUI.DrawRect(nodeToDraw, nodeBgColor);
		}

		private float NodeYPosition(float rectY, int index)
		{
			return DIV_PADDING + rectY + (index * (NODE_PIXEL_WIDTH + NODE_PIXEL_GAP));
		}

		private float RowWidth(float rectWidth, int numberOfRows, float gapRatio)
		{
			float totalWidthUnits = numberOfRows + ((float)numberOfRows + 1) * gapRatio;
			return rectWidth / totalWidthUnits;
		}
		
		private float RowXPosition(float rectX, float gapRatio, float rowWidth, int index)
		{
			float gapWidth = rowWidth * gapRatio;

			float xOffsetFromRect = (rowWidth + gapWidth) * index;

			return rectX + xOffsetFromRect;
		}
		
		private void DrawRect(float xPosition, float yPosition, float width, float height, Color nodeColor)
		{
			Rect rectToDraw = PrepareRect(xPosition, yPosition, width, height);
			EditorGUI.DrawRect(rectToDraw, nodeColor);
		}
		
		private Rect PrepareRect(float xPosition, float yPosition, float width, float height)
		{
			Rect rect = new Rect();
			rect.x = xPosition;
			rect.y = yPosition;
			rect.width = width;
			rect.height = height;

			return rect;
		}

		private Texture2D MakeTexture(Color color)
		{
			Texture2D texture = new Texture2D(1, 1);
			texture.SetPixel(0,0,color);
			texture.Apply();
			return texture;
		}
		
		#endregion
		*/
	}
	
}