using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public bool isDead = false;
    public System.Action<int, int> OnHealthChanged;
    void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        if (isDead) return;

        currentHealth -= amount;
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
        Debug.Log(gameObject.name + " vida: " + currentHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;

        Debug.Log(gameObject.name + " ha muerto");

        // Notificar según tipo
        if (CompareTag("Player"))
        {
            GameManager.Instance.PlayerDied();
        }
        if (CompareTag("Enemigo"))
        {
            GameManager.Instance.EnemyDefeated();
        }
    }
}