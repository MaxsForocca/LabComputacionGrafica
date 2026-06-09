using UnityEngine;

public class LevelTrigger : MonoBehaviour
{
    [SerializeField] private string targetSceneName;

    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que entró es el jugador (requiere el Tag "Player")
        if (other.CompareTag("Player"))
        {
            // Llama al Singleton para cargar el nivel de forma segura
            LevelManager.Instance.LoadLevel(targetSceneName);
        }
    }
}