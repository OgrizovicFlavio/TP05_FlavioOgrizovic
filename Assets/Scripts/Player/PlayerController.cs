using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public enum PlayerState
{
    Idle,
    Move,
    Jump,
    Shoot,
    Hurt,
    Die
}

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float health = 100;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private Image healthBar;
    [SerializeField] private LayerMask enemyLayerMask;
    [SerializeField] private PlayerState playerState;
    [SerializeField] private PlayerState lastPlayerState;


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
        if (Time.timeScale == 0)
        {
            return;
        }
        float moveInput = Input.GetAxisRaw("Horizontal");
        playerMovement.Move(0);
        switch (playerState)
        {
            case PlayerState.Idle:
                //START
                if (playerState != lastPlayerState)
                {
                    lastPlayerState = playerState;
                    playerAnimator.SetInteger("State", 0);
                }
                //UPDATE
                if (moveInput != 0)
                {
                    playerState = PlayerState.Move;
                }
                else if (Input.GetButtonDown("Jump") && playerMovement.IsGrounded())
                {
                    playerState = PlayerState.Jump;
                }
                else if (Input.GetButtonDown("Fire1") && playerMovement.IsGrounded())
                {
                    playerState = PlayerState.Shoot;
                }
                    break;
            case PlayerState.Move:
                //START
                if (playerState != lastPlayerState)
                {
                    lastPlayerState = playerState;
                    playerAnimator.SetInteger("State", 1);
                }
                //UPDATE
                playerMovement.Move(moveInput);

                if (Mathf.Abs(moveInput) < 0.01f)
                {
                    playerState = PlayerState.Idle;
                }
                else if (Input.GetButtonDown("Jump") && playerMovement.IsGrounded())
                {
                    playerState = PlayerState.Jump;
                }
                else if (Input.GetButtonDown("Fire1") && playerMovement.IsGrounded())
                {
                    playerState = PlayerState.Shoot;
                }
                break;
            case PlayerState.Jump:
                //START
                if (playerState != lastPlayerState)
                {
                    lastPlayerState = playerState;
                    playerAnimator.SetInteger("State", 2);
                    playerMovement.Jump();
                }
                //UPDATE
                playerMovement.Move(moveInput);
                if (playerMovement.IsGrounded())
                {
                    if (moveInput != 0)
                    {
                        playerState = PlayerState.Move;
                    }
                    else
                    {
                        playerState = PlayerState.Idle;
                    }
                }
                break;
            case PlayerState.Shoot:
                //START
                if (playerState != lastPlayerState)
                {
                    lastPlayerState = playerState;
                    playerAnimator.SetInteger("State", 3);
                    weapon.Shoot();
                    Invoke(nameof(ChangeToIdle), 0.5f);
                }
                //UPDATE
                break;
            case PlayerState.Hurt:
                //START
                if (playerState != lastPlayerState)
                {
                    lastPlayerState = playerState;
                    playerAnimator.SetInteger("State", 4);
                    Invoke(nameof(ChangeToIdle), 0.5f);
                }
                //UPDATE
                playerMovement.Move(moveInput/2);
                break;
            case PlayerState.Die:
                //START
                if (playerState != lastPlayerState)
                {
                    lastPlayerState = playerState;
                    playerAnimator.SetInteger("State", 5);
                }
                //UPDATE
                if (Input.GetKeyDown(KeyCode.R))
                {
                    playerState = PlayerState.Idle;
                    Heal(100);
                }
                break;
            default:
                playerState = PlayerState.Idle;
                Debug.LogWarning("¡DEFAULT STATE!");
                break;
        }
    }

    private void ChangeToIdle()
    {
        playerState = PlayerState.Idle;
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
        playerState = PlayerState.Hurt;
        health -= amount;
        healthBar.fillAmount = (health / 100);

        if (health < 1)
        {
            health = 0;
            Die();
        }
    }

    public void Die()
    {
        playerState = PlayerState.Die;
    }

    public void Heal(float amount)
    {
        health += amount;
        healthBar.fillAmount = (health / 100);

        if (health > 100)
        {
            health = 100;
        }
    }

    public void BulletPowerUp(float duration)
    {
        StartCoroutine(PickUp(duration));
    }

    private IEnumerator PickUp(float duration)
    {
        if (weapon != null)
        {
            weapon.SetBulletScaleMultiplier(2f);
            weapon.SetBulletDamageMultiplier(2f);
        }

        yield return new WaitForSeconds(duration);

        if (weapon != null)
        {
            weapon.SetBulletScaleMultiplier(1f);
            weapon.SetBulletDamageMultiplier(1f);
        }
    }
}
