using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float damage = 25;
    [SerializeField] private float damageMultiplier = 1f;
    [SerializeField] private Rigidbody2D bulletRb2D;
    [SerializeField] private LayerMask enemyLayerMask;

    void Start()
    {
        bulletRb2D.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (Utilites.CheckLayerInMask(enemyLayerMask, other.gameObject.layer))
        {
            Enemy enemy = other.GetComponent<Enemy>();

            if (enemy != null)
            {
                float finalDamage = damage * damageMultiplier;
                enemy.TakeDamage(finalDamage);
            }
            Destroy(gameObject);
        }
    }
    public void SetDamageMultiplier(float multiplier)
    {
        damageMultiplier = multiplier;
    }
}
