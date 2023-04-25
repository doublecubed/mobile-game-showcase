// ------------------------
// Onur Ereren - April 2023
// ------------------------

using UnityEngine;
using IslandGame.Settings;

namespace IslandGame.Gameplay
{

	public class CharacterSpawner : MonoBehaviour
	{
		#region REFERENCES

		[SerializeField] 
		private GameObject _characterPrefab;

		#endregion

		#region VARIABLES

		[SerializeField]
		private float _islandDimension;
		private float _characterInterval;
		private int[,] _puzzleState;

		[SerializeField] 
		private ColorSettings _colorSettings;
		
		#endregion

		#region MONOBEHAVIOUR

		#endregion

		#region METHODS

		public void SpawnCharacters(Island[] islands, int rowSize, int[,] puzzleState)
		{
			_characterInterval = _islandDimension / (float)rowSize;
			_puzzleState = puzzleState;
			
			for (int i = 0; i < islands.Length; i++)
			{
				SpawnIslandCharacters(islands[i], rowSize, i);
			}
			
		}

		private void SpawnIslandCharacters(Island island, int rowSize, int islandIndex)
		{
			bool reversed = island.FacingRight;

			Vector3 firstRowTopPosition = island.transform.position + Vector3.up * _islandDimension * 0.5f +
			                              Vector3.forward * (_islandDimension - _characterInterval) * 0.5f +
			                              Vector3.right * (reversed ? 1f : -1f) *
			                              (_islandDimension - _characterInterval) * 0.5f;
			                              
			                              
			for (int i = 0; i < rowSize; i++)
			{
				SpawnCharacterRow(island, firstRowTopPosition + (Vector3.right * (reversed ? -1f : 1f) * _characterInterval * i), island.transform, (reversed ? -1 : 1) * Vector3.right, rowSize, islandIndex, i);
			}
		}
		
		private void SpawnCharacterRow(Island island, Vector3 topPosition, Transform parent, Vector3 facing, int rowSize, int islandIndex, int rowIndex)
		{
			int rowColorIndex = _puzzleState[islandIndex, rowIndex];
			
			if (rowColorIndex == 0) return;

			Color rowColor = _colorSettings.colors[rowColorIndex - 1];
			
			for (int i = 0; i < rowSize; i++)
			{

				Transform characterTransform = SpawnCharacter(topPosition + Vector3.back * _characterInterval * i, parent, facing, rowColor);
				island.IslandGrid.SetOccupant(rowIndex, i, characterTransform);
			}
		}
		
		private Transform SpawnCharacter(Vector3 position, Transform parent, Vector3 facing, Color color)
		{
			GameObject character = Instantiate(_characterPrefab, position, Quaternion.LookRotation(facing * 90f), parent);
			character.GetComponent<Character>().Renderer.material.color = color;
			return character.transform;
		}

		
		
		#endregion

	}
}