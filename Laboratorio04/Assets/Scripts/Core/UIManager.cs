using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Panels")]
    public GameObject pausePanel;
    public GameObject winPanel;

    [Header("HUD")]
    public TMP_Text hudTime;           // Texto del cronómetro en el HUD (siempre visible)

    [Header("Pause Panel")]
    public TMP_Text pauseTimeText;     // Texto dentro del panel de pausa: "Tiempo actual: mm:ss.cc"

    [Header("Win Panel")]
    public TMP_Text winTimeText;       // Texto dentro del panel de victoria con el tiempo final

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        pausePanel.SetActive(false);
        winPanel.SetActive(false);
    }

    private void Update()
    {
        // Actualiza el HUD solo mientras se está jugando
        if (GameManager.Instance.IsPlaying())
        {
            hudTime.text = TimerManager.Instance.GetFormattedTime();
        }
    }

    // =========================
    // PANELES
    // =========================
    public void ShowPausePanel()
    {
        // Muestra el tiempo actual en el panel de pausa
        if (pauseTimeText != null)
            pauseTimeText.text = "Tiempo: " + TimerManager.Instance.GetFormattedTime();

        pausePanel.SetActive(true);
    }

    public void HidePausePanel()
    {
        pausePanel.SetActive(false);
    }

    /// <summary>Muestra el panel de victoria con el tiempo final registrado.</summary>
    public void ShowWinPanel(string finalTime)
    {
        if (winTimeText != null)
            winTimeText.text = "Tiempo final: " + finalTime;

        winPanel.SetActive(true);
    }

    // Sobrecarga sin parámetro por compatibilidad (no recomendada, usar la de arriba)
    public void ShowWinPanel()
    {
        ShowWinPanel(TimerManager.Instance.GetFormattedTime());
    }
}