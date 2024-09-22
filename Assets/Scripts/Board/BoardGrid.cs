using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardGrid : MonoBehaviour
{
    // 15 * 21
    public BoardRow[] rows { get; private set; }
    public BoardCell[] cells { get; private set; }

    public int size => cells.Length;
    public int height => rows.Length;
    public int width => size / height;

    private void Awake()
    {
        rows = GetComponentsInChildren<BoardRow>();
        cells = GetComponentsInChildren<BoardCell>();
    }
}
