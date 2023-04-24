// ------------------------
// Onur Ereren - April 2023
// ------------------------

using UnityEngine;
using IslandGame.PuzzleEngine;
using IslandGame.UI;
using IslandGame.Settings;

namespace IslandGame.Gameplay
{
	public class GameDriver : MonoBehaviour
	{
		#region REFERENCES

		[SerializeField] 
		private PuzzleSolver _puzzleSolver;

		[SerializeField] 
		private UIDriver _uiDriver;

		private CommandResolver _commandResolver;

		[SerializeField] 
		private ColorSettings _colorSettings;

		private IslandSpawner _islandSpawner;
		private CharacterSpawner _characterSpawner;
		#endregion

		#region VARIABLES

		private bool _gameStarted;

		[SerializeField] 
		private GameObject _islandPrefab;

		[SerializeField] 
		private GameObject _chibiPrefab;
		
		[SerializeField] 
		private float _islandSpawnZInterval;
		[SerializeField] 
		private float _islandPlacementXPosition;

		[SerializeField] 
		private float _islandDimension;

		private Transform[] _islands;
		private Island[] _islandScripts;
		
		private int[,] _puzzleState;
		
		#endregion

		#region MONOBEHAVIOUR

		private void Start()
		{
			_commandResolver = GetComponent<CommandResolver>();
			_islandSpawner = GetComponent<IslandSpawner>();
			_characterSpawner = GetComponent<CharacterSpawner>();
		}
		
		
		#endregion

		#region METHODS

		public void StartLevel()
		{
			SetupLevel();
			RemoveMenuPanel();
			_gameStarted = true;
		}

		private void RemoveMenuPanel()
		{
			_uiDriver.RemoveMenuPanel();
		}
		
		private void SetupLevel()
		{
			int levelNumber = PlayerPrefs.GetInt("levelNumber", 0);
			_puzzleSolver.LoadPuzzle(levelNumber);
			_puzzleState = _puzzleSolver.GetPuzzleState();
			int numberOfIslands = _puzzleState.GetLength(0);
			int numberOfRows = _puzzleState.GetLength(1);
			//SpawnIslands(numberOfIslands);
			_islandSpawner.SpawnIslands(numberOfIslands, numberOfRows, _commandResolver, out Island[] islands);
			_islandScripts = islands;
			_characterSpawner.SpawnCharacters(_islandScripts, numberOfRows, _islandDimension, _puzzleState);
		}

		private void SpawnIslands(int numberOfIslands)
		{
			_islands = new Transform[numberOfIslands];
			
			int numberOfIslandsOnLeft = (numberOfIslands / 2) + (numberOfIslands % 2);
			int numberOfIslandsOnRight = (numberOfIslands / 2);

			Vector3 leftStartPoint = new Vector3(-_islandPlacementXPosition, 0f, (float)numberOfIslandsOnLeft * _islandSpawnZInterval * 0.5f);

			int islandNumberIndex = 0;
			for (int i = 0; i < numberOfIslandsOnLeft; i++)
			{
				GameObject island = Instantiate(_islandPrefab,
					leftStartPoint + i * Vector3.back * _islandSpawnZInterval, Quaternion.identity);
				island.GetComponent<Island>().Index = islandNumberIndex;
				island.GetComponent<Island>().Resolver = _commandResolver;
				_islands[islandNumberIndex] = island.transform;

				float characterInterval = _islandDimension / (float)_puzzleState.GetLength(1);
				
				Vector3 firstRowTopPosition = island.transform.position + Vector3.up * _islandDimension * 0.5f + (Vector3.right * -1f + Vector3.forward) * (_islandDimension * 0.5f - characterInterval * 0.5f);
				
				for (int j = 0; j < _puzzleState.GetLength(1); j++)
				{
					if (_puzzleState[islandNumberIndex, j] != 0)
					{
						SpawnRow(islandNumberIndex, j, firstRowTopPosition + Vector3.right * characterInterval * j, _puzzleState.GetLength(1), true);						
					}

				}
				
				//SpawnChibis(island.transform, islandNumberIndex, true);
				islandNumberIndex++;
			}

			Vector3 rightStartPoint = new Vector3(_islandPlacementXPosition, 0f, leftStartPoint.z);
			for (int i = 0; i < numberOfIslandsOnRight; i++)
			{
				GameObject island = Instantiate(_islandPrefab,
					rightStartPoint + i * Vector3.back * _islandSpawnZInterval, Quaternion.identity);
				island.GetComponent<Island>().Index = islandNumberIndex;
				island.GetComponent<Island>().Resolver = _commandResolver;
				_islands[islandNumberIndex] = island.transform;
				
				float characterInterval = _islandDimension / (float)_puzzleState.GetLength(1);
				
				Vector3 firstRowTopPosition = island.transform.position + Vector3.up * _islandDimension * 0.5f + (Vector3.right * 1f + Vector3.forward) * (_islandDimension * 0.5f - characterInterval * 0.5f);
				
				for (int j = 0; j < _puzzleState.GetLength(1); j++)
				{
					if (_puzzleState[islandNumberIndex, j] != 0)
					{
						SpawnRow(islandNumberIndex, j, firstRowTopPosition + -Vector3.right * j, _puzzleState.GetLength(1), true);						
					}

				}
				
				//SpawnChibis(island.transform, islandNumberIndex, false);
				islandNumberIndex++;				
			}
			
		}

		private void SpawnRow(int islandIndex, int rowIndex, Vector3 rowPosition, int numberOfCharacters, bool leftIsland)
		{
			float characterInterval = _islandDimension / (float)numberOfCharacters;
			
			for (int i = 0; i < numberOfCharacters; i++)
			{
				Vector3 position = rowPosition + Vector3.back * i * characterInterval;
				GameObject stickman = Instantiate(_chibiPrefab, position,
					Quaternion.Euler(0f, (leftIsland ? 1f : -1f) * 90f, 0f));
				int stickmanColorIndex = _puzzleState[islandIndex, rowIndex] - 1;			// 0 is empty in puzzle state, so in puzzle state colors start from 1. But in ColorSettings they start from 0.
				Color stickmanColor = _colorSettings.colors[stickmanColorIndex];
				stickman.GetComponent<Character>().SetColor(stickmanColor);
			}
		}
		
		private void SpawnChibis(Transform island, int islandIndex, bool leftIsland)
		{
			int[,] puzzleState = _puzzleSolver.GetPuzzleState();
			int numberOfRows = puzzleState.GetLength(1);
			float chibiInterval = _islandDimension / (float)numberOfRows;

			Vector3 firstRowTopPosition = island.position + Vector3.up * _islandDimension * 0.5f + (Vector3.right * (leftIsland ? -1f : 1f) + Vector3.forward) * (_islandDimension * 0.5f - chibiInterval * 0.5f);

			for (int i = 0; i < numberOfRows; i++)
			{
				for (int j = 0; j < numberOfRows; j++)
				{
					Vector3 position = firstRowTopPosition + Vector3.back * j * chibiInterval +
					                   Vector3.right * (leftIsland ? 1f : -1f) * chibiInterval * i;
					GameObject stickman = Instantiate(_chibiPrefab, position,
						Quaternion.Euler(0f, (leftIsland ? 1f : -1f) * 90f, 0f));
					int stickmanColorIndex = _puzzleState[islandIndex, i] - 1;			// 0 is empty in puzzle state, so in puzzle state colors start from 1. But in ColorSettings they start from 0.
					Color stickmanColor = _colorSettings.colors[stickmanColorIndex];
					stickman.GetComponent<Character>().SetColor(stickmanColor);
				}
			}

		}
		
		#endregion

	}
}