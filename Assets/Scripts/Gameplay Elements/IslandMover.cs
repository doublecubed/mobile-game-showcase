// ------------------------
// Onur Ereren - April 2023
// ------------------------

using UnityEngine;
using DG.Tweening;

namespace IslandGame
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
            GeneratePositionVectors();
        }

        private void OnMouseDown()
        {
            if (_isLifted)
            {
                MoveIsland(_startingPosition);
            }
            else
            {
                MoveIsland(_liftedPosition);
            }

            _isLifted = !_isLifted;
        }
        
        #endregion
        
        #region METHODS

        private void GeneratePositionVectors()
        {
            _startingPosition = transform.position;
            _liftedPosition = _startingPosition + Vector3.up * _liftDistance;
        }

        private void MoveIsland(Vector3 targetPosition)
        {
            transform.DOMove(targetPosition, _liftDuration);
        }
        
        #endregion
    }
}