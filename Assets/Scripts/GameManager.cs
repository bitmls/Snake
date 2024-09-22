using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Board board;
    public Snake snake;

    private void Start()
    {
        snake.CreateNewBody(board.grid.rows[7].cells[10], SnakeBodyType.Head);
        snake.CreateNewBody(board.grid.rows[7].cells[11], SnakeBodyType.Body);
        snake.CreateNewBody(board.grid.rows[7].cells[12], SnakeBodyType.Body);
        snake.CreateNewBody(board.grid.rows[7].cells[13], SnakeBodyType.Body);
    }

    private void Update()
    {

    }
}
