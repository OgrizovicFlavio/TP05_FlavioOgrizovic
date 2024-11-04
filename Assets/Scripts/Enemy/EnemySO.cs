using UnityEngine;

[CreateAssetMenu(fileName = "EnemySO", menuName = "SideScroller/EnemySO", order = 3)]

public class EnemySO : ScriptableObject
{
    [Header("Enemy Stats")]
    [SerializeField] public float maxHealth = 100;
    [SerializeField] public float baseDamage = 25;
}
