using UnityEngine;

[CreateAssetMenu(fileName = "BulletSO", menuName = "SideScroller/BulletSO", order = 2)]

public class BulletSO : ScriptableObject
{
    [Header("Bullet Settings")]
    [SerializeField] public float speed = 10f;
    [SerializeField] public float baseDamage = 25;
    [SerializeField] public float lifetime = 1f;

    [Header("Bullet Collisions")]
    [SerializeField] public LayerMask enemyLayerMask;
}
