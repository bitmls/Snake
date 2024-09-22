using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardCell : MonoBehaviour
{
    public Vector2Int coordinates { get; set; }

    public SnakeBody body { get; set; }

    public bool occupied => body != null;
}
