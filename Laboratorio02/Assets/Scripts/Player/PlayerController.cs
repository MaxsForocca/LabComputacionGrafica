using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerStats stats;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundRadius = 0.5f;
    public LayerMask groundLayer;

    private bool isGrounded; // Parametro para verificar si el jugador está en el suelo (Animator)

    [Header("Movement Settings")]
    public float airControlMultiplier = 0.5f; // Control en el aire
    private float moveInput; // Parametro para el movimiento horizontal (Animator)

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        stats = GetComponent<PlayerStats>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!GameManager.Instance.IsPlaying())
        return;
        
        moveInput = Input.GetAxisRaw("Horizontal");

        Jump();

        Flip();
        
        SizeControl();
        
        Animate();
    }

    private void FixedUpdate()
    {
        if (!GameManager.Instance.IsPlaying())
        return;

        Move();
        Jump();
    }

    void Move()
    {
        float targetSpeed = moveInput * stats.moveSpeed;

        if(!isGrounded)
        {
            targetSpeed *= airControlMultiplier;
        }
        rb.velocity = new Vector2(
            targetSpeed,
            rb.velocity.y
        );
        // Debug.Log("Function Move -> Velocidad Actual: " + rb.velocity.x);
    }

    void Jump()
    {
        isGrounded = Physics2D.OverlapCircle(
            groundCheck.position,
            groundRadius,
            groundLayer
        );
        //  Debug.Log("Function Jump -> Is Grounded: " + isGrounded);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(
                rb.velocity.x,
                stats.jumpForce
            );
        }
        // Debug.Log("Function Jump -> Velocidad Actual: " + rb.velocity.y);
    }

    void Flip()
    {
        if (moveInput > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (moveInput < 0)
        {
            spriteRenderer.flipX = true;
        }
    }

    void Animate()
    {
        animator.SetFloat("Speed", Mathf.Abs(moveInput));
        animator.SetBool("Grounded", isGrounded);
        animator.SetFloat("VelocityY", rb.velocity.y);
    }

    void SizeControl()
    {
        if (Input.GetKeyDown(KeyCode.W)  && !stats.isGiant)
        {
            stats.ChangeSize(2f);
            stats.isGiant = true;
        }
        if (Input.GetKeyDown(KeyCode.S) && stats.isGiant)
        {
            stats.ChangeSize(1f);
            stats.isGiant = false;
        } 
    }
    
    private void OnDrawGizmosSelected()
{
    if (groundCheck != null)
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
    }
}
}