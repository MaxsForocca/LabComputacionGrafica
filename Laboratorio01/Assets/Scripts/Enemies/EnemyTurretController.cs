using UnityEngine;

public class EnemyTurretController : MonoBehaviour
{
    [Header("Referencias")]
    public Transform player;
    public Transform turret;
    public Transform cannon;
    public Transform firePoint;

    [Header("Rotación")]
    public float turretRotationSpeed = 5f;
    public float cannonRotationSpeed = 5f;

    [Header("Disparo")]
    public GameObject bulletPrefab;
    public float fireRate = 1.5f;
    public float muzzleVelocity = 60f;

    [Header("Rango")]
    public float detectionRange = 50f;

    private float nextFireTime = 0f;

    [Header("VFX Disparo")]
    public GameObject muzzleFlashPrefab;

    [Header("Audio")]
    public AudioClip shootSound;
    private Health health;
    void Start()
    {
        health = GetComponent<Health>();
        if (CompareTag("Enemigo"))
        {
            GameManager.Instance.RegisterEnemy();
        }
    }
    void Update()
    {
        if (PauseManager.Instance != null && PauseManager.Instance.IsPaused)
        return;
        if (player == null) return;
        if (health != null && health.isDead) return;
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= detectionRange)
        {
            RotateTurret();
            AimCannon();

            if (Time.time >= nextFireTime)
            {
                Shoot();
                nextFireTime = Time.time + 1f / fireRate;
            }
        }
    }

    // Rotación horizontal (torreta)
    void RotateTurret()
    {
        Vector3 direction = player.position - turret.position;
        direction.y = 0f; // ignorar altura

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        turret.rotation = Quaternion.Slerp(
            turret.rotation,
            targetRotation,
            turretRotationSpeed * Time.deltaTime
        );
    }

    // Rotación vertical (cañón)
    void AimCannon()
    {
        Vector3 direction = player.position - cannon.position;

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        Vector3 angles = targetRotation.eulerAngles;

        // Normalizar ángulo X
        float xAngle = angles.x;
        if (xAngle > 180f) xAngle -= 360f;

        // Limitar elevación (opcional)
        xAngle = Mathf.Clamp(xAngle, -10f, 30f);

        Quaternion finalRotation = Quaternion.Euler(xAngle, turret.eulerAngles.y, 0f);

        cannon.rotation = Quaternion.Slerp(
            cannon.rotation,
            finalRotation,
            cannonRotationSpeed * Time.deltaTime
        );
    }

    // Disparo
    void Shoot()
    {
        // VFX en FirePoint
        if (muzzleFlashPrefab != null)
        {
            GameObject vfx = Instantiate(muzzleFlashPrefab, firePoint.position, firePoint.rotation);
            Destroy(vfx, 2f); // limpieza automática
        }
        AudioManager.Instance.PlaySFX(shootSound);
        GameObject bullet = Instantiate(
            bulletPrefab,
            firePoint.position,
            firePoint.rotation
        );

        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.velocity = firePoint.forward * muzzleVelocity;
        }
    }
}