// ------------------------
// Onur Ereren - April 2023
// ------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IslandGame.Gameplay
{

	public class IslandGrid
	{
		#region VARIABLES

		public Vector3[,] GridPositions { get; set; }
		private Transform[,] _gridOccupants;

		#endregion

		#region CONSTRUCTOR

		public IslandGrid(int rows, int columns, float islandDimension, Vector3 facing)
		{
			_gridOccupants = new Transform[rows, columns];
			GridPositions = new Vector3[rows, columns];
			
			//GenerateGridPositions(rows, columns,islandDimension, facing);
		}
		
		#endregion

		#region METHODS

		private void GenerateGridPositions(int rows, int columns, float islandDimension, Vector3 facing)
		{
			float characterInterval = islandDimension / (float)columns;
			
			for (int i = 0; i < columns; i++) // "rows" are vertical, so they are the inner iteration
			{
				for (int j = 0; j < rows; j++)
				{
					GridPositions[i, j] = Vector3.up * islandDimension * 0.5f;
					GridPositions[i, j] += -facing * (0.5f * characterInterval) * (columns - 1) +
					                       facing * characterInterval * i;
					GridPositions[i, j] += Vector3.forward * (0.5f * characterInterval) * (rows - 1) +
					                       Vector3.back * characterInterval * j;
				}
					
			}		
		}
		
		public Transform[,] GetOccupantGrid()
		{
			return _gridOccupants;
		}
		
		public void SetOccupant(int row, int column, Transform newOccupant)
		{
			_gridOccupants[row, column] = newOccupant;
		}

		public void FlushOccupant(int row, int column)
		{
			_gridOccupants[row, column] = null;
		}
		
		public Transform GetOccupant(int row, int column)
		{
			return _gridOccupants[row, column];
		}
		
		#endregion

	}
}