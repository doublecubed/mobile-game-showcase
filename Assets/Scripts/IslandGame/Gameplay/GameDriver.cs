// ------------------------
// Onur Ereren - April 2023
// ------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IslandGame.PuzzleEngine;

namespace IslandGame.Gameplay
{
	public class GameDriver : MonoBehaviour
	{
		#region REFERENCES

		[SerializeField] 
		private PuzzleSolver _puzzleSolver;

		#endregion

		#region VARIABLES

		#endregion

		#region MONOBEHAVIOUR

		#endregion

		#region METHODS

		public void StartLevel()
		{
			SetupLevel();
		}
		
		private void SetupLevel()
		{
			int levelNumber = PlayerPrefs.GetInt("levelNumber", 0);
			_puzzleSolver.LoadPuzzle(levelNumber);
		}
		
		#endregion

	}
}