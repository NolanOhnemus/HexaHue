using System.ComponentModel;
using UnityEngine;

public class HexGrid : MonoBehaviour
{
    private static int width = -1;
    private static int height = -1;

    private Vector3 gridCenter;

    public HexCell hexPrefab;

    static HexCell[] cells;

    private void Start()
    {
        width = CrossScene.GetGridSize();
        height = CrossScene.GetGridSize();

        if (width < 0 || height < 0)
        {
            width = height = 6;
        }
        
        cells = new HexCell[width * height];
        var cellIndex = 0;
        for(var i = 0; i < height; i++)
        {
            for (var j = 0; j < width; j++)
            {
                CreateCell(j, i, cellIndex);
                cellIndex++;
            }
        }
        gridCenter = getCenter();
        transform.position = gridCenter;
    }

    public static int getTotalHints()
    {
        var total = 0;
        foreach (var cell in cells)
        {
            total += cell.getNumNeighbors();
        }
        return total;
    }

    private Vector3 getCenter()
    {
        var sumVector = new Vector3(0, 0, 0);
        var numCells = 0;
        foreach (HexCell cell in cells)
        {
            sumVector += cell.transform.position;
            numCells++;
        }

        return new Vector3(-sumVector.x / numCells, -sumVector.y / numCells, 0);

    }

    private void CreateCell(int x, int y, int cellIndex)
    {
        Vector3 cellPosition;
        cellPosition.x = (x + y * 0.5f - y / 2) * (HexMath.innerRadius * 2f);
        cellPosition.y = y * (HexMath.outerRadius * 1.5f);
        cellPosition.z = 0;

        var cell = Instantiate(hexPrefab, transform, false);
        cell.name = "Hexagon";
        cells[cellIndex] = cell;
        cell.transform.localPosition = cellPosition;

    }

    public bool IsSolved()
    {
        var allSolved = true;
        foreach (var cell in cells)
        {
            if (!cell.wasSolved)
            {
                allSolved = false;
            }
        }

        return allSolved;
    }

    public int GetWidth()
    {
        if (width <= 0)
        {
            width = CrossScene.GetGridSize();
        }
        return width;
    }
}
