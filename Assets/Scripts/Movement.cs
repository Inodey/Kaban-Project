using UnityEngine;

public class Movement : MonoBehaviour
{
    private CharacterController controller;
    private Animations animationsScript;

    public LayerMask groundLayer;
    public Transform groundCheck;
    public float groundCheckRadius = 0.3f;

    public float verticalSpeed = 0f;
    public bool isGrounded = false;

    public const float gravityScale = 20f;
    public const float speedScale = 5f;
    public const float jumpForce = 8f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animationsScript = GameObject.Find("AnimationLibrary_Unity_Standard").GetComponent<Animations>();
    }

    void Update()
    {
        HandleMovement();
        ReloadGun();
        Interact();
        Aim();
    }

    void HandleMovement()
    {
        // Manual ground check
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);

        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

        if (input.magnitude > 0.1f)
            animationsScript.WalkAnimation(true);
        else
            animationsScript.WalkAnimation(false);

        Vector3 move = transform.TransformDirection(input) * speedScale;

        if (isGrounded)
        {
            if (verticalSpeed < 0f)
                verticalSpeed = -2f; // Keeps grounded better

            if (Input.GetKeyDown(KeyCode.Space))
            {
                verticalSpeed = jumpForce;
                animationsScript.JumpAnimation();
            }
        }

        // Apply gravity
        verticalSpeed -= gravityScale * Time.deltaTime;
        move.y = verticalSpeed;

        controller.Move(move * Time.deltaTime);
    }

    void ReloadGun()
    {
        if (Input.GetKeyDown(KeyCode.R))
            animationsScript.ReloadAnimation();
    }

    void Interact()
    {
        if (Input.GetKeyDown(KeyCode.E))
            animationsScript.InteractAnimation();
    }

    void Aim()
    {
        if (Input.GetMouseButton(1))
        {
            animationsScript.AimAnimation(true);
            if (Input.GetMouseButtonDown(0))
                animationsScript.ShootAnimation();
        }
        else
        {
            animationsScript.AimAnimation(false);
        }
    }
}