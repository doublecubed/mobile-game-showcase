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

		private Transform[,] _gridOccupants;

		#endregion

		#region CONSTRUCTOR

		public IslandGrid(int rows, int columns)
		{
			_gridOccupants = new Transform[rows, columns];
		}
		
		#endregion

		#region METHODS

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