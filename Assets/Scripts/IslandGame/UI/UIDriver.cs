// ------------------------
// Onur Ereren - April 2023
// ------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IslandGame.UI
{
	public class UIDriver : MonoBehaviour
	{
		#region REFERENCES

		[SerializeField]
		private GameObject _menuPanel;
		
		#endregion
	
		#region VARIABLES
	
		#endregion
	
		#region MONOBEHAVIOUR
	
		#endregion
	
		#region METHODS

		public void RemoveMenuPanel()
		{
			_menuPanel.SetActive(false);
		}
		
		#endregion

	}	
}


