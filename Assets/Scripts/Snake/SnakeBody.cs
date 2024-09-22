using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum SnakeBodyType
{
    Body,
    Head
}

public class SnakeBody : MonoBehaviour
{
    private SnakeBodyType type;
    public BoardCell cell;

    public Image image;
    private RectTransform imageShape;

    private void Awake()
    {
        imageShape = image.GetComponent<RectTransform>();
    }

    public void SetType(SnakeBodyType newType)
    {
        this.type = newType;
        if(newType == SnakeBodyType.Body)
        {
            imageShape.sizeDelta = new Vector2(44, 44);
        }
        else if(newType == SnakeBodyType.Head)
        {
            imageShape.sizeDelta = new Vector2(50, 50);
        }
    }

    public void MoveBodyTo(BoardCell cell)
    {
        this.cell = cell;
        this.cell.body = this;

        transform.position = cell.transform.position;
    }
}
