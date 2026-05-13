using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
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
        ResetControl();
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
        
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        // Corregir rotación (enderezar el carro)
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);

        
        transform.position += Vector3.up * 0.5f;
    }
}
