using UnityEngine;
using UnityEngine.SceneManagement;
class PlayerStats : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float jumpForce = 7f;

    [Header("Health")]
    public int maxHealth = 100;
    public int currentHealth;

    [Header("Size")]
    public float sizeMultiplier = 1f;
    public bool isGiant = false;

    //private void Awake()
    private void Start()
    {
        InitializeHealth();
    }
    void InitializeHealth()
    {
        currentHealth = maxHealth;

        UIManager.Instance.SetMaxHealth(maxHealth);

        UIManager.Instance.UpdateHealth(currentHealth);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        UIManager.Instance.UpdateHealth(currentHealth);
        Debug.Log("Vida Actual: " + currentHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Player has died.");
        // Reiniciar el nivel
        Time.timeScale = 1f;

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }

    public void ChangeSize(float multiplier)
    {
        sizeMultiplier = multiplier;
        transform.localScale = Vector3.one * sizeMultiplier;

        float growthFactor = 1f + (sizeMultiplier - 1f) * 0.25f;
        // Ajuste de Stats basados en el tamaño
        moveSpeed = growthFactor * 5f;
        jumpForce = growthFactor * 7f;
    }
}