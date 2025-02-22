using UnityEngine;
using UnityEngine.InputSystem;

public class JakePlayerMove : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float lookSpeed = 50f;
    public Vector2 pitchRange = new Vector2(-60, 50);

    public PlayerInput playerInput;
    private Camera cam;

    private Vector2 moveInput;
    private Vector2 lookInput;
    private float pitch;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = Camera.main;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        playerInput.actions["Move"].performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        playerInput.actions["Move"].canceled += ctx => moveInput = Vector2.zero;

        playerInput.actions["Look"].performed += ctx => lookInput = ctx.ReadValue<Vector2>();
        playerInput.actions["Look"].canceled += ctx => lookInput = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        //-60:50 
        transform.Translate(new Vector3(moveInput.x, 0, moveInput.y) * Time.deltaTime * moveSpeed);

        pitch += -lookInput.y * Time.deltaTime * lookSpeed;
        pitch = Mathf.Clamp(pitch, pitchRange.x, pitchRange.y);

        cam.transform.localRotation = Quaternion.Euler(new Vector3(pitch, 0, 0));
        transform.rotation *= Quaternion.Euler(new Vector3(0, lookInput.x, 0) * Time.deltaTime * lookSpeed);
    }
}
