// ------------------------
// Onur Ereren - April 2023
// ------------------------

using UnityEngine;

namespace IslandGame.Settings
{
    [CreateAssetMenu(fileName = "New Color Settings", menuName = "Island Game/New Color Settings")]
    public class ColorSettings : ScriptableObject
    {
        public Color[] colors;
    }
}