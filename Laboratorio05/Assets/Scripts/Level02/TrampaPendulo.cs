using UnityEngine;

public class TrampaPendulo : MonoBehaviour
{
    public float velocidadPendulo = 3.0f;
    public float anguloMaximo = 45.0f; // El límite de -45 a 45 grados

    void Update()
    {
        // Mathf.Sin nos devuelve un valor oscilante y fluido entre -1 y 1
        float tiempo = Time.time * velocidadPendulo;
        float anguloZ = Mathf.Sin(tiempo) * anguloMaximo;

        // Aplicamos la rotación en Z conservando las rotaciones originales de X e Y
        transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x, transform.localRotation.eulerAngles.y, anguloZ);
    }
}