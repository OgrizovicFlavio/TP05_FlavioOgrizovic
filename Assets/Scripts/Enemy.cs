using System;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public static event Action <Vector3> onEnemyDeath;

    [SerializeField] private float health = 100;
    [SerializeField] private float damage = 25;
    [SerializeField] private Image healthBar;
    [SerializeField] private Animator enemyAnimator;

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
        onEnemyDeath?.Invoke(transform.position);
        Destroy(gameObject);
    }

    public float GetDamage()
    {
        return damage;
    }
}
