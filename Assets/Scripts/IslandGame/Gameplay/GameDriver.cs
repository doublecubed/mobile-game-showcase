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

		private IslandSpawner _islandSpawner;
		private CharacterSpawner _characterSpawner;
		private IslandMover _islandMover;
		
		#endregion

		#region VARIABLES

		private bool _gameStarted;

		private Transform[] _islands;
		public Island[] IslandScripts { get; private set; }
		
		private int[,] _puzzleState;
		
		#endregion

		#region MONOBEHAVIOUR

		private void Start()
		{
			GetComponentReferences();
		}
		
		
		#endregion

		#region METHODS

		private void GetComponentReferences()
		{
			_commandResolver = GetComponent<CommandResolver>();
			_islandSpawner = GetComponent<IslandSpawner>();
			_characterSpawner = GetComponent<CharacterSpawner>();
			_islandMover = GetComponent<IslandMover>();
		}
		
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
			LoadLevelData();
			
			int numberOfIslands = _puzzleState.GetLength(0);
			int numberOfRows = _puzzleState.GetLength(1);

			SpawnIslands(numberOfIslands, numberOfRows);
			FeedIslandsToIslandMover();
			
			SpawnCharacters(numberOfRows);
		}



		private void LoadLevelData()
		{
			int levelNumber = PlayerPrefs.GetInt("levelNumber", 0);
			_puzzleSolver.LoadPuzzle(levelNumber);
			_puzzleState = _puzzleSolver.GetPuzzleState();
		}
		
		private void SpawnIslands(int numberOfIslands, int numberOfRows)
		{
			_islandSpawner.SpawnIslands(numberOfIslands, numberOfRows, _commandResolver, out Island[] islands);
			IslandScripts = islands;
			_islands = IslandTransforms(IslandScripts);
		}

		private void FeedIslandsToIslandMover()
		{
			_islandMover.FeedIslands(_islands);
		}
		
		private void SpawnCharacters(int numberOfRows)
		{
			_characterSpawner.SpawnCharacters(IslandScripts, numberOfRows, _puzzleState);
		}

		private Transform[] IslandTransforms(Island[] scripts)
		{
			Transform[] islandTransforms = new Transform[scripts.Length];
			for (int i = 0; i < scripts.Length; i++)
			{
				islandTransforms[i] = scripts[i].transform;
			}

			return islandTransforms;
		}
		
		#endregion

	}
}