// ------------------------
// Onur Ereren - April 2023
// ------------------------

// Bridge object script. Using splines package to draw the bridge. (Currently not implemented)

using UnityEngine;
using UnityEngine.Splines;

namespace IslandGame.Gameplay
{

	public class Bridge : MonoBehaviour
	{
		#region REFERENCES

		[SerializeField] 
		private Spline _bridgeSpline;

		#endregion

		#region VARIABLES

		#endregion

		#region MONOBEHAVIOUR

		private void Awake()
		{
			_bridgeSpline = GetComponentInChildren<Spline>();
		}

		#endregion

		#region METHODS

		public void SetBridgeEnd(Vector3 position, Vector3 heading)
		{
			Vector3 localPosition = position - transform.position;
			BezierKnot secondKnot = new BezierKnot(localPosition);
			secondKnot.Rotation = Quaternion.LookRotation(heading);
			_bridgeSpline.SetKnot(1, secondKnot);
		}
		
		#endregion

	}
}