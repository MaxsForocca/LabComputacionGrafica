using UnityEngine;

public class TrampaCuchilla : MonoBehaviour
{
    public float velocidadRotacion = 250f;

    void Update()
    {
        // Gira en el eje Y positivo (0, 1, 0)
        transform.Rotate(Vector3.up * velocidadRotacion * Time.deltaTime);
    }
}