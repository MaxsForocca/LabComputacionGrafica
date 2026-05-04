using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Referencia")]
    public Transform player;

    [Header("Configuración de Suavizado")]
    [Tooltip("Tiempo que tarda la cámara en alcanzar al jugador. Menor = más rápido.")]
    public float smoothTime = 0.3f;
    private Vector3 velocity = Vector3.zero;

    [Header("Límites de Cámara")]
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    // Usamos LateUpdate para que la cámara se mueva DESPUÉS de que el jugador se haya movido
    void LateUpdate()
    {
        if (player == null) return;

        // 1. Obtener la posición objetivo basada en el jugador
        Vector3 targetPosition = new Vector3(player.position.x, player.position.y, transform.position.z);

        // 2. Limitar la posición (Clamping)
        targetPosition.x = Mathf.Clamp(targetPosition.x, minX, maxX);
        targetPosition.y = Mathf.Clamp(targetPosition.y, minY, maxY);

        // 3. Mover la cámara suavemente
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }

    // Dibujar los límites en la vista de Escena para configurarlos fácilmente
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        // Dibujar un recuadro que representa los límites
        Vector3 center = new Vector3((minX + maxX) / 2, (minY + maxY) / 2, 0);
        Vector3 size = new Vector3(maxX - minX, maxY - minY, 0);
        Gizmos.DrawWireCube(center, size);
    }
}