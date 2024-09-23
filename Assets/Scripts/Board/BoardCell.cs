using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardCell : MonoBehaviour
{
    public Vector2Int coordinates { get; set; }

    public SnakeBody body { get; set; }

    public Fruit fruit { get; set; }

    public bool occupied => body != null || fruit != null;
    public bool empty => body == null || fruit == null;
}
