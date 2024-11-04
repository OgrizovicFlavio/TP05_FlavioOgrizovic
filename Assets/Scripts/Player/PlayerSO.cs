using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSO", menuName = "SideScroller/PlayerSO", order = 1)]

public class PlayerSO : ScriptableObject
{
    [Header("Player Movement")]
    [SerializeField] public float movementSpeed = 5f;
    [SerializeField] public float jumpForce = 10f;

    [Header("Player Stats")]
    [SerializeField] public float maxHealth = 100;

    [Header("Player Collisions")]
    [SerializeField] public float groundCheckRadius = 0.2f;
    [SerializeField] public LayerMask enemyLayerMask;
    [SerializeField] public LayerMask groundLayerMask;
}

