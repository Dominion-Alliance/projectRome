using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

//Credit to http://catlikecoding.com/unity/tutorials/hex-map-1/ for help and tutorial

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]

public class HexGrid : MonoBehaviour
{

    public int width = 6;
    public int height = 6;
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

        Text label = Instantiate<Text>(cellLabelPrefab);
        label.rectTransform.SetParent(gridCanvas.transform, false);
        label.rectTransform.anchoredPosition =
            new Vector2(position.x, position.z);
        label.text = x.ToString() + "\n" + z.ToString();

    }
}