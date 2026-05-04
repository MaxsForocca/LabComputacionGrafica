using UnityEngine;
using UnityEngine.UI;

public class BarraDeVida : MonoBehaviour
{
    private Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();

        if (slider == null)
        {
            Debug.LogError("No se encontró Slider en BarraDeVida");
        }
    }

    public void SetMaxHealth(int maxHealth)
{
    slider.maxValue = maxHealth;
}

public void SetHealth(int currentHealth)
{
    slider.value = currentHealth;
}
    public void Initialize(Health health)
    {
        if (health != null)
        {
            SetMaxHealth(health.maxHealth);
            SetHealth(health.currentHealth);
        }
    }
}