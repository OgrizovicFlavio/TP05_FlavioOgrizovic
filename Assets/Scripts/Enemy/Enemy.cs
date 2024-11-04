using System;
using UnityEngine;
using UnityEngine.UI;

public enum EnemyState
{
    Idle,
    Hurt
}

public class Enemy : MonoBehaviour
{
    public static event Action<Vector3> onEnemyDeath;

    [SerializeField] private EnemySO enemySO;
    [SerializeField] private Image healthBar;
    [SerializeField] private Animator enemyAnimator;
    [SerializeField] private EnemyState enemyState;
    [SerializeField] private EnemyState lastEnemyState;

    private AudioManager audioManager;
    private float health;
    private float damage;

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        health = enemySO.maxHealth;
        damage = enemySO.baseDamage;
    }

    private void Update()
    {
        if (Time.timeScale == 0)
        {
            return;
        }
        switch (enemyState)
        {
            case EnemyState.Idle:
                if (enemyState != lastEnemyState)
                {
                    lastEnemyState = enemyState;
                    enemyAnimator.SetInteger("State", 0);
                }
                break;
            case EnemyState.Hurt:
                if (enemyState != lastEnemyState)
                {
                    lastEnemyState = enemyState;
                    enemyAnimator.SetInteger("State", 1);
                    audioManager.Play("EnemyHurt");
                    Invoke(nameof(ChangeToIdle), 0.5f);
                }
                break;
            default:
                enemyState = EnemyState.Idle;
                Debug.LogWarning("¡DEFAULT STATE!");
                break;
        }
    }

    private void ChangeToIdle()
    {
        enemyState = EnemyState.Idle;
    }

    public void TakeDamage(float amount)
    {
        enemyState = EnemyState.Hurt;
        health -= amount;
        healthBar.fillAmount = (health / 100);

        if (health < 1)
        {
            health = 0;
            Die();
        }
    }

    private void Die()
    {
        audioManager.Play("EnemyDie");
        onEnemyDeath?.Invoke(transform.position);
        Destroy(gameObject);
    }

    public float GetDamage()
    {
        return damage;
    }
}
