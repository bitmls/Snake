using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    public List<SnakeBody> bodys;

    public SnakeBody bodyPrefab;

    public Board board;

    private Vector2Int direction = Vector2Int.left;

    private float timer = 0;
    public float duration = 1f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            ChangeDirection(Vector2Int.up);
        else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            ChangeDirection(Vector2Int.down);
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            ChangeDirection(Vector2Int.left);
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            ChangeDirection(Vector2Int.right);

        timer += Time.deltaTime;

        if (timer > duration)
        {
            timer = 0;
            SnakeMove();
        }
    }

    public void CreateNewBody(BoardCell cell, SnakeBodyType type)
    {
        SnakeBody newBody = Instantiate(bodyPrefab, transform);

        newBody.SetType(type);
        newBody.MoveBodyTo(cell);
        bodys.Add(newBody);
    }

    private void ChangeDirection(Vector2Int dir)
    {
        direction = dir;
    }

    public void SnakeMove()
    {
        BoardCell adjacentCell = board.GetAdjacentCell(bodys[0].cell, direction);
        if (adjacentCell != null)
        {
            BoardCell fromCell;
            BoardCell toCell = adjacentCell;
            for (int i = 0; i < bodys.Count; i++)
            {
                fromCell = bodys[i].cell;
                bodys[i].MoveBodyTo(toCell);
                toCell = fromCell;
            }
        }
    }
}
