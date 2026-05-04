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
    public Slider healthSlider;
    public TMP_Text coinsText;

    private int currentCoins = 0;

    private void Awake()
    {
        // Singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        pausePanel.SetActive(false);
        winPanel.SetActive(false);

        UpdateCoins(0);
    }

    // =========================
    // VIDA
    // =========================

    public void SetMaxHealth(int maxHealth)
    {
        healthSlider.maxValue = maxHealth;
        healthSlider.value = maxHealth;
    }

    public void UpdateHealth(int health)
    {
        healthSlider.value = health;
        Debug.Log("UI actualizada");
    }

    // =========================
    // MONEDAS
    // =========================

    public void AddCoin(int amount)
    {
        currentCoins += amount;

        UpdateCoins(currentCoins);
    }

    void UpdateCoins(int coins)
    {
        coinsText.text = coins.ToString();
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