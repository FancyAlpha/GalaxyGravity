using UnityEngine;

public class CameraController : MonoBehaviour {
    public Transform player;

    void LateUpdate () {
        transform.position = player.position;
    }
}
