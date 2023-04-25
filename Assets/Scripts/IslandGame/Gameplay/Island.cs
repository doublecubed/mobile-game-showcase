// ------------------------
// Onur Ereren - April 2023
// ------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IslandGame.Gameplay
{

	public class Island : MonoBehaviour
	{
		#region REFERENCES

		private CommandResolver _resolver;
		public CommandResolver Resolver
		{
			get { return _resolver; }
			set { _resolver = value; }
		}

		public IslandGrid IslandGrid { get; set; }
		
		#endregion
	
		#region VARIABLES

		[SerializeField]
		private int _index;
		public int Index	// for checking in the inspector. Properties are not serialized and thus are not visible in Unity inspector
		{
			get { return _index; }
			set { _index = value; }
		}
		
		public bool FacingRight { get; set; }
		
		#endregion
	
		#region MONOBEHAVIOUR
		

		private void OnMouseDown()
		{
			Resolver.IslandTapped(_index);
		}
		
		#endregion
	
		#region METHODS

		public void SetupIslandGrid(int rows, int columns, float islandDimension, Vector3 facing)
		{
			IslandGrid = new IslandGrid(rows, columns, islandDimension, facing);
		}
		
		
		#endregion

	}	
}

