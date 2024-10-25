using System.Collections;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private LayerMask playerLayerMask;
    [SerializeField] private GameObject pickUpEffect;

    private float duration = 5f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController playerController = other.gameObject.GetComponent<PlayerController>();
        if (playerController != null)
        {
            StartCoroutine(PickUp(playerController));
        }
    }

    private IEnumerator PickUp(PlayerController playerController)
    {
        Instantiate(pickUpEffect, transform.position, transform.rotation);
        Weapon weapon = playerController.GetComponent<Weapon>();

        if (weapon != null)
        {
            weapon.SetBulletScaleMultiplier(2f);
            weapon.SetBulletDamageMultiplier(2f);
        }
        /*GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;*/
        yield return new WaitForSeconds(duration);
        weapon.SetBulletScaleMultiplier(1f);
        weapon.SetBulletDamageMultiplier(1f);

        Destroy(gameObject);
    }
}
