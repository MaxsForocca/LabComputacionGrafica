using UnityEngine;

public class CamaraLateral25D : MonoBehaviour
{
    [Header("Objetivo")]
    public Transform jugador; // Arrastra aquí a tu personaje en el Inspector

    [Header("Configuración de Distancia")]
    [Tooltip("Distancia lateral en X desde donde la cámara mira el nivel")]
    public float distanciaX = 10f;
    
    [Tooltip("Desplazamiento vertical de la cámara respecto al jugador")]
    public float alturaY = 2f;
    
    [Tooltip("Desplazamiento en Z (por si quieres que la cámara vea un poco más adelante)")]
    public float desfaseZ = 0f;

    [Header("Suavizado")]
    public float velocidadSuavizado = 5f;

    void Start()
    {
        if (jugador == null)
        {
            Debug.LogError("Por favor, asigna el Transform del jugador a la cámara.");
            return;
        }
    }

    void LateUpdate()
    {
        if (jugador == null) return;

        // Definimos la posición ideal de la cámara manteniendo su distancia fija en X
        // pero persiguiendo los cambios del jugador en Y (salto) y Z (avance)
        Vector3 posicionObjetivo = new Vector3(
            jugador.position.x + distanciaX,
            jugador.position.y + alturaY,
            jugador.position.z + desfaseZ
        );

        // Interpolación lineal suave entre la posición actual de la cámara y la objetivo
        Vector3 posicionSuavizada = Vector3.Lerp(transform.position, posicionObjetivo, velocidadSuavizado * Time.deltaTime);
        transform.position = posicionSuavizada;

        // Forzar a la cámara a mirar siempre hacia la línea de posición del jugador
        Vector3 puntoMirada = new Vector3(jugador.position.x, jugador.position.y + alturaY, jugador.position.z);
        transform.LookAt(puntoMirada);
    }
}