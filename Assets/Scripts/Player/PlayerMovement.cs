using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Canvas canvas;

    public PlayerSO playerSO;
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
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, playerSO.groundCheckRadius, playerSO.groundLayerMask);
        }
    }

    public void Jump()
    {
        if (isGrounded)
        {
            playerRb2D.AddForce(new Vector2(0f, playerSO.jumpForce), ForceMode2D.Impulse);
            isGrounded = false;
            disableTime = 0.1f;
        }
    }

    public void Move(float moveInput)
    {
        playerRb2D.velocity = new Vector2(moveInput * playerSO.movementSpeed, playerRb2D.velocity.y);

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
