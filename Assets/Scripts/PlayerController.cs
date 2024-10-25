using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float health = 100;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private Image healthBar;
    [SerializeField] private LayerMask enemyLayerMask;

    private float horizontalInput;
    private PlayerMovement playerMovement;
    private Weapon weapon;

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        weapon = GetComponent<Weapon>();
    }

    private void Update()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");
        playerMovement.Move(moveInput);
        if (Input.GetButtonDown("Jump") && playerMovement.IsGrounded())
        {
            playerMovement.Jump();
        }
        if (Input.GetButtonDown("Fire1"))
        {
            weapon.Shoot();
        }

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (Utilites.CheckLayerInMask(enemyLayerMask, other.gameObject.layer))
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();

            if (enemy != null)
            {
                TakeDamage(enemy.GetDamage());
            }
        }
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        healthBar.fillAmount -= (amount / 100);

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    public void Heal(float amount)
    {
        health += amount;
        healthBar.fillAmount += (amount / 100);

        if (health > 100)
        {
            health = 100;
        }
    }
}
