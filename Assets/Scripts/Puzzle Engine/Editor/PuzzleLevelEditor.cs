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
	public class PuzzleLevelEditor : Editor
	{
		private SerializedProperty _levelRowSize;
		private SerializedProperty _numberOfNodes;
		private SerializedProperty _numberOfLockedNodes;
		private SerializedProperty _rowConfiguration;

		private const int DIV_PADDING = 10;
		
		private const int NODE_PIXEL_WIDTH = 80;
		private const int NODE_PIXEL_GAP = 10;

		private const float ROW_GAP_RATIO = 0.5f;   // The amount of gap between the rows, specified in multiples of row width

		private Color nodeBgColor = new Color(1f, 1f, 1f, 1f);
		private Color neutralRowColor = new Color(0.5f, 0.5f, 0.5f, 1f);
		
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
				_levelRowSize.intValue = 1;
			}

			if (_numberOfNodes.intValue < 1)
			{
				_numberOfNodes.intValue = 1;
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
							DrawRect(rowXPosition, NodeYPosition(sectionRectLeft.y,i) + DIV_PADDING, rowWidth,NODE_PIXEL_WIDTH - (2*DIV_PADDING), neutralRowColor);
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
			Rect rectToDraw = new Rect();
			rectToDraw.x = xPosition;
			rectToDraw.y = yPosition;
			rectToDraw.width = width;
			rectToDraw.height = height;
			
			EditorGUI.DrawRect(rectToDraw, nodeColor);
		}
		
	}
	
}