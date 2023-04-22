// ------------------------
// Onur Ereren - April 2023
// ------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestArray : MonoBehaviour
{
	#region REFERENCES
	
	#endregion
	
	#region VARIABLES

	private int[] intArray;
	
	#endregion
	
	#region MONOBEHAVIOUR

	private void Start()
	{
		intArray = new int[3];
		intArray[0] = 0;
		intArray[1] = 1;
		intArray[2] = 2;

		Debug.Log("[" + intArray[0] + "," + intArray[1] + "," + intArray[2] + "]");
		ModifyArray(intArray);
		Debug.Log("[" + intArray[0] + "," + intArray[1] + "," + intArray[2] + "]");
	}

	#endregion
	
	#region METHODS

	private void ModifyArray(int[] leArray)
	{
		int[] lelArray = new int[3];
		Array.Copy(leArray, lelArray, 3);
		lelArray[0] = 5;
	}
	
	#endregion

}
