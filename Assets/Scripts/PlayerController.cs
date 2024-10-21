using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator playerAnimator;
    private PlayerMovement playerMovement;
    private float horizontalInput;

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");

        playerMovement.Move(moveInput);

        playerAnimator.SetFloat("Speed", Mathf.Abs(moveInput)); //Me aseguro que el valor de moveInput siempre sea positivo para que entre la animación.

        if (playerMovement.IsGrounded())
        {
            playerAnimator.SetBool("IsJumping", false);
        }
        else
        {
            playerAnimator.SetBool("IsJumping", true);
        }

        if (Input.GetButtonDown("Jump") && playerMovement.IsGrounded())
        {
            playerMovement.Jump();
        }
    }
}
