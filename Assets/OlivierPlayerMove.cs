using UnityEngine;
using UnityEngine.InputSystem;

public class OlivierPlayerMove : MonoBehaviour
{

    public float moveSpeed = 50f;
    [Range(0, 1)]
    public float groundDamping = 0.97f;
    public float lookSpeed = 50f;
    public float jumpForce = 50f;
    public Vector2 pitchRange = new Vector2(-60, 50);

    public PlayerInput playerInput;
    private Camera cam;
    private Rigidbody rb;

    private Vector2 moveInput;
    private Vector2 lookInput;
    private float pitch;
    private LayerMask mask;

    void Start()
    {
        cam = Camera.main;
        rb = GetComponent<Rigidbody>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        playerInput.actions["Move"].performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        playerInput.actions["Move"].canceled += ctx => moveInput = Vector2.zero;

        playerInput.actions["Look"].performed += ctx => lookInput = ctx.ReadValue<Vector2>();
        playerInput.actions["Look"].canceled += ctx => lookInput = Vector2.zero;

        playerInput.actions["Jump"].started += ctx => Jump();
        playerInput.actions["Jump"].canceled += ctx => CancelJump();
    }

    // Update is called once per frame
    void Update()
    {
        pitch += -lookInput.y * Time.deltaTime * lookSpeed;
        pitch = Mathf.Clamp(pitch, pitchRange.x, pitchRange.y);

        cam.transform.localRotation = Quaternion.Euler(new Vector3(pitch, 0, 0));
        transform.rotation *= Quaternion.Euler(new Vector3(0, lookInput.x, 0) * Time.deltaTime * lookSpeed);
    }

    private bool isGrounded()
    {
        RaycastHit hit;
        Physics.Raycast(transform.position + Vector3.up, Vector3.down, out hit, 1.1f);
        return hit.collider != null;
    }

    private void FixedUpdate()
    {
        rb.linearVelocity += transform.forward * moveInput.y * moveSpeed;
        rb.linearVelocity += transform.right * moveInput.x * moveSpeed;
        rb.linearVelocity = new Vector3(rb.linearVelocity.x * groundDamping, rb.linearVelocity.y, rb.linearVelocity.z* groundDamping);
    }

    private void Jump()
    {
        if (isGrounded())
        {
            rb.mass = 1;
            rb.AddRelativeForce(new Vector3(0, 1, 0) * jumpForce, ForceMode.Impulse);
        }
    }

    private void CancelJump()
    {
        if (!isGrounded())
        {
            if(rb.linearVelocity.y > 0)
            {
                rb.linearVelocity = new Vector3(rb.linearVelocity.x, rb.linearVelocity.y*0.3f, rb.linearVelocity.z);
            }

            rb.mass = 10;
        }

    }

}
