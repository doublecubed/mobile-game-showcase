// ------------------------
// Onur Ereren - April 2023
// ------------------------

using UnityEngine;
using DG.Tweening;

namespace IslandGame.Gameplay
{
    public class IslandMover : MonoBehaviour
    {
        #region REFERENCES
        
        #endregion
        
        #region VARIABLES
        
        [SerializeField] private float _liftDistance;
        [SerializeField] private float _liftDuration;

        private Transform[] _islands;
        
        private bool[] _isLifted;

        private float _startingYPosition;
        private float _liftedYPosition;
        

        #endregion
        
        #region MONOBEHAVIOUR
        
       
        #endregion
        
        #region METHODS

        public void FeedIslands(Transform[] islands)
        {
            _islands = islands;
            _startingYPosition = _islands[0].position.y;
            _liftedYPosition = _startingYPosition + _liftDistance;
            _isLifted = new bool[_islands.Length];
        }
        
        public void MoveIsland(int islandIndex)
        {
            if (!_isLifted[islandIndex])        // going up
            {
                _islands[islandIndex].DOMoveY(_liftedYPosition, MoveDuration(islandIndex, _liftedYPosition));
            }   
            else                                // going down
            {
                _islands[islandIndex].DOMoveY(_startingYPosition, MoveDuration(islandIndex, _startingYPosition));
            }

            _isLifted[islandIndex] = !_isLifted[islandIndex];
            
        }

        private float MoveDuration(int islandIndex, float destinationY)
        {
            float currentY = _islands[islandIndex].position.y;
            float ratioTraveled = 0;
            
            if (destinationY <= currentY) // going down
            {
                ratioTraveled = (_liftedYPosition - currentY) / _liftDistance;
            }
            else                        // going up
            {
                ratioTraveled = (currentY - _startingYPosition) / _liftDistance;
            }

            float remainingTime = _liftDuration * (1-ratioTraveled);
            return remainingTime;
        }
        
        #endregion
    }
}