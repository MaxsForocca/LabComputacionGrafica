using UnityEngine;

public class CamaraTopDown25D : MonoBehaviour
{
    [Header("Objetivo")]
    public Transform jugador; // Arrastra aquí a tu personaje 3D

    [Header("Configuración de Posición Isométrica")]
    [Tooltip("Qué tan arriba se posiciona la cámara respecto al jugador")]
    public float alturaY = 10f;

    [Tooltip("Desplazamiento hacia atrás en Z para lograr la inclinación hacia el frente")]
    public float desfaseZ = -6f;

    [Tooltip("Desplazamiento hacia un lado en X (déjalo en 0 si quieres vista cenital pura, o auméntalo para vista isométrica angular)")]
    public float desfaseX = -6f;

    [Header("Suavizado")]
    public float velocidadSuavizado = 5f;

    void LateUpdate()
    {
        if (jugador == null) return;

        // Calculamos la posición ideal en la que debería estar la cámara sumando los desfases
        Vector3 posicionObjetivo = new Vector3(
            jugador.position.x + desfaseX,
            jugador.position.y + alturaY,
            jugador.position.z + desfaseZ
        );

        // Interpolación lineal suave para que la cámara no se mueva con tirones
        Vector3 posicionSuavizada = Vector3.Lerp(transform.position, posicionObjetivo, velocidadSuavizado * Time.deltaTime);
        transform.position = posicionSuavizada;

        // Obligamos a la cámara a mirar fijamente hacia el centro del cuerpo del jugador
        transform.LookAt(jugador.position);
    }
}