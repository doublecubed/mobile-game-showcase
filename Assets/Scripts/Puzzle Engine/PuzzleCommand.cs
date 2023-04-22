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

		public PuzzleCommand(int exiting, int destination)
		{
			ExitingNode = exiting;
			DestinationNode = destination;
		}

	}
}