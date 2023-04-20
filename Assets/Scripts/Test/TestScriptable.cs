// ------------------------
// Onur Ereren - April 2023
// ------------------------

using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level", menuName = "Island Game/Create Level")]
public class TestScriptable : ScriptableObject
{
	public int rowCount;

	public int[] rowNumbers;

	private void OnValidate()
	{
		if (rowNumbers == null || rowNumbers.Length != rowCount)
		{
			System.Array.Resize(ref rowNumbers, rowCount);
		}
	}
}
