using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

//Credit and thanks to http://catlikecoding.com/unity/tutorials/hex-map-1/ for help and tutorial

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]

public class HexGrid : MonoBehaviour
{

    public int width = 6;
    public int height = 6;
    public Color grasslandColor = Color.white;
    public Color waterColor = Color.blue;
    public Color touchedColor = Color.magenta;
    System.Random rnd = new System.Random();
    Canvas gridCanvas;

    public HexCell cellPrefab;
    public Text cellLabelPrefab;
    HexaMesh hexMesh;


    HexCell[] cells;

    void Start()
    {
        hexMesh.Triangulate(cells);
    }

    void Awake()
    {
        gridCanvas = GetComponentInChildren<Canvas>();
        hexMesh = GetComponentInChildren<HexaMesh>();
        cells = new HexCell[height * width];

        for (int z = 0, i = 0; z < height; z++)
        {
            for (int x = 0; x < width; x++)
            {
                CreateCell(x, z, i++);
            }
        }
    }

    void CreateCell(int x, int z, int i)
    {
        Vector3 position;
        position.x = (x + z * 0.5f - z / 2) * (HexMetrics.innerRadius * 2f);
        position.y = 0f;
        position.z = z * (HexMetrics.outerRadius * 1.5f);

        HexCell cell = cells[i] = Instantiate<HexCell>(cellPrefab);
        cell.transform.SetParent(transform, false);
        cell.transform.localPosition = position;
        cell.coordinates = HexCoordinates.FromOffsetCoordinates(x, z);

        //Random Number Generation
        int cellType = rnd.Next(1, 3);
        //////////////////////////
        if (cellType == 1)
        {
            cell.color = grasslandColor;
            cell.type = Type.grassland;
        }
        else
        {
            cell.color = waterColor;
            cell.type = Type.water;
        }


        Text label = Instantiate<Text>(cellLabelPrefab);
        label.rectTransform.SetParent(gridCanvas.transform, false);
        label.rectTransform.anchoredPosition =
            new Vector2(position.x, position.z);
        label.text = cell.coordinates.ToStringOnSeparateLines();
        
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            HandleInput();
        }
    }

    void HandleInput()
    {
        Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(inputRay, out hit))
        {
            TouchCell(hit.point);
        }
    }

    void TouchCell(Vector3 position)
    {
        position = transform.InverseTransformPoint(position);
        HexCoordinates coordinates = HexCoordinates.FromPosition(position);
        Debug.Log("touched at " + coordinates.ToString());
        int index = coordinates.X + coordinates.Z * width + coordinates.Z / 2;
        HexCell cell = cells[index];
        cell.color = touchedColor;
        hexMesh.Triangulate(cells);

    }



}