using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movimiento")]
    public float moveSpeed = 7f;
    public float rotationSpeed = 100f;

    [Header("Referencias")]
    public Transform turret;
    public Transform cannon;

    [Header("Rotación")]
    public float turretSpeed = 50f;
    public float cannonSpeed = 25f;

    [Header("Límites del cañón")]
    public float minCannonAngle = -25f;
    public float maxCannonAngle = 10f;

    private float currentCannonAngle = 0f;

    [Header("Disparo")]
    public GameObject bulletPrefab;
    public Transform firePoint;

    public float minVelocity = 40f;
    public float maxVelocity = 100f;
    public float oscillationSpeed = 2f; // velocidad de oscilación
    public BarraVelocidad barraVelocidad; // referencia a la barra de velocidad
    private float currentMuzzleVelocity;
    private bool isCharging = false;
    private float chargeTime = 0f;

    [Header("VFX Disparo")]
    public GameObject muzzleFlashPrefab;
    
    public TrajectoryPredictor trajectory;

    public AudioClip shootSound;
    
    void Start()
    {
        if (barraVelocidad != null)
        {
            barraVelocidad.Initialize(minVelocity, maxVelocity);
        }
    }
    void Update()
    {
        if (PauseManager.Instance != null && PauseManager.Instance.IsPaused)
        return;
        HandleMovement();
        HandleTurretRotation();
        HandleCannonRotation();
        HandleShooting();
    }

    void HandleMovement()
    {
        float move = Input.GetAxis("Vertical");   // W/S
        float turn = Input.GetAxis("Horizontal"); // A/D

        // Movimiento adelante/atrás
        transform.Translate(0,0, move * moveSpeed * Time.deltaTime);

        // Rotación del tanque
        transform.Rotate(0, turn * rotationSpeed * Time.deltaTime,0);
    }

    void HandleTurretRotation()
    {
        float mouseX = Input.GetAxis("Mouse X");

        // Rotacion en Y (izquierda/derecha)
        turret.Rotate(Vector3.up * mouseX * turretSpeed * Time.deltaTime);
    }

    // Control de elevacion del cañón con Q/E
    void HandleCannonRotation()
    {
        float input = 0f;

        if (Input.GetKey(KeyCode.E)) input = -1f;   // subir
        if (Input.GetKey(KeyCode.Q)) input = 1f;  // bajar

        // Ajustar angulo
        currentCannonAngle += input * cannonSpeed * Time.deltaTime;

        // Limita el rango de angulo de rotacion del cañón
        currentCannonAngle = Mathf.Clamp(currentCannonAngle, minCannonAngle, maxCannonAngle);

        // Aplicar rotacion local en X
        cannon.localRotation = Quaternion.Euler(currentCannonAngle, 0f, 0f);
    }
    void HandleShooting()
    {
        // Mantener click → cargar
        if (Input.GetMouseButton(0))
        {
            isCharging = true;
            chargeTime += Time.deltaTime;

            // Oscilación tipo seno
            float t = (Mathf.Sin(chargeTime * oscillationSpeed) + 1f) / 2f;

            currentMuzzleVelocity = Mathf.Lerp(minVelocity, maxVelocity, t);
            trajectory.ShowTrajectory(currentMuzzleVelocity);

            barraVelocidad.SetVelocity(currentMuzzleVelocity);
        }

        // Soltar click -> disparar
        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log(currentMuzzleVelocity);
            isCharging = false;
            Shoot(currentMuzzleVelocity);

            trajectory.HideTrajectory();

            chargeTime = 0f;
        }
    }
    void Shoot(float velocity)
    {
        // VFX en FirePoint
        if (muzzleFlashPrefab != null)
        {
            GameObject vfx = Instantiate(muzzleFlashPrefab, firePoint.position, firePoint.rotation);
            Destroy(vfx, 2f); // limpieza automática
        }

        // Proyectil
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        AudioManager.Instance.PlaySFX(shootSound);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.velocity = firePoint.forward * velocity;
        }
    }
}
