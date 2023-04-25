// ------------------------
// Onur Ereren - April 2023
// ------------------------


using UnityEngine;

namespace IslandGame.Gameplay
{
	public class Character : MonoBehaviour
	{
		#region REFERENCES

		private SkinnedMeshRenderer _renderer;

		public SkinnedMeshRenderer Renderer
		{
			get { return _renderer; }
			private set { _renderer = value; }
		}

		#endregion

		#region MONOBEHAVIOUR

		private void Awake()
		{
			Renderer = GetComponentInChildren<SkinnedMeshRenderer>();
		}

		#endregion

		#region METHODS

		public void SetColor(Color color)
		{
			Renderer.sharedMaterial.color = color;
		}

		#endregion

	}
}