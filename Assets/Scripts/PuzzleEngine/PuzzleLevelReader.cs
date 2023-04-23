// ------------------------
// Onur Ereren - April 2023
// ------------------------

// In the provided txt level format, the first line is always full of zeros. This line is used to denote how many rows the level has. The reader will only use the Length of the first line, and then discard it.

using System;
using System.IO;
using UnityEngine;

namespace IslandGame.PuzzleEngine
{

	public class PuzzleLevelReader : ILevelReader
	{
		#region REFERENCES

		private const string FILE_DIRECTORY = "Assets/Levels";
		private const int NUMBER_OF_LEVEL_FILES = 10; // Number of levels is constrained to 10 for this iteration;
		private string _levelContent;
		private string[] _lineContents;
		private bool[] _lockedNodes;
		private int[,] _nodeContent;
		private int _numberOfRows;

		
		#endregion

		#region VARIABLES

		
		
		#endregion

		#region MONOBEHAVIOUR

		#endregion

		#region METHODS

		public void ReadLevel(int levelNo)
		{
			int levelNoModulus = levelNo % NUMBER_OF_LEVEL_FILES; 
			
			ReadLevelFile(levelNoModulus);
			ParseLevelContent();
		}

		public int[,] GetLevelConfiguration()
		{
			return _nodeContent;
		}

		private string FullLevelPath(int levelNo)
		{
			return FILE_DIRECTORY + "/level" + levelNo + ".txt";
		}

		private void ReadLevelFile(int levelNo)
		{
			string levelPath = FullLevelPath(levelNo);
			_lineContents = File.ReadAllLines(levelPath);
		}

		private void ParseLevelContent()
		{
			//LogStringContent(_lineContents);
			
			_numberOfRows = _lineContents[0].Length;
			_nodeContent = new int[_lineContents.Length - 1, _numberOfRows];
			_lockedNodes = new bool[_lineContents.Length - 1];
			
			for (int i = 1; i < _lineContents.Length; i++)
			{
				string singleLineContent = _lineContents[i];
				_lockedNodes[i - 1] = (_lineContents[i].Length > _numberOfRows);
				
				for (int j = 0; j < _numberOfRows; j++)
				{
					string numberContent = singleLineContent[j].ToString();
					_nodeContent[i - 1, j] = Convert.ToInt32(numberContent);
				}
			}
		}
		
		
		
		#endregion

		#region TESTING

		private void LogStringContent(string[] content)
		{
			for (int i = 0; i < content.Length; i++)
			{
				Debug.Log(i + ": " + content[i]);
			}
		}
		
		
		#endregion
		
		
	}
}