using UnityEngine;

public class RollingTrap : MonoBehaviour
{
    public float force = 10f;       // Empuje inicial
    public float torque = 10f;      // Fuerza de rotación (Giro)
    public int damage = 20;
    public float bounceForce = 7f;
    
    private Rigidbody2D rb;
    private bool isRolling = false; // Bandera para activar solo una vez

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerStats player = collision.gameObject.GetComponent<PlayerStats>();
            player.TakeDamage(damage);

            // Rebote vertical
            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
            playerRb.velocity = new Vector2(playerRb.velocity.x, bounceForce);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Verificamos si es el jugador y si no ha empezado a rodar aún
        if (other.CompareTag("Player") && !isRolling)
        {
            isRolling = true; // Bloqueamos para que no se active de nuevo

            // ForceMode2D.Impulse aplica la fuerza de golpe instantáneo
            rb.AddForce(Vector2.left * force, ForceMode2D.Impulse);
            
            // AddTorque hace que el objeto rote (efecto de rodar)
            // Usamos un valor positivo o negativo dependiendo de la dirección
            rb.AddTorque(torque, ForceMode2D.Impulse);
        }
    }
}