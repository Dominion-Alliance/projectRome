using UnityEngine;
using System.Collections;

public class HexCell : MonoBehaviour {

    public HexCoordinates coordinates;

    public Color color;

    public HexMeshDeclarations.terrainType type;

    public localPlayer owner;


    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if (type == HexMeshDeclarations.terrainType.grassland)
        {
            color = HexMeshDeclarations.terrainColor.grasslandColor;
        }
        if (type == HexMeshDeclarations.terrainType.water)
        {
            color = HexMeshDeclarations.terrainColor.waterColor;
        }
    }
}
