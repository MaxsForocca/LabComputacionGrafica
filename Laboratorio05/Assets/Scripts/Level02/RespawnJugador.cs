using UnityEngine;

public class RespawnJugador : MonoBehaviour
{
    private Vector3 posicionInicial;
    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        // Guardamos la posición exacta donde inicia el nivel
        posicionInicial = transform.position;
    }

    // Para CharacterController, es mejor usar OnControllerColliderHit 
    // ya que detecta las colisiones mientras el personaje se mueve.

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // Comprobamos si el objeto con el que chocamos tiene el Tag "Trampa"
        if (hit.gameObject.CompareTag("Trampa"))
        {
            EjecutarRespawn();
        }
    }

    // Por si acaso usas Triggers (colisionadores fantasmas) en algunas trampas
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Trampa"))
        {
            EjecutarRespawn();
        }
    }

    public void EjecutarRespawn()
    {
        Debug.Log("¡El jugador cayó en una trampa! Reiniciando...");
        
        // IMPORTANTE: Para teletransportar un objeto con CharacterController,
        // primero debemos apagarlo temporalmente, moverlo y volverlo a encender.
        if (controller != null)
        {
            controller.enabled = false;
            transform.position = posicionInicial;
            controller.enabled = true;
        }
        else
        {
            transform.position = posicionInicial;
        }
    }
}