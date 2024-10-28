using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class DeathPoint : MonoBehaviour
{
    [SerializeField] private LayerMask playerLayerMask;
    [SerializeField] private CinemachineVirtualCamera cmCamera;

    private float reloadSceneTime = 2f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(Utilites.CheckLayerInMask(playerLayerMask, other.gameObject.layer))
        {
            PlayerController playerController = other.gameObject.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.Die();
                StopCameraFollow();
                StartCoroutine(ReloadScene());
            }
        }
    }

    private void StopCameraFollow()
    {
        cmCamera.Follow = null;
    }

    private IEnumerator ReloadScene()
    {
        yield return new WaitForSeconds(reloadSceneTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
