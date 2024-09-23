using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum FruitType
{
    APPLE,
    BANANA
}

public class Fruit : MonoBehaviour
{
    public FruitType type;
    public BoardCell cell;
    public Image image;

    public void SetType(FruitType type, Color color)
    {
        this.type = type;
        this.image.color = color;
    }

    public void SetCell(BoardCell cell)
    {
        this.cell = cell;
        this.cell.fruit = this;
        transform.position = cell.transform.position;
    }
}
