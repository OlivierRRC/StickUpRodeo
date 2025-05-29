using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OlivierPlayerMove : MonoBehaviour
{
    // Basic Variables to tweak in the editor
    public float moveSpeed = 50f;
    [Range(0, 1)] // Turns the below variable into a range slider in the editor
    public float groundDamping = 0.97f;
    public float lookSpeed = 50f;
    public float jumpForce = 50f;
    public Vector2 pitchRange = new Vector2(-60, 50);
    public float health = 100f;

    // Horse pictures for damage animations
    public Sprite[] honses;
    public Image honsesImage;

    // Inputs
    public PlayerInput playerInput;
    private Rigidbody rb;
    private Vector2 moveInput;
    
    // Camera Variables
    private Camera cam;
    private Vector2 lookInput;
    private float pitch; // x-axis camera rotation
    private float yaw; // y-axis camera rotation

    // Bullet Objects
    public GameObject hitParticles;
    public GameObject bulletPrefab;

    // UI and Text
    public TMP_Text AmmoText;
    
    public TMP_Text healthtext;
    public Image healthBar;

    // Sound
    public AudioSource footstep;
    public AudioSource reload;
    public AudioSource voice;
    public AudioSource SFX;
    public AudioSource theHorsesMouth;

    // Misc
    private int ammo = 8;
    private bool reloading;
    private LayerMask mask;
    Animator anim;
    public Animator sceneTransition;
    public Transform shotPoint;

    void Start()
    {
        // Set objects that need to be set in code
        anim = GetComponentInChildren<Animator>();
        cam = Camera.main;
        rb = GetComponent<Rigidbody>();

        // Lock Cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Player Inputs
        playerInput.actions["Move"].performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        playerInput.actions["Move"].canceled += ctx => moveInput = Vector2.zero;

        playerInput.actions["Look"].performed += ctx => lookInput = ctx.ReadValue<Vector2>();
        playerInput.actions["Look"].canceled += ctx => lookInput = Vector2.zero;

            // Let go of the jump button to leave it early
        playerInput.actions["Jump"].started += ctx => Jump();
        playerInput.actions["Jump"].canceled += ctx => CancelJump();

        playerInput.actions["Attack"].started += ctx => Shoot();

        playerInput.actions["Reload"].started += ctx => StartCoroutine(Reload());

        playerInput.actions["Escape"].started += ctx => SceneManager.LoadScene(0);

    }

    void Update()
    {
        // Set x and y axis camera rotation seperately.
        yaw += lookInput.x * lookSpeed;
        pitch += -lookInput.y * lookSpeed;
        pitch = Mathf.Clamp(pitch, pitchRange.x, pitchRange.y);

        // Rotate the player object on the y-axis
        transform.rotation = Quaternion.Euler(new Vector3(0, yaw, 0));
        // Rotate the camera object (child of the player) on the x-axis
        cam.transform.localRotation = Quaternion.Euler(new Vector3(pitch, 0, 0));
    }

    private bool isGrounded()
    {
        // Use a raycast to check if the player is on the ground
        RaycastHit hit;
        Physics.Raycast(transform.position + Vector3.up, Vector3.down, out hit, 1.1f);
        return hit.collider != null;
    }

    private void FixedUpdate()
    {
        // Apply forward and backward inputs on the player's forward(z) axis
        rb.linearVelocity += transform.forward * moveInput.y * moveSpeed;
        // Apply sideways inputs on the player's right(x) axis
        rb.linearVelocity += transform.right * moveInput.x * moveSpeed;
        // Apply ground damping to both axes
        rb.linearVelocity = new Vector3(rb.linearVelocity.x * groundDamping, rb.linearVelocity.y, rb.linearVelocity.z* groundDamping);

        if (rb.linearVelocity.magnitude >= 1)
        {
            // Play footstep SFX if the player is on the ground and the SFX aren't already playing
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
        // Check if the player is on the ground. If so, reduce the player's mass and apply an upward impulse force to jump.
        if (isGrounded())
        {
            rb.mass = 1;
            rb.AddRelativeForce(new Vector3(0, 1, 0) * jumpForce, ForceMode.Impulse);
        }
    }

    private void CancelJump()
    {
        // If player isn't on the ground when the jump is cancelled, cancel upward velocity and increase mass to make the fall quicker
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
        // Update healthbar and vet bills according to damage taken
        healthBar.fillAmount = health / 100;
        health -= damage;
        healthtext.text = "Vet Bill: $" + (100 - health) * 367;
        // Flash a sad horse image
        honsesImage.sprite = honses[Random.Range(0, honses.Length)];
        honsesImage.GetComponent<Animator>().SetTrigger("Hurt");
        // Use the Voice Line Manager script to play a little hit grunt sound effect from a list in the editor.
        GetComponentInChildren<VoiceLineManager>().playDamageVoiceLine();
        // If the Player dies, run the game over coroutine.
        if (health <= 0) { StartCoroutine(TransitionToLose()); }
    }

    IEnumerator TransitionToLose()
    {
        // FADE TO BLACK AND GO TO LOSE SCREEN
        sceneTransition.SetTrigger("SceneExit");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Lose");
    }


    public void Shoot()
    {
        // check to see if the player can't shoot before shooting.
        if ( reloading ) { return; }
        if ( ammo <= 0 )
        {
            StartCoroutine(Reload());
            return;
        }

        // Start animations, sound FX and detract ammo count
        anim.SetTrigger("Shoot");
        theHorsesMouth.pitch = 1 + (Random.Range(-0.4f, 0.4f));
        theHorsesMouth.Play();
        ammo--;
        AmmoText.text = ammo + "/8";

        // Fire a raycast down the hit reticle
        RaycastHit hit;
        Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, Mathf.Infinity);
        // If it hits something...
        if(hit.collider != null)
        {
            // Make a benadryl bullet and fire it from the horse's mouth to the place where the raycast hit.
            GameObject bp = Instantiate(bulletPrefab, shotPoint.position, Quaternion.identity);
            bp.GetComponent<FakeProjectile>().endPosition = hit.point;
            Instantiate(hitParticles, hit.point, Quaternion.identity);

            // If the bullet hits a target's body apply damage
            if (hit.collider.GetComponent<EnemyBase>())
            {
                hit.collider.GetComponent<EnemyBase>().TakeDamage(10);
            }
            // If it hits a head apply 2.5x damage, and play a ding SFX
            else if (hit.collider.GetComponentInParent<EnemyBase>())
            {
                hit.collider.GetComponentInParent<EnemyBase>().TakeDamage(25);

                SFX.pitch = 1 + (Random.Range(-0.2f, 0.2f));
                SFX.Play();
            }
            // If it hits a gore pile, run Rain's fun gore code (just bounces it into the air and if hit 3 times it explodes into fireworks)
            else if (hit.collider.GetComponent<Gore>())
            {
                hit.collider.GetComponent<Gore>().BoomBoom();
            }
        }
    }

    public IEnumerator Reload()
    {
        // If the player is already reloading, stop the coroutine here
        if (reloading)
        {
            yield break;
        }

        // Play the reload animation and shredder sound effect and set the reloading boolean to true.
        anim.SetTrigger("Reload");
        reload.pitch = 1 + (Random.Range(-0.2f, 0.2f));
        reload.Play();
        reloading = true;

        // A simple particle system that plays as the carrot enters the player's mouth.
        yield return new WaitForSeconds(1);
        GetComponentInChildren<ParticleSystem>().Play();

        // Stop the reload process from finishing until the animations are finished.
        yield return new WaitForSeconds(1.5f);
        // Play a reload voice line in the voice manager script.
        GetComponentInChildren<VoiceLineManager>().playReloadVoiceLine();
        // Update ammo count and stop reloading.
        ammo = 8;
        AmmoText.text = ammo + "/8";
        reloading = false;
        
    }

}
