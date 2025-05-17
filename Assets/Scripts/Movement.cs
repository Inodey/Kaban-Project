using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private Camera goCamera;

    private CharacterController controller;
    private Animations animationsScript;

    public bool isGrounded = false;

    public float verticalSpeed = 0f,
                        mouseX = 0f,
                        mouseY = 0f,
                        currentAngleX = 0f;

    public const float gravityScale = 9.8f,
                        speedScale = 5f,
                        jumpForce = 8f,
                        turnSpeed = 90f;

    private void MoveCharacter()
    {
    isGrounded = controller.isGrounded;
    // Get input for horizontal and vertical movement
    Vector3 velocity = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
    if(Input.GetKey(KeyCode.W))
    {
        animationsScript.WalkAnimation(true);
    }
    else
    {
        animationsScript.WalkAnimation(false);
    }

    // Transform the input direction to world space and apply speed
    velocity = transform.TransformDirection(velocity) * speedScale;

    // Check if the character is grounded
    if (controller.isGrounded)
    {
        // Reset vertical speed when grounded
        verticalSpeed = 0f;

        // Check if the jump button is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Apply jump force
            verticalSpeed = jumpForce;
            animationsScript.JumpAnimation();
        }
    }

    // Apply gravity
    verticalSpeed -= gravityScale * Time.deltaTime;

    // Set the vertical component of the velocity
    velocity.y = verticalSpeed;

    // Move the character using the character controller
    controller.Move(velocity * Time.deltaTime);
}

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animationsScript = GameObject.Find("AnimationLibrary_Unity_Standard").GetComponent<Animations>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveCharacter();
        ReloadGun();
        Interact();
        Aim();
    }

    private void ReloadGun()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            animationsScript.ReloadAnimation();
        }
    }

    private void Interact()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            animationsScript.InteractAnimation();
        }
    }

    private void Grabbing()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            animationsScript.GrabbingAnimation();
        }
    }

    private void Aim()
    {
        if(Input.GetMouseButton(1))
        {
            animationsScript.AimAnimation(true);
            if(Input.GetMouseButtonDown(0))
            {
                animationsScript.ShootAnimation();
            }
        }
        else
        {
            animationsScript.AimAnimation(false);
        }
    }
}