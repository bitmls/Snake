using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Board board;
    public Snake snake;
    public Fruits fruits;

    private float timer = 0;
    public float duration = 0.2f;

    public int score = 0;
    public int bestScore = 0;

    private void Start()
    {
        NewGame();
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer > duration)
        {
            timer = 0;

            CheckGameOver();

            snake.SnakeMove();
            //CheckEmptyCellCount();
        }

        SnakeChangeDirection();

        CheckEatFruit();
    }

    public void NewGame()
    {
        Time.timeScale = 1f;
        snake.ClearSnakeBody();
        snake.direction = Vector2Int.left;
        fruits.ClearFruitList();

        snake.CreateNewBody(board.grid.rows[7].cells[10], SnakeBodyType.Head);
        snake.CreateNewBody(board.grid.rows[7].cells[11], SnakeBodyType.Body);
        snake.CreateNewBody(board.grid.rows[7].cells[12], SnakeBodyType.Body);
        snake.CreateNewBody(board.grid.rows[7].cells[13], SnakeBodyType.Body);

        fruits.SpawnFruit(FruitType.APPLE);
    }

    private void CheckGameOver()
    {
        BoardCell adjacentCell = board.GetAdjacentCell(snake.bodys[0].cell, snake.direction);
        if (adjacentCell == null)
            GameOver();
        for (int i = 0; i < snake.bodys.Count; i++)
            if (adjacentCell == snake.bodys[i].cell)
                GameOver();
    }

    private void GameOver()
    {
        Debug.Log("Game Over");
        Time.timeScale = 0f;
    }

    public void QuitGame()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    private void SnakeChangeDirection()
    {
        if (snake.canChangeDir)
        {
            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
                snake.ChangeDirection(Vector2Int.up);
            else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
                snake.ChangeDirection(Vector2Int.down);
            else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
                snake.ChangeDirection(Vector2Int.left);
            else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
                snake.ChangeDirection(Vector2Int.right);
        }
    }

    private void CheckEatFruit()
    {
        for (int i = 0; i < fruits.fruitList.Count; i++)
        {
            if (snake.bodys[0].cell == fruits.fruitList[i].cell)
            {
                fruits.RemoveFruit(i);
                snake.CreateNewBody(snake.lastBodyTail.cell, SnakeBodyType.Body);
                fruits.SpawnFruit(FruitType.APPLE);
            }
        }
    }

    private void CheckEmptyCellCount()
    {
        int count = 0;
        for(int i = 0; i < board.grid.width; i++)
        {
            for(int j = 0; j < board.grid.height; j++)
            {
                if (board.grid.rows[j].cells[i].occupied)
                    count++;
            }
        }
        Debug.Log(count);
    }
}
