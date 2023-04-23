// ------------------------
// Onur Ereren - April 2023
// ------------------------

namespace IslandGame.PuzzleEngine
{

    public interface ILevelReader
    {
        public void ReadLevel(int levelNo);

        public int[,] GetLevelConfiguration();
    }
}