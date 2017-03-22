using UnityEngine;

public class AlignToGravity : MonoBehaviour {
    RaycastHit hit1;

    void Update () {
        Physics.Raycast(transform.position , -transform.up , out hit1);
        transform.rotation = Quaternion.FromToRotation(transform.up , hit1.normal) * transform.rotation;

        Debug.DrawLine(transform.position, hit1.point, Color.green);
        Debug.DrawLine(hit1.point , hit1.normal , Color.red);
    }
}