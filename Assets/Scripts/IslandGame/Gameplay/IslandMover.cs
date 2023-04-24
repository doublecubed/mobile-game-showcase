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

        private bool _isLifted;

        private Vector3 _startingPosition;
        private Vector3 _liftedPosition;

        #endregion
        
        #region MONOBEHAVIOUR
        
        private void Awake()
        {
            
        }

       
        #endregion
        
        #region METHODS

        private void GeneratePositionVectors(Vector3 position)
        {
            _startingPosition = position;
            _liftedPosition = _startingPosition + Vector3.up * _liftDistance;
        }

        private void MoveIsland(Vector3 targetPosition)
        {
            transform.DOMove(targetPosition, _liftDuration);
        }
        
        #endregion
    }
}