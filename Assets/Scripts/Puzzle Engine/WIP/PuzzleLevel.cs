// ------------------------
// Onur Ereren - April 2023
// ------------------------

using UnityEngine;

namespace IslandGame.PuzzleEngine
{
	[CreateAssetMenu(fileName = "New Puzzle Level", menuName = "Island Game/New Level")]
	public class PuzzleLevel : ScriptableObject
	{
		public int _rowSize; 
		public int _numberOfNodes;
		[SerializeField] private int _numberOfLockedNodes;

		public int?[,] _rowConfiguration = new int?[1, 1];

		public int?[,] ResizeLevel(int numberOfNodes, int rowSize)
		{
			int?[,] previousArray = _rowConfiguration;

			int?[,] resultArray = new int?[numberOfNodes, rowSize];

			for (int i = 0; i < numberOfNodes; i++)
			{
				for (int j = 0; j < rowSize; j++)
				{
					if (i >= previousArray.GetLength(0) || j >= previousArray.GetLength(1))
					{
						resultArray[i, j] = null;
					}
					else
					{
						resultArray[i, j] = previousArray[i, j];
					}
				}
			}

			return resultArray;
		}

	}
}