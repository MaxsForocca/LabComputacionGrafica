using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private BarraDeVida barraDeVida;

    void Start()
    {
        Health health = GetComponent<Health>();

        if (barraDeVida != null && health != null)
        {
            barraDeVida.Initialize(health);

            health.OnHealthChanged += (current, max) =>
            {
                barraDeVida.SetHealth(current);
            };
        }
    }
}