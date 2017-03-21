using UnityEngine;

public class Controller : MonoBehaviour {
    Rigidbody rb;
    //check 1

    public float gravity = 9.81f;

    public float mass = 0.3f;
    public float moveAcceleration = 1f;
    public float maxMoveSpeed = 2.0f;
    public float slowdownTime = 0.3f;

    public float jumpForce = 20;
    public float jumpReact = 5;

    public float jumpTimeMargin = 0.1f;

    private float moveSpeed = 0f;

    private float lastInput;

    private bool elevating = false;

    //debug
    private Vector3 lastDiff;

    void Start () {
        lastInput = Time.time;
        lastDiff = Vector3.zero;

        rb = GetComponent<Rigidbody>();
    }

    void addGravity () {
        rb.AddForce(-transform.up * gravity);
    }

    void jump () {
        rb.AddForce(transform.up * gravity * jumpForce);
    }

    void processMove () {
        float zAxis = Input.GetAxis("Vertical");
        float xAxis = Input.GetAxis("Horizontal");

        moveSpeed += ( Mathf.Abs(xAxis) + Mathf.Abs(zAxis) ) * moveAcceleration * Time.deltaTime;
        moveSpeed = Mathf.Clamp(moveSpeed , -Mathf.Infinity , maxMoveSpeed);

        if ( Mathf.Abs(xAxis) > 0.2 || Mathf.Abs(zAxis) > 0.2 ) {
            lastInput = Time.time;
            Transform mainCamera = Camera.main.transform;

            Vector3 diff = ( mainCamera.up * zAxis + mainCamera.right * xAxis );
            diff.Normalize();
            diff = transform.InverseTransformDirection(diff);
            diff.y = 0;
            diff = transform.TransformDirection(diff);
            transform.LookAt(transform.position + diff , transform.up);
            lastDiff = diff;
        }//turn object around withought rotating camera

        Debug.DrawRay(transform.position , lastDiff , Color.blue);
        moveSpeed = Mathf.Lerp(moveSpeed , 0 , ( Time.time - lastInput ) / slowdownTime); //equivalent to drag
        if ( !elevating )
            rb.AddForce(transform.forward * moveSpeed);
    }

    void FixedUpdate () {
        addGravity();
        processMove();

        if ( Input.GetKeyDown("space") )
            jump();
    }
}
