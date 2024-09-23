using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public BoardGrid grid;

    private void Awake()
    {
        grid = GetComponentInChildren<BoardGrid>();
    }

    private void Start()
    {
        InitBoard();
    }

    public void InitBoard()
    {
        for (int y = 0; y < grid.height; y++)
        {
            for (int x = 0; x < grid.width; x++)
            {
                grid.rows[y].cells[x].coordinates = new Vector2Int(x, y);
            }
        }
    }

    public BoardCell GetAdjacentCell(BoardCell cell, Vector2Int dir)
    {
        Vector2Int srcPostion = cell.coordinates;
        Vector2Int dstPostion = cell.coordinates + dir;
        if (dstPostion.x < 0 || dstPostion.x >= grid.width || dstPostion.y < 0 || dstPostion.y >= grid.height)
            return null;
        return grid.rows[dstPostion.y].cells[dstPostion.x];
    }

    public BoardCell GetRamdomEmptyCell()
    {
        int index = Random.Range(0, grid.cells.Length);

        while (grid.cells[index].occupied)
        {
            index = Random.Range(0, grid.cells.Length);
        }
        return grid.cells[index];
    }
}
