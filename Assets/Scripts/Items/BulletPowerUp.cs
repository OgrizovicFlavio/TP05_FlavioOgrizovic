using UnityEngine;

public class BulletPowerUp : MonoBehaviour
{
    [SerializeField] private LayerMask playerLayerMask;
    [SerializeField] private GameObject pickUpEffect;

    private float duration = 10f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (Utilites.CheckLayerInMask(playerLayerMask, other.gameObject.layer))
        {
            PlayerController playerController = other.gameObject.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.BulletPowerUp(duration);
                Instantiate(pickUpEffect, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }
    }
}
