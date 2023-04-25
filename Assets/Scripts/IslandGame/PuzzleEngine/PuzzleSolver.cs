// ------------------------
// Onur Ereren - April 2023
// ------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using IslandGame.DI;

namespace IslandGame.PuzzleEngine
{

	public class PuzzleSolver : MonoBehaviour
	{
		#region REFERENCES

		private ILevelReader _levelReader;
		
		#endregion

		#region VARIABLES

		private int[,] _puzzleState;
		private int _levelNumber;
		private bool[] _lockedNodes;
		private int _rowsPerNode;
		private int _numberOfNodes;
		private bool _puzzleLoaded;
		
		private Queue<PuzzleCommand> _commandQueue;
		private Stack<PuzzleCommand> _commandLog;
		private Dictionary<PuzzleCommand, int[,]> _stateLog;	// each entry in the dictionary shows the state "before" that command, making undo possible
		
		#endregion
		
		#region MONOBEHAVIOUR

		private void Awake()
		{
			var injectionContainer = FindObjectOfType<DIContainer>();
			_levelReader = injectionContainer.Resolve<ILevelReader>();
			
		}

		private void Update()
		{
			if (_puzzleLoaded)
			{
				HandleCommandExecution();
			}
		}
		
		#endregion

		#region PUZZLE SETUP

		private void SetPuzzleParameters()
		{
			_numberOfNodes = _puzzleState.GetLength(0);
			_rowsPerNode = _puzzleState.GetLength(1);
			
		}
		
		private void GetLastFinishedLevelNumber()
		{
			_levelNumber = PlayerPrefs.GetInt("levelNumber", 0);
		}

		private void ReadLevel(int levelNumber)
		{
			_levelReader.ReadLevel(levelNumber);
		}

		private void SetPuzzleState()
		{
			_puzzleState = _levelReader.GetLevelConfiguration();
		}

		private void ResetCommandCollections()
		{
			_commandQueue = new Queue<PuzzleCommand>();
			_commandLog = new Stack<PuzzleCommand>();
			_stateLog = new Dictionary<PuzzleCommand, int[,]>();
		}
		
		private void ResetPuzzle()
		{
			GetLastFinishedLevelNumber();
			ReadLevel(_levelNumber);
			SetPuzzleState();
			SetPuzzleParameters();
			LogPuzzleState();
			ResetCommandCollections();
		}

		public void LoadPuzzle(int levelNumber)
		{
			ReadLevel(levelNumber);
			SetPuzzleState();
			SetPuzzleParameters();
			LogPuzzleState();
			ResetCommandCollections();
			_puzzleLoaded = true;
		}
		
		
		#endregion

		#region COMMAND HANDLING

		public void UndoLastCommand()
		{
			if (_commandLog.Count <= 0)
			{
				Debug.Log("command log is empty");
				return;
			}
			
			PuzzleCommand commandToUndo = _commandLog.Pop();
			int[,] stateToReturnTo = _stateLog[commandToUndo];
			_puzzleState = stateToReturnTo;
			LogPuzzleState();
		}
		
		private void HandleCommandExecution()
		{
			if (_commandQueue.Count > 0)
			{
				PuzzleCommand currentCommand = _commandQueue.Dequeue();
				UpdateStatusLog(currentCommand);
				ExecuteCommand(currentCommand);
				LogCommand(currentCommand);
			}
		}
		
		public void RegisterCommand(int exiting, int destination, out PuzzleCommand nextCommand)
		{
			
			// Check if the node numbers are out of range. (Not possible except for testing)
			if (exiting < 0 || exiting > _numberOfNodes || destination < 0 ||
			    destination > _numberOfNodes)
			{
				Debug.LogError("Requested exit/destination node doesn't exist");
				nextCommand = null;
				return;
			}
			
			// Check if rows can be transferred.
			if (!TransferIsPossible(exiting, destination))
			{
				nextCommand = null;
				return;
			}

			
			// Check how many rows can be transferred
			int transferableRows = NumberOfRowsTransferable(exiting);
			int receivableRows = NumberOfEmptyRows(destination);
			int finalTransferRowAmount = Math.Min(transferableRows, receivableRows);
			
			// Exiting node first exiting row
			int exitingEmptySpaces = NumberOfEmptyRows(exiting);
			int exitingNodeFirstExitingRow = (_rowsPerNode - 1)- exitingEmptySpaces;
			
			// Recieving node first empty row to receive;
			int destinationEmptySpaces = NumberOfEmptyRows(destination);
			int destinationNodeFirstReceivingRow = _rowsPerNode - destinationEmptySpaces;
			
			
			nextCommand = new PuzzleCommand(exiting, destination, finalTransferRowAmount, exitingNodeFirstExitingRow, destinationNodeFirstReceivingRow);
			QueueCommand(nextCommand);

		}
		
