// ------------------------
// Onur Ereren - April 2023
// ------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

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

		private void Start()
		{
			//GenerateBridge(new Vector3(-4f,1f,4.5f), new Vector3(4f,1f,-1.5f), Vector3.right);
			
		}

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