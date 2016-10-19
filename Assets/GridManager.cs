using UnityEngine;
using System.Collections;

public class GridManager : MonoBehaviour
{
    public GameObject Hex;
    //This time instead of specifying the number of hexes you should just drop your ground game object on this public variable
    public GameObject Ground;

    private float hexWidth;
    private float hexHeight;
    private float groundWidth;
    private float groundHeight;

    void setSizes()
    {
        hexWidth = Hex.GetComponent<Renderer>().bounds.size.x;
        hexHeight = Hex.GetComponent<Renderer>().bounds.size.z;
        groundWidth = Ground.GetComponent<Renderer>().bounds.size.x;
        groundHeight = Ground.GetComponent<Renderer>().bounds.size.z;
    }

    //The method used to calculate the number hexagons in a row and number of rows
    //Vector2.x is gridWidthInHexes and Vector2.y is gridHeightInHexes
    Vector2 calcGridSize()
    {
        //According to the math textbook hexagon's side length is half of the height
        float sideLength = hexHeight / 2;
        //the number of whole hex sides that fit inside inside ground height
        int nrOfSides = (int)(groundHeight / sideLength);
        //I will not try to explain the following calculation because I made some assumptions, which might not be correct in all cases, to come up with the formula. So you'll have to trust me or figure it out yourselves.
        int gridHeightInHexes = (int)(nrOfSides * 2 / 3);
        //When the number of hexes is even the tip of the last hex in the offset column might stick up.
        //The number of hexes in that case is reduced.
        if (gridHeightInHexes % 2 == 0
&& (nrOfSides + 0.5f) * sideLength > groundHeight)
            gridHeightInHexes--;
        //gridWidth in hexes is calculated by simply dividing ground width by hex width
        return new Vector2((int)(groundWidth / hexWidth), gridHeightInHexes);
    }
    //Method to calculate the position of the first hexagon tile
    //The center of the hex grid is (0,0,0)
    Vector3 calcInitPos()
    {
        Vector3 initPos;
        initPos = new Vector3(-groundWidth / 2 + hexWidth / 2, 0,
        groundHeight / 2 - hexWidth / 2);

        return initPos;
    }

    Vector3 calcWorldCoord(Vector2 gridPos)
    {
        Vector3 initPos = calcInitPos();
        float offset = 0;
        if (gridPos.y % 2 != 0)
            offset = hexWidth / 2;

        float x = initPos.x + offset + gridPos.x * hexWidth;
        float z = initPos.z - gridPos.y * hexHeight * 0.75f;
        //If your ground is not a plane but a cube you might set the y coordinate to sth like groundDepth/2 + hexDepth/2
        return new Vector3(x, 74, z);
    }

    void createGrid()
    {
        Vector2 gridSize = calcGridSize();
        GameObject hexGridGO = new GameObject("HexGrid");

        for (float y = 0; y < gridSize.y; y++)
        {
            float sizeX = gridSize.x;
            //if the offset row sticks up, reduce the number of hexes in a row
            if (y % 2 != 0 && (gridSize.x + 0.5) * hexWidth > groundWidth)
                sizeX--;
            for (float x = 0; x < sizeX; x++)
            {
                GameObject hex = (GameObject)Instantiate(Hex);
                Vector2 gridPos = new Vector2(x, y);
                hex.transform.position = calcWorldCoord(gridPos);
                hex.transform.parent = hexGridGO.transform;
            }
        }
    }

    void Start()
    {
        setSizes();
        createGrid();
    }
}

//https://tbswithunity3d.wordpress.com/2012/02/21/hexagonal-grid-generating-the-grid/

//public class GridManager : MonoBehaviour
//{
//    //following public variable is used to store the hex model prefab;
//    //instantiate it by dragging the prefab on this variable using unity editor
//    public GameObject Hex;
//    //next two variables can also be instantiated using unity editor
//    public int gridWidthInHexes = 10;
//    public int gridHeightInHexes = 10;

//    //Hexagon tile width and height in game world
//    private float hexWidth;
//    private float hexHeight;

//    //Method to initialise Hexagon width and height
//    void setSizes()
//    {
//        //renderer component attached to the Hex prefab is used to get the current width and height
//        hexWidth = Hex.GetComponent<Renderer>().bounds.size.x;
//        hexHeight = Hex.GetComponent<Renderer>().bounds.size.z;
//    }

//    //Method to calculate the position of the first hexagon tile
//    //The center of the hex grid is (0,0,0)
//    Vector3 calcInitPos()
//    {
//        Vector3 initPos;
//        //the initial position will be in the left upper corner
//        initPos = new Vector3(-hexWidth * gridWidthInHexes / 2f + hexWidth / 2, 0,
//gridHeightInHexes / 2f * hexHeight - hexHeight / 2);

//        return initPos;
//    }

//    //method used to convert hex grid coordinates to game world coordinates
//    public Vector3 calcWorldCoord(Vector2 gridPos)
//    {
//        //Position of the first hex tile
//        Vector3 initPos = calcInitPos();
//        //Every second row is offset by half of the tile width
//        float offset = 0;
//        if (gridPos.y % 2 != 0)
//            offset = hexWidth / 2;

//        float x = initPos.x + offset + gridPos.x * hexWidth;
//        //Every new line is offset in z direction by 3/4 of the hexagon height
//        float z = initPos.z - gridPos.y * hexHeight * 0.75f;
//        return new Vector3(x, 0, z);
//    }

//    //Finally the method which initialises and positions all the tiles
//    void createGrid()
//    {
//        //Game object which is the parent of all the hex tiles
//        GameObject hexGridGO = new GameObject("HexGrid");

//        for (float y = 0; y < gridHeightInHexes; y++)
//        {
//            for (float x = 0; x < gridWidthInHexes; x++)
//            {
//                //GameObject assigned to Hex public variable is cloned
//                GameObject hex = (GameObject)Instantiate(Hex);
//                //Current position in grid
//                Vector2 gridPos = new Vector2(x, y);
//                hex.transform.position = calcWorldCoord(gridPos);
//                hex.transform.parent = hexGridGO.transform;
//            }
//        }
//    }

//    //The grid should be generated on game start
//    void Start()
//    {
//        setSizes();
//        createGrid();
//    }
//}