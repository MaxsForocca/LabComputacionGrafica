using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState currentState;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        Time.timeScale = 1f;

        currentState = GameState.Playing;
    }

    private void Start()
    {
        CursorManager.Instance.HideCursor();
    }

    // =========================
    // PAUSE
    // =========================

    public void PauseGame()
    {
        if (currentState != GameState.Playing)
            return;

        currentState = GameState.Paused;

        UIManager.Instance.ShowPausePanel();

        Time.timeScale = 0f;

        CursorManager.Instance.ShowCursor();
    }

    public void ResumeGame()
    {
        if (currentState != GameState.Paused)
            return;

        currentState = GameState.Playing;

        UIManager.Instance.HidePausePanel();

        Time.timeScale = 1f;

        CursorManager.Instance.HideCursor();
    }

    // =========================
    // WIN
    // =========================

    public void WinGame()
    {
        if (currentState == GameState.Win)
            return;

        currentState = GameState.Win;

        UIManager.Instance.ShowWinPanel();

        Time.timeScale = 0f;

        CursorManager.Instance.ShowCursor();
    }

    // =========================
    // LEVEL
    // =========================

    public void RestartLevel()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    // =========================
    // HELPERS
    // =========================

    public bool IsPlaying()
    {
        return currentState == GameState.Playing;
    }

    public bool IsPaused()
    {
        return currentState == GameState.Paused;
    }

    public bool IsWin()
    {
        return currentState == GameState.Win;
    }
}