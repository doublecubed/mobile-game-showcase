// ------------------------
// Onur Ereren - April 2023
// ------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IslandGame.PuzzleEngine;

namespace IslandGame.Gameplay
{
	public class CommandResolver : MonoBehaviour
	{
		#region REFERENCES

		private IslandMover _islandMover;
		[SerializeField]
		private PuzzleSolver _puzzleSolver;
		
		#endregion

		#region VARIABLES

		private bool _commandStarted;
		private int _firstIsland;
		private int _secondIsland;
		
		#endregion

		#region MONOBEHAVIOUR

		private void Start()
		{
			_islandMover = GetComponent<IslandMover>();
			
		}

		#endregion

		#region METHODS

		public void IslandTapped(int index)
		{
			if (!_commandStarted)
			{
				_commandStarted = true;
				_firstIsland = index;
				_islandMover.MoveIsland(index);
			}
			else
			{
				if (index == _firstIsland)
				{
					_commandStarted = false;
					_islandMover.MoveIsland(index);
				}
				else
				{
					_secondIsland = index;
					RegisterCommand(_firstIsland, _secondIsland);
				}
			}
		}

		private void RegisterCommand(int firstIsland, int secondIsland)
		{
			_puzzleSolver.RegisterCommand(firstIsland, secondIsland, out PuzzleCommand commandResponse);
			if (commandResponse != null)
			{
				_islandMover.MoveIsland(secondIsland);
			}
			else
			{
				_islandMover.MoveIsland(firstIsland);
			}

			_commandStarted = false;
		}
		
		#endregion

	}
	
}