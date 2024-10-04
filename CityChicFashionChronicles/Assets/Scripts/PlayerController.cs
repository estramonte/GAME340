using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Animator animator;

    public float moveSpeed = 5.0f;
    public float rotationSpeed = 180.0f;
    public float gravity = -9.81f;

    private Vector3 moveDirection = Vector3.zero;
    private float verticalVelocity = 0f;    // For gravity

    void Start()
    {
        // Get the CharacterController and Animator components
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        // Null check to make sure CharacterController is assigned
        if (controller == null)
        {
            Debug.LogError("No CharacterController component found!");
        }

        if (animator == null)
        {
            Debug.LogError("No Animator component found!");
        }
    }

    void Update()
    {
        // Make sure CharacterController is available before continuing
        if (controller != null && animator != null)
        {
            // W/S for forward/backward, A/D for rotation
            float move = Input.GetAxis("Vertical");      // Forward/backward
            float rotate = Input.GetAxis("Horizontal");  // Left/right rotation

            // Apply gravity
            if (controller.isGrounded)
            {
                verticalVelocity = 0f;  // Reset gravity when grounded
            }
            else
            {
                verticalVelocity += gravity * Time.deltaTime;  // Apply gravity over time
            }

            // Move forward/backward based on W/S input
            moveDirection = -transform.right * move * moveSpeed;

            // Rotate character based on A/D input
            transform.Rotate(0, rotate * rotationSpeed * Time.deltaTime, 0);

            // Apply gravity to the movement
            moveDirection.y = verticalVelocity;

            // Move the character using the CharacterController
            controller.Move(moveDirection * Time.deltaTime);

            // Handle animations based on movement
            if (move != 0 || rotate != 0)
            {
                animator.SetInteger("legs", 1);  // Walking animation
            }
            else
            {
                animator.SetInteger("legs", 0);  // Idle animation
            }
        }
    }
}
