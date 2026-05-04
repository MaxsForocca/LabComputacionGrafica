using UnityEngine;

public class Trap : MonoBehaviour
{
    public int damage = 15;
    public float bounceForce = 7f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerStats player =
                collision.gameObject.GetComponent<PlayerStats>();

            Rigidbody2D rb =
                collision.gameObject.GetComponent<Rigidbody2D>();

            player.TakeDamage(damage);

            // Rebote
            rb.velocity = new Vector2(
                rb.velocity.x,
                bounceForce
            );
        }
    }
}