		private void ExecuteCommand(PuzzleCommand command)
		{
			int exitingIndex = command.ExitingNode;
			int destinationIndex = command.DestinationNode;
			
			// Check how many rows can be transferred
			//int transferableRows = NumberOfRowsTransferable(exitingIndex);
			//int receivableRows = NumberOfEmptyRows(destinationIndex);

			//int finalTransferRowAmount = Math.Min(transferableRows, receivableRows);
			
			int transferValue = GetNodeFinalRowValue(exitingIndex);
			
			// Set the destination rows to value
			int transferredRows = 0;
			for (int i = 0; i < _rowsPerNode; i++)
			{
				if (_puzzleState[destinationIndex, i] == 0)
				{
					_puzzleState[destinationIndex, i] = transferValue;
					transferredRows++;

					if (transferredRows >= command.RowsTransferrable) break;
				}
			}
			
			// Set the exiting rows to 0
			int emptiedRows = 0;
			for (int i = 0; i < _rowsPerNode; i++)
			{
				if (_puzzleState[exitingIndex, ReverseIndex(i)] == transferValue)
				{
					_puzzleState[exitingIndex, ReverseIndex(i)] = 0;
					emptiedRows++;
					if (emptiedRows >= command.RowsTransferrable) break;
				}
			}

			
			LogPuzzleState();
		}
		
		public void QueueCommand(PuzzleCommand command)
		{
			_commandQueue.Enqueue(command);
		}

		public void LogCommand(PuzzleCommand command)
		{
			_commandLog.Push(command);
		}

		public void UpdateStatusLog(PuzzleCommand command)
		{
			//int[,] stateToLog = CopyPuzzleState();
			int[,] stateToLog = new int[_numberOfNodes, _rowsPerNode];
			Buffer.BlockCopy(_puzzleState, 0, stateToLog, 0, (_numberOfNodes * _rowsPerNode) * sizeof(int));
			_stateLog.Add(command, stateToLog);
		}
		
		#endregion
		
		#region PUZZLE STATE HANDLING

		/*
		private int[,] CopyPuzzleState()
		{
			int row = _puzzleState.GetLength(0);
			int column = _puzzleState.GetLength(1);
			
			int[,] cloneState = new int[row, column];
			for (int i = 0; i < row; i++)
			{
				for (int j = 0; j < column; j++)
				{
					cloneState[i, j] = _puzzleState[i, j];
				}
			}

			return cloneState;
		}
		*/


		public bool NodeIsCompleted(int nodeIndex)
		{
			return NumberOfRowsTransferable(nodeIndex) == _rowsPerNode;
		}

		public bool PuzzleIsCompleted()
		{
			bool result = true;

			for (int i = 0; i < _numberOfNodes; i++)
			{
				if (NodeIsEmpty(i) || NodeIsCompleted(i))
				{
					continue;
				}

				result = false;
			}

			return result;
		}
		
		private int NumberOfRowsTransferable(int nodeIndex)
		{
			int valueToSearch = GetNodeFinalRowValue(nodeIndex);
			
			int transferrableNodes = 0;
			for (int i = 0; i < _rowsPerNode; i++)
			{
				int rowValue = _puzzleState[nodeIndex, ReverseIndex(i)];
				if (rowValue == 0) continue;
				if (rowValue == valueToSearch)
				{
					transferrableNodes++;
				}
				else
				{
					break;
				}
			}


			return transferrableNodes;
		}

		
		private int NumberOfEmptyRows(int nodeIndex)
		{
			int emptyRows = 0;
			for (int i = 0; i < _rowsPerNode; i++)
			{
				
				if (_puzzleState[nodeIndex, ReverseIndex(i)] != 0) break;
				emptyRows++;
			}

			return emptyRows;
		}
		
		private bool TransferIsPossible(int exiting, int destination)
		{
			if (NodeIsFull(destination)) return false;
			if (NodeIsEmpty(exiting)) return false;

			if (GetNodeFinalRowValue(destination) != 0 && GetNodeFinalRowValue(exiting) != GetNodeFinalRowValue(destination)) return false;
			
			return true;
		}
		
		private bool NodeIsFull(int nodeIndex)
		{
			return _puzzleState[nodeIndex, _rowsPerNode - 1] != 0;
		}

		private bool NodeIsEmpty(int nodeIndex)
		{
			return GetNodeFinalRowValue(nodeIndex) == 0;
		}
		
		private int GetNodeFinalRowValue(int nodeIndex)	// Obviously, gets the first non-empty (non-zero) value, starting from the end. Returns zero if the entire node is empty
		{
			int returnValue = 0;

			for (int i = 0; i < _rowsPerNode; i++)
			{
				int checkValue = _puzzleState[nodeIndex, ReverseIndex(i)]; 
				
				if (checkValue != 0)
				{
					returnValue = checkValue;
					break;
				}
			}

			return returnValue;
		}

		private int ReverseIndex(int index)
		{
			return (_rowsPerNode - 1) - index;
		}

		public int[,] GetPuzzleState()
		{
			return _puzzleState;
		}
		
		#endregion
		
		#region TESTING
		
		private void LogPuzzleState()
		{
			if (_puzzleState == null)
			{
				Debug.Log("Puzzle State is null");
				return;
			}
			
			for (int i = 0; i < _puzzleState.GetLength(0); i++)
			{
				string lineString = new string("");
				
				for (int j = 0; j < _puzzleState.GetLength(1); j++)
				{
					lineString += (_puzzleState[i, j] + " ");
				}
				
				Debug.Log("line " + i + ": " + lineString);
			}
		}
		
		#endregion
	}
}