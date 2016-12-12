using UnityEngine;
using System.Collections;

public class HexMeshDeclarations : MonoBehaviour
{
    public enum terrainType
    {
        grassland,
        water,
        desert,
        mountain,
        coast,
    }

    public struct terrainColor
    {
        public static Color grasslandColor = Color.green;
        public static Color waterColor = Color.blue;
        public static Color touchedColor = Color.magenta;
        public static Color desertColor = Color.yellow; //change
        public static Color coastColor = Color.cyan; //change
        public static Color mountainColor = Color.gray;
    }    
}