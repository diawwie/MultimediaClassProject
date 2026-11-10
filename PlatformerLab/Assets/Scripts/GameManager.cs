using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private Transform player;
    [SerializeField] private float fallLimit = -8f;

    [Header("Interface")]
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private GameObject endPanel;
    [SerializeField] private TMP_Text endTitle;

    private int score;
    private bool gameOver;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        Time.timeScale = 1f;   // a restart arrives here from a frozen game
    }

    void Start()
    {
        ShowScore();
    }

    void Update()
    {
        if (!gameOver && player.position.y < fallLimit)
        {
            PlayerDied();
        }
    }

    public void AddScore(int points)
    {
        score += points;
        ShowScore();
    }

    public void PlayerDied()
    {
        EndGame("Game over");
    }

    public void LevelComplete()
    {
        EndGame("Level complete!");
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void EndGame(string title)
    {
        if (gameOver) return;
        gameOver = true;
        endTitle.text = title;
        endPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    private void ShowScore()
    {
        scoreText.text = $"Score: {score}";
    }
}
