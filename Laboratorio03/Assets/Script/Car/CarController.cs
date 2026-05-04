using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [Header("Size")]
    public float sizeMultiplier = 1f;
    public bool isGiant = false;

    [System.Serializable]
    public class infoEje
    {
        public WheelCollider ruedaIzquierda;
        public WheelCollider ruedaDerecha;
        public bool motor;
        public bool direccion;
    }

    public List<infoEje> infoEjes;

    public float maxMotorTorsion = 400f;
    public float maxAnguloGiro = 30f;

    // BUG FIX: Cacheamos el Rigidbody para poder resetear velocidades
    // sin buscarlo en cada frame.
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void posRuedas(WheelCollider rueda)
    {
        if (rueda.transform.childCount == 0) return;

        Transform ruedaVisual = rueda.transform.GetChild(0);
        Vector3 posicion;
        Quaternion rotacion;
        rueda.GetWorldPose(out posicion, out rotacion);

        ruedaVisual.transform.position = posicion;
        ruedaVisual.transform.rotation = rotacion;
    }

    private void FixedUpdate()
    {
        float motor = maxMotorTorsion * Input.GetAxis("Vertical");
        float direccion = maxAnguloGiro * Input.GetAxis("Horizontal");

        foreach (infoEje eje in infoEjes)
        {
            if (eje.direccion)
            {
                eje.ruedaIzquierda.steerAngle = direccion;
                eje.ruedaDerecha.steerAngle = direccion;
            }
            if (eje.motor)
            {
                eje.ruedaIzquierda.motorTorque = motor;
                eje.ruedaDerecha.motorTorque = motor;
            }

            posRuedas(eje.ruedaIzquierda);
            posRuedas(eje.ruedaDerecha);
        }
    }

    void Update()
    {
        SizeControl();
        ResetControl();
    }

    public void ChangeSize(float multiplier)
    {
        sizeMultiplier = multiplier;

        // BUG FIX: Primero reseteamos el carro (con velocidades a cero) ANTES
        // de cambiar la escala, así el motor de física no entra en conflicto
        // con la nueva escala del Rigidbody.
        ResetCar();

        transform.localScale = Vector3.one * sizeMultiplier;

        float growthFactor = 1f + (sizeMultiplier - 1f) * 0.25f;
        maxMotorTorsion = growthFactor * 400f;
    }

    public void SizeControl()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !isGiant)
        {
            ChangeSize(2f)      ;
            isGiant = true;
        }
        if (Input.GetKeyDown(KeyCode.E) && isGiant)
        {
            ChangeSize(1f);
            isGiant = false;
        }
    }

    public void ResetControl()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetCar();
        }
    }

    void ResetCar()
    {
        // BUG FIX: Antes de mover/rotar, zeroeamos las velocidades del Rigidbody.
        // Sin esto, el motor de física y la posición manual entran en conflicto
        // causando los tirones (jitter).
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        // Corregir rotación (enderezar el carro)
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);

        // BUG FIX: Levantamos el carro solo al hacer reset manual (R),
        // NO al cambiar de tamaño, para evitar teletransportes inesperados.
        // El levantamiento ahora es más suave (0.5f en vez de 2f).
        transform.position += Vector3.up * 0.5f;
    }
}
