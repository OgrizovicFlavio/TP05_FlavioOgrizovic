using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform player;

    private Vector3 cameraOffset = new Vector3(0f, 0f, -10f);
    private float smoothTime = 0.25f;
    private Vector3 velocity = Vector3.zero;

    private void Update()
    {
        Vector3 playerPosition = player.position + cameraOffset;
        transform.position = Vector3.SmoothDamp(transform.position, playerPosition, ref velocity, smoothTime); //Mueve suavemente la cámara hacia la posición del jugador.
    }
}
