using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Panels")]
    public GameObject pausePanel;
    public GameObject winPanel;

    [Header("HUD")]
    public TMP_Text collectibleText;

    private int totalCollectibles = 0;
    private int currentCollectibles = 0;

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

        // BUG FIX: No llamamos UpdateCollectibles(0) aquí porque los coleccionables
        // ya habrán registrado su total en Start(). Actualizamos el texto con los
        // valores reales que ya están en las variables.
        RefreshText();
    }

    // =========================
    // COLECCIONABLES
    // =========================

    public void RegistryCollectible()
    {
        totalCollectibles++;
        // BUG FIX: Actualizamos el texto cada vez que se registra un coleccionable,
        // así el total mostrado es siempre correcto desde el inicio.
        RefreshText();
    }

    public void AddCollectible(int amount)
    {
        currentCollectibles += amount;
        RefreshText();
    }

    // BUG FIX: Método centralizado para actualizar el texto, evita inconsistencias.
    void RefreshText()
    {
        collectibleText.text = "Estrellas: " + currentCollectibles + "/" + totalCollectibles;

        if (totalCollectibles > 0 && currentCollectibles >= totalCollectibles)
        {
            GameManager.Instance.WinGame();
        }
    }

    // =========================
    // PANELES
    // =========================

    public void ShowPausePanel()
    {
        pausePanel.SetActive(true);
    }

    public void HidePausePanel()
    {
        pausePanel.SetActive(false);
    }

    public void ShowWinPanel()
    {
        winPanel.SetActive(true);
    }
}
