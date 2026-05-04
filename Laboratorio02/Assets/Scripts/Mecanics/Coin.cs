using UnityEngine;

public class Coin : MonoBehaviour
{
    public int value = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Moneda recogida: +" + value);
            UIManager.Instance.AddCoin(value);

            Destroy(gameObject);
        }
    }
}