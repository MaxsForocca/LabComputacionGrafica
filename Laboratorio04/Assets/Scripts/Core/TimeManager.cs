using UnityEngine;

public class TimerManager : MonoBehaviour
{
    public static TimerManager Instance;

    private float elapsedTime = 0f;
    private bool isRunning = false;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Update()
    {
        if (!isRunning) return;
        elapsedTime += Time.deltaTime;
    }

    // =========================
    // CONTROL
    // =========================
    public void StartTimer()
    {
        elapsedTime = 0f;
        isRunning = true;
    }

    public void PauseTimer()
    {
        isRunning = false;
    }

    public void ResumeTimer()
    {
        isRunning = true;
    }

    public void StopTimer()
    {
        isRunning = false;
    }

    // =========================
    // GETTERS
    // =========================

    /// <summary>Tiempo total en segundos.</summary>
    public float GetElapsedTime()
    {
        return elapsedTime;
    }

    /// <summary>Tiempo formateado como mm:ss.cc</summary>
    public string GetFormattedTime()
    {
        return FormatTime(elapsedTime);
    }

    public static string FormatTime(float seconds)
    {
        int minutes = (int)(seconds / 60f);
        int secs    = (int)(seconds % 60f);
        int cents   = (int)((seconds - Mathf.Floor(seconds)) * 100f);
        return string.Format("{0:00}:{1:00}.{2:00}", minutes, secs, cents);
    }
}