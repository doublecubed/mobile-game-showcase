// ------------------------
// Onur Ereren - April 2023
// ------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IslandGame.PuzzleEngine
{

	public static class PuzzleUtilites
	{
		#region METHODS

		public static int?[,] ResizeArray(int?[,] originalArray, int numberOfRows, int numberOfColumns)
		{
			int?[,] resultArray = new int?[numberOfRows, numberOfColumns];

			for (int i = 0; i < numberOfRows; i++)
			{
				for (int j = 0; j < numberOfColumns; j++)
				{
					if (i >= originalArray.GetLength(0) || j >= originalArray.GetLength(1))
					{
						resultArray[i, j] = null;
					}
					else
					{
						resultArray[i, j] = originalArray[i, j];
					}
				}
			}

			return resultArray;
		}
		
		#endregion

	}
}