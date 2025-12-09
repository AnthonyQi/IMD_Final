using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class LongJump_StopOnLandWithFault_TMP : MonoBehaviour
{
    [Header("Movement Settings")]
    public float maxSpeed = 15f;
    public float speedPerPress = .5f;
    public float deceleration = 4f;
    public float jumpHeight = 2f;
    public float gravity = -9.81f;

    [Header("References")]
    public Animator animator;

    private CharacterController controller;
    private float currentSpeed = 0f;
    private Vector3 velocity;
    private bool isJumping = false;


    void Start()
    {
        controller = GetComponent<CharacterController>();

        
    }

    void Update()
    {
        HandleInput();
        ApplyGravity();
        MoveCharacter();
        UpdateAnimator();
    }

    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.W) && !isJumping)
        {
            currentSpeed += speedPerPress;
            currentSpeed = Mathf.Clamp(currentSpeed, 0, maxSpeed);
        }

        if (Input.GetKeyDown(KeyCode.Space) && controller.isGrounded && currentSpeed > 0 && !isJumping)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            isJumping = true;
            animator.SetBool("isJump", true);
        }

        if (!Input.GetKey(KeyCode.W) && !isJumping && currentSpeed > 0)
        {
            currentSpeed -= deceleration * Time.deltaTime;
            if (currentSpeed < 0) currentSpeed = 0;
        }
    }

    void MoveCharacter()
    {
        Vector3 move = transform.forward * currentSpeed * Time.deltaTime;
        controller.Move(move + velocity * Time.deltaTime);
    }

    void ApplyGravity()
    {
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; 

            if (isJumping)
            {
                currentSpeed = 0f;

                isJumping = false;
                animator.SetBool("isJump", false);
            }
        }

        velocity.y += gravity * Time.deltaTime;
    }

    void UpdateAnimator()
    {
        animator.SetFloat("Speed", currentSpeed / maxSpeed);
    }




}
