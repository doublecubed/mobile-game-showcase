// ------------------------
// Onur Ereren - April 2023
// ------------------------

// Bridge generator. Uses Unity's spline package to generate a bridge between islands. (Currently not implemented)

using UnityEngine;

namespace IslandGame.Gameplay
{
	public class BridgeGenerator : MonoBehaviour
	{
		#region REFERENCES

		[SerializeField]
		private GameObject _bridgePrefab;
		
		#endregion

		#region VARIABLES

		[SerializeField] 
		private float _bridgeDrawDuration;

		#endregion

		#region MONOBEHAVIOUR

		#endregion

		#region METHODS

		public void GenerateBridge(Vector3 pointOne, Vector3 pointTwo, Vector3 heading)
		{
			GameObject bridge = Instantiate(_bridgePrefab, pointOne, Quaternion.LookRotation(heading));
			Bridge bridgeScript = bridge.GetComponent<Bridge>();
			bridgeScript.SetBridgeEnd(pointTwo, heading);
		}


		#endregion

	}
}