// ------------------------
// Onur Ereren - April 2023
// ------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IslandGame.Gameplay
{
	public class CommandResolver : MonoBehaviour
	{
		#region REFERENCES

		private IslandMover _islandMover;
		
		#endregion

		#region VARIABLES

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
			
		}
		
		#endregion

	}
}