using UnityEngine;

public class AlignToGravity : MonoBehaviour {
    public Transform followCam;
    RaycastHit hit;

    void FixedUpdate () {
        Physics.Raycast(transform.position , -transform.up , out hit);
        transform.rotation = getUpRotation(transform , hit);
        followCam.rotation = getUpRotation(followCam , hit);

        Debug.DrawLine(transform.position, hit.point, Color.green);
        Debug.DrawLine(hit.point , hit.normal , Color.red);
    }

    void LateUpdate () {
        followCam.position = transform.position;
    }

    Quaternion getUpRotation (Transform position, RaycastHit floor) {
        return Quaternion.FromToRotation(position.up , floor.normal) * position.rotation;
    }
}