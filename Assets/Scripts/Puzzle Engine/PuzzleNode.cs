// ------------------------
// Onur Ereren - April 2023
// ------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IslandGame.PuzzleEngine
{
	public class PuzzleNode
	{
		#region REFERENCES

		#endregion

		#region VARIABLES

		private int _numberOfRows;
		private int?[] _rowContent;

		#endregion

		#region CONSTRUCTOR

		public PuzzleNode(int numberOfRows)
		{
			_numberOfRows = numberOfRows;
			_rowContent = new int?[numberOfRows];
		}
		
		#endregion

		#region METHODS

		public void SetRowValue(int index, int? value)
		{
			_rowContent[index] = value;
		}

		public int? GetRowValue(int index)
		{
			return _rowContent[index];
		}

		public bool AllRowsEmpty()
		{
			return (NumberOfEmptyRows() == _numberOfRows);
		}

		public int? FirstEmptyRow()
		{
			int firstEmptyRow = _numberOfRows - NumberOfEmptyRows();
			if (firstEmptyRow == _numberOfRows) return null;
			return firstEmptyRow;
		}
		public int NumberOfEmptyRows()
		{
			int emptyRowCount = 0;
			for (int i = 0; i < _rowContent.Length; i++)
			{
				int sortedIndex = _rowContent.Length - ( i + 1 );
				if (_rowContent[sortedIndex] == null)
				{
					emptyRowCount++;
				}
				else
				{
					break;
				}
			}

			return emptyRowCount;
		}
		
		
		#endregion

	}
}