using UnityEngine;

public class TrampaSierra : MonoBehaviour
{
    public float velocidadRotacion = 500f;
    public float velocidadMovimiento = 3f;
    
    private float posicionInicialZ;
    private int direccion = 1; // 1 = Adelante, -1 = Atrás

    void Start()
    {
        // Guardamos su coordenada Z de origen
        posicionInicialZ = transform.localPosition.z;
    }

    void Update()
    {
        // 1. Rotación constante en el eje X positivo (1, 0, 0)
        transform.Rotate(Vector3.right * velocidadRotacion * Time.deltaTime);

        // 2. Movimiento de vaivén en el eje Z local
        float nuevoZ = transform.localPosition.z + (direccion * velocidadMovimiento * Time.deltaTime);
        
        // Comprobar los límites de desvío [-1, 1] respecto a su posición original
        if (nuevoZ >= posicionInicialZ + 1f)
        {
            nuevoZ = posicionInicialZ + 1f;
            direccion = -1; // Cambia de rumbo hacia atrás
        }
        else if (nuevoZ <= posicionInicialZ - 1f)
        {
            nuevoZ = posicionInicialZ - 1f;
            direccion = 1; // Cambia de rumbo hacia adelante
        }

        // Aplicar la nueva posición manteniendo fijos X e Y
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, nuevoZ);
    }
}