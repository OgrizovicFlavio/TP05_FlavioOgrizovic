using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private Canvas canvas;

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D playerRb2D;
    private bool isGrounded;
    private bool facingRight = true;
    private float disableTime = 0.1f;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerRb2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        disableTime -= Time.deltaTime;
        if (disableTime < 0f)
        {
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayerMask);
        }
    }

    public void Jump()
    {
        if (isGrounded)
        {
            playerRb2D.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            isGrounded = false;
            disableTime = 0.1f;
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
        transform.Rotate(0f, 180f, 0f);
        canvas.transform.Rotate(0f, 180f, 0f);
    }

    public bool IsGrounded()
    {
        return isGrounded;
    }
}
