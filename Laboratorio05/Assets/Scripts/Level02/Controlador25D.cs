using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Controlador25D : MonoBehaviour
{
    [Header("Movimiento")]
    public float velocidad = 7.0f;
    public float fuerzaSalto = 8.0f;
    public float gravedad = 20.0f;

    [Header("Restricción de Eje")]
    [Tooltip("Posición fija en X para mantener el juego en 2.5D")]
    public float posicionFijaX = 0f;

    private CharacterController controller;
    private Vector3 direccionMovimiento = Vector3.zero;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        
        // Posicionar inicialmente al personaje en el eje X bloqueado
        Vector3 posInicial = transform.position;
        posInicial.x = posicionFijaX;
        transform.position = posInicial;
    }

    void Update()
    {
        // 1. Obtener la entrada horizontal (Eje Z en el mundo)
        // Usamos el input tradicional de Unity (Flechas / A-D / Joystick)
        float entradaHorizontal = Input.GetAxis("Horizontal");

        if (controller.isGrounded)
        {
            // Si está en el suelo, calculamos el movimiento en Z
            direccionMovimiento = new Vector3(0, 0, entradaHorizontal * velocidad);

            // Rotar visualmente al personaje hacia la dirección en la que camina
            if (entradaHorizontal != 0)
            {
                float anguloRotacion = entradaHorizontal > 0 ? 0f : 180f;
                transform.rotation = Quaternion.Euler(0, anguloRotacion, 0);
            }

            // 2. Manejo del Salto (Eje Y)
            if (Input.GetButtonDown("Jump"))
            {
                direccionMovimiento.y = fuerzaSalto;
            }
        }
        else
        {
            // Si está en el aire, permitimos que siga controlando su dirección en Z
            direccionMovimiento.z = entradaHorizontal * velocidad;
        }

        // 3. Aplicar Gravedad constantemente
        direccionMovimiento.y -= gravedad * Time.deltaTime;

        // 4. Ejecutar el movimiento a través del CharacterController
        controller.Move(direccionMovimiento * Time.deltaTime);

        // 5. Hard Lock estricto en X para evitar desvíos por colisiones físicas residuales
        Vector3 posicionActual = transform.position;
        if (posicionActual.x != posicionFijaX)
        {
            posicionActual.x = posicionFijaX;
            transform.position = posicionActual;
        }
    }
}