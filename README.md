# mobile-game-case

The game implements a puzzle engine implementation in the background, while a gameplay implementation reflects the state of the puzzle engine in the game. 

The puzzle engine uses a txt level loading system. I was developing a scriptable asset system and an editor script for it, but it isn't complete. The progress can be seen in the WIP folder under Scripts\PuzzleEngine.

PuzzleEngine can be tested by itself in the Terminal scene under Scenes folder. Terminal play has undo function, while it is not implemented in the gameplay.

Puzzle engine can take any number of islands and any number of rows in those islands. It can also know which islands are locked for unlocking with ads. Ad locked islands are not implemented in the gameplay. Also, due to the size of the island prefab (2f) any test other than 4 rows would create unstable results.

Gameplay implementation lacks most of the game feel elements of the original game.

Current Issues:

* Puzzle state drags one move behind the command execution. This means it is required to transfer a full island into an empty one after the level is completed in order to finish the level. This is a problem with the puzzle engine because the same is valid for the Terminal play. The console log of the puzzle state gives accurate results, thus the game can be played by checking the console instead of the game view.

Further Improvements:

* Obviously, the gameplay section needs polish.
* Also, the gameplay implementation code cleanup is required. (too much of limited time spent on puzzle engine. Not wise.)
* Level loading can be switched using the interface; possible options are the work-in-progress scriptable objects, or using addressables to load more levels remotely.
