using UnityEngine;

public class Controller : MonoBehaviour {
    Rigidbody rb;
    Vector3 input;

    public float gravity;
    public float jumpForce;
    public float moveForce;

    void processMove () {
        input.Set(Input.GetAxis("Horizontal") , 0 , Input.GetAxis("Vertical"));

        //turn object around withought rotating camera
        Transform mainCamera = Camera.main.transform;      
        Vector3 diff = ( mainCamera.up * input.z + mainCamera.right * input.x ).normalized;
        diff = transform.InverseTransformDirection(diff);
        diff.y = 0;
        diff = transform.TransformDirection(diff);
        transform.LookAt(transform.position + diff , transform.up);

        rb.AddForce(transform.forward * moveForce * Mathf.Clamp01(input.magnitude));
    }

    void useGravity () {
        rb.AddForce(-transform.up * gravity * rb.mass);
    }

    void jump () {
        rb.AddForce(transform.up * jumpForce);
    }

    void Start () {
        rb = GetComponent<Rigidbody>();
        input = new Vector3();
    }

    void FixedUpdate () {
        useGravity();
        processMove();

        if ( Input.GetKeyDown("space") )
            jump();
    }
}
