using UnityEngine;
using UnityEngine.UI;
public class EnemyUI : MonoBehaviour
{
    public BarraDeVida barraDeVida;

    void Start()
    {
        Health health = GetComponent<Health>();

        if (health != null && barraDeVida != null)
        {
            barraDeVida.Initialize(health);

            health.OnHealthChanged += (current, max) =>
            {
                barraDeVida.SetHealth(current);
            };
        }
    }
    /*void LateUpdate()
    {
        transform.forward = Camera.main.transform.forward;
    }*/
}