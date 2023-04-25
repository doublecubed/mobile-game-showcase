// ------------------------
// Onur Ereren - April 2023
// ------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IslandGame.PuzzleEngine
{

	public class PuzzleCommand
	{
		public readonly int ExitingNode;
		public readonly int DestinationNode;
		public readonly int RowsTransferrable;
		public readonly int ExitingNodeFirstExitRow;
		public readonly int DestinationNodeFirstRecievingRow;

		public PuzzleCommand(int exiting, int destination, int rowsTransferrable, int firstExitingRow, int firstReceivingRow)
		{
			ExitingNode = exiting;
			DestinationNode = destination;
			RowsTransferrable = rowsTransferrable;
			ExitingNodeFirstExitRow = firstExitingRow;
			DestinationNodeFirstRecievingRow = firstReceivingRow;
		}

	}
}