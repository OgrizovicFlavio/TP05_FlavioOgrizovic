using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;

    private Rigidbody2D playerRb2D;
    private bool isGrounded;
    private bool facingRight = true;

    private void Start()
    {
        playerRb2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    public void Jump()
    {
        if (isGrounded)
        {
            playerRb2D.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }
    }

    public void Move(float moveInput)
    {
        playerRb2D.velocity = new Vector2(moveInput * movementSpeed, playerRb2D.velocity.y);

        if (moveInput > 0 && !facingRight)
        {
            Flip();
        }
        else if (moveInput < 0 && facingRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    public bool IsGrounded()
    {
        return isGrounded;
    }
}
