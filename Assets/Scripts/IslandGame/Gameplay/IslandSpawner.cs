// ------------------------
// Onur Ereren - April 2023
// ------------------------

// This script spawns the islands and characters into play, but also assigns them to the appropriate places on the island matrices

using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;


namespace IslandGame.Gameplay
{
	public class IslandSpawner : MonoBehaviour
	{
		#region REFERENCES

		[SerializeField] 
		private GameObject _islandPrefab;

		#endregion

		#region VARIABLES

		[SerializeField] 
		private float _islandDimension;
		
		[SerializeField] 
		private float _islandSpawnZInterval;

		[SerializeField] 
		private float _islandPlacementXPosition;

		private int _numberOfIslands;
		private int _numberOfRows;

		private Island[] _islands;
		
		#endregion

		#region MONOBEHAVIOUR

		#endregion

		#region METHODS

	
		public void SpawnIslands(int numberOfIslands, int numberOfRows, CommandResolver resolver, out Island[] islands)
		{
			_islands = new Island[numberOfIslands];
			
			int numberOfIslandsOnLeft = (numberOfIslands / 2) + (numberOfIslands % 2);
			int numberOfIslandsOnRight = (numberOfIslands / 2);
			int islandNumberIndex = 0;
			
			Vector3 leftStartPoint = new Vector3(-_islandPlacementXPosition, 0f, (float)numberOfIslandsOnLeft * _islandSpawnZInterval * 0.5f);
			for (int i = 0; i < numberOfIslandsOnLeft; i++)
			{
				_islands[islandNumberIndex] = SpawnIsland((leftStartPoint + i * Vector3.back * _islandSpawnZInterval), islandNumberIndex, numberOfRows, resolver, Vector3.right);
				islandNumberIndex++;
			}

			Vector3 rightStartPoint = new Vector3(_islandPlacementXPosition, 0f, leftStartPoint.z);
			for (int i = 0; i < numberOfIslandsOnRight; i++)
			{
				_islands[islandNumberIndex] = SpawnIsland(rightStartPoint + i * Vector3.back * _islandSpawnZInterval, islandNumberIndex, numberOfRows, resolver, Vector3.left);
				islandNumberIndex++;
			}

			islands = _islands;
		}

		private Island SpawnIsland(Vector3 position, int islandIndex, int numberOfRows, CommandResolver resolver, Vector3 faceDirection)
		{
			GameObject island = Instantiate(_islandPrefab, position, Quaternion.identity);
			Island islandScript = island.GetComponent<Island>();
			islandScript.Index = islandIndex;
			islandScript.Resolver = resolver; 
			islandScript.SetupIslandGrid(numberOfRows, numberOfRows, _islandDimension, faceDirection);
			islandScript.FaceDirection = faceDirection;
			return islandScript;
		}

		public Island[] GetIslands()
		{
			return _islands;
		}
		
		#endregion

	}
}