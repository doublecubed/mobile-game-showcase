// ------------------------
// Onur Ereren - April 2023
// ------------------------

// GameDriver is the entry point of the gameplay implementation.
// GameDriver loads levels, and acts on win condition.

using UnityEngine;
using IslandGame.PuzzleEngine;
using IslandGame.UI;
using UnityEngine.Serialization;


namespace IslandGame.Gameplay
{
	public class GameDriver : MonoBehaviour
	{
		#region REFERENCES
		
		
		public PuzzleSolver PuzzleSolver;
		public UIDriver UIDriver;

		private CommandResolver _commandResolver;

		private IslandSpawner _islandSpawner;
		private CharacterSpawner _characterSpawner;
		private IslandMover _islandMover;
		
		#endregion

		#region VARIABLES

		private Transform[] _islands;
		public Island[] IslandScripts { get; private set; }
		
		private int[,] _puzzleState;
		
		#endregion

		#region MONOBEHAVIOUR

		private void Start()
		{
			GetComponentReferences();
			UIDriver.ShowMenuPanel();
			SetupLevel();
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
			UIDriver.ShowGamePanel();
			_commandResolver.ReceiveInput = true;
		}

		public void NextLevel()
		{
			SetupLevel();
			UIDriver.ShowGamePanel();
			_commandResolver.ReceiveInput = true;
		}
		
		private void SetupLevel()
		{
			CleanPreviousLevel();
			LoadLevelData(out int levelNumber);
			UIDriver.SetLevelLabel(levelNumber);
			
			int numberOfIslands = _puzzleState.GetLength(0);
			int numberOfRows = _puzzleState.GetLength(1);

			SpawnIslands(numberOfIslands, numberOfRows);
			FeedIslandsToIslandMover();
			
			SpawnCharacters(numberOfRows);
		}

		private void CleanPreviousLevel()	// Messy solution, to be tidied up
		{
			Island[] islands = FindObjectsOfType<Island>();
			foreach (Island island in islands)
			{
				Destroy(island.gameObject);
			}

			Character[] characters = FindObjectsOfType<Character>();
			foreach (Character character in characters)
			{
				Destroy(character.gameObject);
			}
				
		}
		
		public void PuzzleCompleted()
		{
			int currentLevel = PlayerPrefs.GetInt("levelNumber", 0);
			PlayerPrefs.SetInt("levelNumber", currentLevel + 1);
			UIDriver.ShowWinPanel();
			_commandResolver.ReceiveInput = false;
		}
		

		private void LoadLevelData(out int levelNumber)
		{
			levelNumber = PlayerPrefs.GetInt("levelNumber", 0);
			PuzzleSolver.LoadPuzzle(levelNumber);
			_puzzleState = PuzzleSolver.GetPuzzleState();
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