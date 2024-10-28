using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    [SerializeField] private LayerMask playerLayerMask;
    [SerializeField] private GameObject pickUpEffect;

    private float healthPoints = 25;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (Utilites.CheckLayerInMask(playerLayerMask, other.gameObject.layer))
        {
            PlayerController playerController = other.gameObject.GetComponent<PlayerController>();
            if (playerController != null)
            {
                PickUp(playerController);
            }
        }
    }

    private void PickUp(PlayerController playerController)
    {
        Instantiate(pickUpEffect, transform.position, transform.rotation);
        playerController.Heal(healthPoints);
        Destroy(gameObject);
    }
}
