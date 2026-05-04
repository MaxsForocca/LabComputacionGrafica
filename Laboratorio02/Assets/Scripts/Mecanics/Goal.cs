using UnityEngine;

public class Goal : MonoBehaviour
{
    public GameObject winPanel;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.Instance.WinGame();
            Debug.Log("¡Has ganado!");
        }
    }
}