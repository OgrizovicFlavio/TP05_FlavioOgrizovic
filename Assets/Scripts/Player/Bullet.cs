using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private BulletSO bulletSO;
    [SerializeField] private Rigidbody2D bulletRb2D;

    private float damage;
    private float damageMultiplier = 1f;

    void Start()
    {
        bulletRb2D.velocity = transform.right * bulletSO.speed;
        Destroy(gameObject, bulletSO.lifetime);
        damage = bulletSO.baseDamage;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (Utilites.CheckLayerInMask(bulletSO.enemyLayerMask, other.gameObject.layer))
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
