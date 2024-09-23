using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    public List<SnakeBody> bodys;

    public SnakeBody bodyPrefab;

    public Board board;

    public Vector2Int direction = Vector2Int.left;

    public SnakeBody lastBodyTail;

    public bool canChangeDir { get; private set; }

    public void ClearSnakeBody()
    {
        for(int i = 0; i < bodys.Count; i++)
        {
            bodys[i].cell.body = null;
            Destroy(bodys[i].gameObject);
        }
        bodys.Clear();
    }

    public void CreateNewBody(BoardCell cell, SnakeBodyType type)
    {
        SnakeBody newBody = Instantiate(bodyPrefab, transform);

        newBody.SetType(type);
        newBody.MoveBodyTo(cell);
        bodys.Add(newBody);
    }

    public void ChangeDirection(Vector2Int dir)
    {
        if ((direction == Vector2Int.up && dir == Vector2Int.down) ||
            (direction == Vector2Int.down && dir == Vector2Int.up) ||
            (direction == Vector2Int.left && dir == Vector2Int.right) ||
            (direction == Vector2Int.right && dir == Vector2Int.left))
        { return; }
        direction = dir;
        canChangeDir = false;
    }

    public void SnakeMove()
    {
        BoardCell adjacentCell = board.GetAdjacentCell(bodys[0].cell, direction);
        if (adjacentCell != null)
        {
            lastBodyTail = bodys[bodys.Count - 1];
            for (int i = bodys.Count - 1; i > 0; i--)
            {
                bodys[i].MoveBodyTo(bodys[i - 1].cell);
            }
            bodys[0].MoveBodyTo(adjacentCell);

            lastBodyTail.cell.body = null;
        }
        canChangeDir = true;
    }
}
