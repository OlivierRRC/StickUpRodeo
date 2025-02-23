using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OlivierPlayerMove : MonoBehaviour
{

    public float moveSpeed = 50f;
    [Range(0, 1)]
    public float groundDamping = 0.97f;
    public float lookSpeed = 50f;
    public float jumpForce = 50f;
    public Vector2 pitchRange = new Vector2(-60, 50);
    public float health = 100f;

    public Sprite[] honses;
    public Image honsesImage;

    public PlayerInput playerInput;
    private Camera cam;
    private Rigidbody rb;

    private Vector2 moveInput;
    private Vector2 lookInput;
    private float pitch;
    private float yaw;
    private LayerMask mask;

    public GameObject hitParticles;
    public GameObject bulletPrefab;

    public TMP_Text AmmoText;
    private int ammo = 8;
    private bool reloading;
    public Image healthBar;

    //Sound
    public AudioSource footstep;
    public AudioSource reload;

    Animator anim;

    public Transform shotPoint;


    void Start()
    {
        anim = GetComponentInChildren<Animator>();
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

        playerInput.actions["Attack"].started += ctx => Shoot();

        playerInput.actions["Reload"].started += ctx => StartCoroutine(Reload());

        playerInput.actions["Escape"].started += ctx => SceneManager.LoadScene(0);

    }

    // Update is called once per frame
    void Update()
    {
        pitch += -lookInput.y * lookSpeed;
        pitch = Mathf.Clamp(pitch, pitchRange.x, pitchRange.y);

        yaw += lookInput.x * lookSpeed;

        cam.transform.localRotation = Quaternion.Euler(new Vector3(pitch, 0, 0));
        transform.rotation = Quaternion.Euler(new Vector3(0, yaw, 0));

        //Jake's Footstep Code
        if (moveInput != Vector2.zero) 
        {

        }
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

        if (rb.linearVelocity.magnitude >= 1)
        {
            if (footstep.isPlaying)
            {
                return;
            }
            if (!isGrounded())
            {
                return;
            }
            footstep.pitch = 1 + (Random.Range(-0.2f, 0.2f));
            footstep.Play();
        }
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


    public void TakeDamage(float damage)
    {
        healthBar.fillAmount = health / 100;
        honsesImage.sprite = honses[Random.Range(0, honses.Length)];
        honsesImage.GetComponent<Animator>().SetTrigger("Hurt");
        health -= damage;
        if (health <= 0)
        {
            SceneManager.LoadScene("Lose");
        }
    }

    public void Shoot()
    {
        if(reloading)
        {
            return;
        }

        if (ammo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        anim.SetTrigger("Shoot");
        ammo--;
        AmmoText.text = ammo + "/8";

        RaycastHit hit;
        Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, Mathf.Infinity);

        if(hit.collider != null)
        {
            
            GameObject bp = Instantiate(bulletPrefab, shotPoint.position, Quaternion.identity);
            bp.GetComponent<FakeProjectile>().endPosition = hit.point;
            Instantiate(hitParticles, hit.point, Quaternion.identity);

            if (hit.collider.GetComponent<EnemyBase>())
            {
                hit.collider.GetComponent<EnemyBase>().TakeDamage(10);
            }
            else if (hit.collider.GetComponent<Gore>())
            {
                hit.collider.GetComponent<Gore>().BoomBoom();
            }
        }
    }

    public IEnumerator Reload()
    {
        anim.SetTrigger("Reload");
        if (reloading)
        {
            yield break;
        }
        //trigger animation here
        reload.pitch = 1 + (Random.Range(-0.2f, 0.2f));
        reload.Play();
        reloading = true;
        yield return new WaitForSeconds(1);
        ammo = 8;
        AmmoText.text = ammo + "/8";
        reloading = false;
        GetComponent<VoiceLineManager>().playReloadVoiceLine();
    }

}
