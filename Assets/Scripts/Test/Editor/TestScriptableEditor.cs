using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TestScriptable))]
public class TestScriptableEditor : Editor
{
    SerializedProperty arraySizeProp;
    SerializedProperty arrayItselfProp;
    const int MAX_RECTS_PER_ROW = 8;
    const int RECT_SIZE = 16;
    const int RECT_SPACING = 4;
    const int SQUARE_PADDING = 8;
    Color squareColor = new Color(0.9f, 0.9f, 0.9f, 1f);
    Color rectColor = new Color(0.2f, 0.2f, 0.2f, 1f);

    void OnEnable()
    {
        arraySizeProp = serializedObject.FindProperty("rowCount");
        arrayItselfProp = serializedObject.FindProperty("rowNumbers");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.BeginHorizontal();

            EditorGUILayout.BeginVertical();
            
                Rect sectionRectOne = GUILayoutUtility.GetRect(GUIContent.none, GUIStyle.none,GUILayout.ExpandWidth(true));

                EditorGUI.DrawRect(sectionRectOne, Color.white);
            
            EditorGUILayout.EndVertical();
        
            EditorGUILayout.BeginVertical();
            
                Rect sectionRectTwo = GUILayoutUtility.GetRect(GUIContent.none, GUIStyle.none,GUILayout.ExpandWidth(true));

                EditorGUI.DrawRect(sectionRectTwo, Color.red);
            
            EditorGUILayout.EndVertical();
            
            EditorGUILayout.BeginVertical();
            
                Rect sectionRectThree = GUILayoutUtility.GetRect(GUIContent.none, GUIStyle.none,GUILayout.ExpandWidth(true));
                Rect verticalRect = new Rect(sectionRectThree.x, sectionRectThree.y, sectionRectThree.width, 400);
                
                EditorGUI.DrawRect(verticalRect, Color.green);
            
            EditorGUILayout.EndVertical();
            
        EditorGUILayout.EndHorizontal();
        
        EditorGUILayout.BeginVertical();

        EditorGUILayout.PropertyField(arraySizeProp);

        if (arraySizeProp.intValue < 0)
        {
            arraySizeProp.intValue = 0;
        }

        arrayItselfProp.arraySize = arraySizeProp.intValue;

        
        Rect squareRect = GUILayoutUtility.GetRect(0f, 0f);
        squareRect.x += EditorGUIUtility.labelWidth;
        squareRect.width = (MAX_RECTS_PER_ROW * RECT_SIZE) + ((MAX_RECTS_PER_ROW - 1) * RECT_SPACING) + SQUARE_PADDING * 2;
        squareRect.height = Mathf.CeilToInt((float)arrayItselfProp.arraySize / MAX_RECTS_PER_ROW) * (RECT_SIZE + RECT_SPACING) + SQUARE_PADDING * 2;

        EditorGUI.DrawRect(squareRect, squareColor);
        

        int numRects = arrayItselfProp.arraySize;
        int rectCount = 0;
        for (int row = 0; row < Mathf.CeilToInt((float)numRects / MAX_RECTS_PER_ROW); row++)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Space(squareRect.x + SQUARE_PADDING);
            for (int col = 0; col < MAX_RECTS_PER_ROW; col++)
            {
                if (rectCount < numRects)
                {
                    Rect rect = new Rect(squareRect.x + col * (RECT_SIZE + RECT_SPACING) + SQUARE_PADDING, squareRect.y + row * (RECT_SIZE + RECT_SPACING) + SQUARE_PADDING, RECT_SIZE, RECT_SIZE);
                    EditorGUI.DrawRect(rect, rectColor);
                }
                rectCount++;
            }
            GUILayout.EndHorizontal();
        }

        EditorGUILayout.EndVertical();

        serializedObject.ApplyModifiedProperties();
    }
}
