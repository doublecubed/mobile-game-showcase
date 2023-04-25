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

		[SerializeField] private GameObject _menuPanel;
		[SerializeField] private GameObject _gamePanel;
		[SerializeField] private GameObject _winPanel;
		
		#endregion
	
		#region VARIABLES
	
		#endregion
	
		#region MONOBEHAVIOUR
	
		#endregion
	
		#region METHODS

		public void ShowMenuPanel()
		{
			_menuPanel.SetActive(true);
			_gamePanel.SetActive(false);
			_winPanel.SetActive(false);
		}

			public void HideMenuPanel()
		{
			_menuPanel.SetActive(false);
		}

		public void ShowGamePanel()
		{
			_gamePanel.SetActive(true);
			_menuPanel.SetActive(false);
			_winPanel.SetActive(false);
			
		}

		public void HideGamePanel()
		{
			_gamePanel.SetActive(false);
		}

		public void ShowWinPanel()
		{
			_winPanel.SetActive(true);
			_gamePanel.SetActive(false);
			_menuPanel.SetActive(false);
		}

		public void HideWinPanel()
		{
			_winPanel.SetActive(false);
		}


		#endregion

	}	
}


