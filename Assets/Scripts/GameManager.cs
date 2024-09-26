using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Board board;
    public Snake snake;
    public Fruits fruits;

    private float timer = 0;
    public float duration = 0.15f;

    private int score = 0;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI bestScoreText;

    public TextMeshProUGUI NumberText;

    public CanvasGroup gameOverCanvasGroup;

    private bool isGameOver = false;

    private float spawnTimer = 0;
    public float spawnDuration = 10;

    private void Start()
    {
        NewGame();
    }

    private void Update()
    {
        if (isGameOver)
            return;

        timer += Time.deltaTime;
        spawnTimer += Time.deltaTime;

        if (timer > duration)
        {
            timer = 0;

            CheckGameOver();

            snake.SnakeMove();
            //CheckEmptyCellCount();
        }

        if (spawnTimer > spawnDuration)
        {
            spawnTimer = 0;

            if (fruits.fruitList.Count < 3)
            {
                fruits.SpawnFruit(FruitType.BANANA);
            }
        }

        SnakeChangeDirection();

        CheckEatFruit();
    }

    public void NewGame()
    {
        snake.ClearSnakeBody();
        snake.direction = Vector2Int.left;
        fruits.ClearFruitList();

        SetScore(0);
        bestScoreText.text = PlayerPrefs.GetInt("bestScore", 0).ToString();

        gameOverCanvasGroup.alpha = 0f;
        gameOverCanvasGroup.interactable = false;

        isGameOver = false;

        Time.timeScale = 1f;

        NumberText.enabled = false;
        //StartCoroutine(StartDelay(3.5f));

        spawnTimer = 0;

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
        isGameOver = true;
        gameOverCanvasGroup.interactable = true;
        StartCoroutine(Fade(gameOverCanvasGroup, 1.0f, 0.5f));
    }

    public void QuitGame()
    {
        SceneManager.LoadScene("StartMenu");
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
                FruitType type = fruits.fruitList[i].type;
                fruits.RemoveFruit(i);
                snake.CreateNewBody(snake.lastBodyTail.cell, SnakeBodyType.Body);
                if (type == FruitType.APPLE)
                {
                    fruits.SpawnFruit(FruitType.APPLE);
                }
                else if (type == FruitType.BANANA)
                {
                    StartCoroutine(SpeedUp(2));
                }
                ScoreAddValue();
            }
        }
    }

    private void CheckEmptyCellCount()
    {
        int count = 0;
        for (int i = 0; i < board.grid.width; i++)
        {
            for (int j = 0; j < board.grid.height; j++)
            {
                if (board.grid.rows[j].cells[i].occupied)
                    count++;
            }
        }
        Debug.Log(count);
    }

    public void ScoreAddValue(int value = 1)
    {
        SetScore(score + value);
    }

    private void SetScore(int value)
    {
        this.score = value;
        scoreText.text = value.ToString();

        SaveBestData();
    }

    private void SaveBestData()
    {
        int bestScore = PlayerPrefs.GetInt("bestScore", 0);

        if (score > bestScore)
        {
            PlayerPrefs.SetInt("bestScore", score);
        }
    }

    private IEnumerator Fade(CanvasGroup canvasGroup, float to, float delay)
    {
        yield return new WaitForSeconds(delay);

        float elapsed = 0f;
        float duration = 0.5f;
        float from = canvasGroup.alpha;
        while (elapsed < duration)
        {
            if (!isGameOver)
            {
                yield break;
            }

            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(from, to, elapsed / duration);
            yield return null;
        }

        canvasGroup.alpha = to;
    }

    private IEnumerator StartDelay(float delay)
    {
        isGameOver = true;

        NumberText.enabled = true;

        float elapsed = delay;

        NumberText.text = 3.ToString();

        while (elapsed >= 0.5)
        {
            elapsed -= Time.deltaTime;

            if (elapsed > 2 && elapsed <= 3)
                NumberText.text = 2.ToString();
            else if (elapsed > 1 && elapsed <= 2)
                NumberText.text = 1.ToString();
            else if (elapsed > 0 && elapsed <= 1)
                NumberText.text = "GO!";

            yield return null;
        }

        NumberText.enabled = false;

        isGameOver = false;
    }

    private IEnumerator SpeedUp(float seconds)
    {
        duration = 0.08f;

        yield return new WaitForSeconds(seconds);

        duration = 0.15f;
    }

    public void SnakeDirectionUp()
    {
        if (snake.direction != Vector2Int.up)
            snake.direction = Vector2Int.down;
    }

    public void SnakeDirectionDown()
    {
        if (snake.direction != Vector2Int.down)
            snake.direction = Vector2Int.up;
    }

    public void SnakeDirectionLeft()
    {
        if (snake.direction != Vector2Int.right)
            snake.direction = Vector2Int.left;
    }

    public void SnakeDirectionRight()
    {
        if (snake.direction != Vector2Int.left)
            snake.direction = Vector2Int.right;
    }
}