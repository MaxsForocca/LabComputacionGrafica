using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState currentState;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        Time.timeScale = 1f;
        currentState = GameState.Playing;
    }

    private void Start()
    {
        CursorManager.Instance.HideCursor();
        TimerManager.Instance.StartTimer();
    }

    // =========================
    // PAUSE
    // =========================
    public void PauseGame()
    {
        if (currentState != GameState.Playing) return;
        currentState = GameState.Paused;
        TimerManager.Instance.PauseTimer();
        StartCoroutine(PauseRoutine());
    }

    private IEnumerator PauseRoutine()
    {
        // ORDEN CRÍTICO:
        // 1. Liberar cursor primero — el EventSystem necesita esto ANTES de timeScale = 0
        CursorManager.Instance.ShowCursor();

        // 2. Mostrar panel
        UIManager.Instance.ShowPausePanel();

        // 3. Esperar un frame REAL (WaitForSecondsRealtime ignora timeScale)
        //    Esto le da tiempo al EventSystem de procesar el nuevo estado del cursor
        yield return new WaitForSecondsRealtime(0.1f);

        // 4. Congelar el tiempo AL FINAL
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        if (currentState != GameState.Paused) return;
        currentState = GameState.Playing;

        // Al reanudar: primero descongelar, luego ocultar UI
        Time.timeScale = 1f;
        TimerManager.Instance.ResumeTimer();
        UIManager.Instance.HidePausePanel();
        CursorManager.Instance.HideCursor();
    }

    // =========================
    // WIN
    // =========================
    public void WinGame()
    {
        if (currentState == GameState.Win) return;
        currentState = GameState.Win;
        TimerManager.Instance.StopTimer();
        StartCoroutine(WinRoutine());
    }

    private IEnumerator WinRoutine()
    {
        string finalTime = TimerManager.Instance.GetFormattedTime();

        // Mismo orden que PauseRoutine
        CursorManager.Instance.ShowCursor();
        UIManager.Instance.ShowWinPanel(finalTime);

        yield return new WaitForSecondsRealtime(0.1f);

        Time.timeScale = 0f;
    }

    // =========================
    // LEVEL
    // =========================
    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
    }

    // =========================
    // HELPERS
    // =========================
    public bool IsPlaying() => currentState == GameState.Playing;
    public bool IsPaused()  => currentState == GameState.Paused;
    public bool IsWin()     => currentState == GameState.Win;
}