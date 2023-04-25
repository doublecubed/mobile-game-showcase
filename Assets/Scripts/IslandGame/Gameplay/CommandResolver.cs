// ------------------------
// Onur Ereren - April 2023
// ------------------------

// CommandResolver is the bridge between the Gameplay and PuzzleEngine implementations.
// It interacts with an ISolverAccess interface to register commands to the Puzzle Engine.

using UnityEngine;
using IslandGame.PuzzleEngine;
namespace IslandGame.Gameplay
{
	public class CommandResolver : MonoBehaviour
	{
		#region REFERENCES

		private GameDriver _gameDriver;
		private IslandMover _islandMover;
		[SerializeField]
		private PuzzleSolver _puzzleSolver;
		
		#endregion

		#region VARIABLES

		public bool ReceiveInput { get; set; }
		
		private bool _commandStarted;
		private int _firstIsland;
		private int _secondIsland;

		private float _islandDimension = 2f;
		private int _numberOfRows = 4;
		
		#endregion

		#region MONOBEHAVIOUR

		private void Start()
		{
			_islandMover = GetComponent<IslandMover>();
			_gameDriver = GetComponent<GameDriver>();
		}

		#endregion

		#region METHODS


	
		public void IslandTapped(int index)
		{
			if (!ReceiveInput) return;
			
			if (!_commandStarted && _puzzleSolver.GetPuzzleState()[index,0] != 0)
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
				//_islandMover.MoveIsland(secondIsland);
				int numberOfRowsMoving = commandResponse.RowsTransferrable;
				int exitingFirstExitRow = commandResponse.ExitingNodeFirstExitRow;
				int destinationFirstReceivingRow = commandResponse.DestinationNodeFirstRecievingRow;

				for (int i = 0; i < numberOfRowsMoving; i++)
				{
					MoveRow(firstIsland, secondIsland, exitingFirstExitRow - i, destinationFirstReceivingRow + i, _gameDriver.IslandScripts[secondIsland].FaceDirection, _islandDimension, _numberOfRows);
				}
				
				_islandMover.MoveIsland(firstIsland);
				//_islandMover.MoveIsland(secondIsland);
				_commandStarted = false;
			}
			else
			{
				_islandMover.MoveIsland(firstIsland);
				_commandStarted = false;
			}

			if (_puzzleSolver.PuzzleIsCompleted())
			{
				_gameDriver.PuzzleCompleted();
				ReceiveInput = false;
			}
			_commandStarted = false;
		}

		private void MoveRow(int islandFrom, int islandTo, int rowFrom, int rowTo, Vector3 targetFacing, float islandDimension, int numberOfRows)
		{
			Debug.Log("move row running");
			
			Transform[,] exitingOccupants = _gameDriver.IslandScripts[islandFrom].IslandGrid.GetOccupantGrid();
			Transform[,] enteringOccupants = _gameDriver.IslandScripts[islandTo].IslandGrid.GetOccupantGrid();
			
			for (int i = 0; i < exitingOccupants.GetLength(1); i++)
			{
				exitingOccupants[rowFrom, i].parent = _gameDriver.IslandScripts[islandTo].transform;
				
				exitingOccupants[rowFrom, i].position = TargetPosition(_gameDriver.IslandScripts[islandTo].transform,
					rowTo, i, targetFacing, islandDimension, numberOfRows);
				exitingOccupants[rowFrom, i].rotation = Quaternion.LookRotation(targetFacing);

				enteringOccupants[rowTo, i] = exitingOccupants[rowFrom, i];
				exitingOccupants[rowFrom, i] = null;
			}
			

		}

		private Vector3 TargetPosition(Transform islandTo, int row, int position, Vector3 targetFacing, float islandDimension, int numberOfRows)
		{
			float characterInterval = islandDimension / numberOfRows;

			Vector3 basePosition = islandTo.position + Vector3.up * islandDimension * 0.5f;
			Vector3 xAddedPosition = basePosition + -targetFacing * (0.5f * characterInterval) * (numberOfRows - 1) +
			                         targetFacing * characterInterval * row;
			Vector3 zAddedPosition = xAddedPosition +
			                         Vector3.forward * (0.5f * characterInterval) * (numberOfRows - 1) +
			                         Vector3.back * characterInterval * position;
			return zAddedPosition;
		}
		
		#endregion

	}
	
}