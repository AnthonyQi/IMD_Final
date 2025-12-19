using UnityEngine;
using TMPro;

[RequireComponent(typeof(CharacterController))]
public class LongJump : MonoBehaviour
{
    public float maxSpeed = 15f;
    public float speedPerPress = .5f;
    public float deceleration = 4f;
    public float jumpHeight = 2f;
    public float gravity = -9.81f;

    public Animator animator;
    
    public TextMeshProUGUI messageText; // 
    public TextMeshProUGUI attemptsText; // shows remaining attempts
    public TextMeshProUGUI distanceText; // shows jump distance
    public TextMeshProUGUI inputPromptText; //q and e 

    public Transform faultLineTransform; 

    public int maxAttempts = 3;
    
    public float distanceScale = 1.25f; //converts distance 

    private CharacterController controller;
    private float currentSpeed = 0f;
    private Vector3 velocity;
    private bool isJumping = false;
    private bool isFaulted = false;
    private bool jumpCompleted = false;
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    
    // Jump tracking variables
    private int attemptsRemaining;
    private Vector3 takeoffPosition;
    private bool hasRecordedTakeoff = false;
    private float currentJumpDistance = 0f;
    private float bestJumpDistance = 0f;

    private bool expectingQ = true; 

    void Start()
    {
        controller = GetComponent<CharacterController>();

        
            initialPosition = transform.position;
            initialRotation = transform.rotation;
        

        attemptsRemaining = maxAttempts;
        UpdateAttemptsDisplay();
        UpdateInputPrompt();

        if (messageText != null)
        {
            messageText.gameObject.SetActive(false);
        }
        
        if (distanceText != null)
        {
            distanceText.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (jumpCompleted && attemptsRemaining > 0)
            {
                ResetJump();
            }
            else if (attemptsRemaining <= 0)
            {
                RestartGame();
            }
            return;
        }

        if (!isFaulted && !jumpCompleted)
        {
            HandleInput();
            ApplyGravity();
            MoveCharacter();
            UpdateAnimator();
            TrackJump();
        }
    }

    void HandleInput()
{
    // Check for alternating Q and E key presses
    if (!isJumping)
    {
        if (expectingQ && Input.GetKeyDown(KeyCode.Q))
        {
            currentSpeed += speedPerPress;
            currentSpeed = Mathf.Clamp(currentSpeed, 0, maxSpeed);
            expectingQ = false; 
            UpdateInputPrompt();
        }
        else if (!expectingQ && Input.GetKeyDown(KeyCode.E))
        {
            currentSpeed += speedPerPress;
            currentSpeed = Mathf.Clamp(currentSpeed, 0, maxSpeed);
            expectingQ = true; 
            UpdateInputPrompt();
        }
        else if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E))
        {
            currentSpeed -= speedPerPress * 1.5f; 
            if (currentSpeed < 0) currentSpeed = 0;
            Debug.Log("Wrong key! You lost speed!");
        }
    }

    if (Input.GetKeyDown(KeyCode.Space) && controller.isGrounded && currentSpeed > 0 && !isJumping)
    {
        animator.SetBool("isJump", true);

        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        isJumping = true;
        animator.Update(0f);
        takeoffPosition = transform.position;
        hasRecordedTakeoff = true;
        
        if (inputPromptText != null)
        {
            inputPromptText.gameObject.SetActive(false);
        }
    }

    if (!Input.GetKey(KeyCode.Q) && !Input.GetKey(KeyCode.E) && !isJumping && currentSpeed > 0)
    {
        currentSpeed -= deceleration * Time.deltaTime;
        if (currentSpeed < 0) currentSpeed = 0;
    }
}
    void UpdateInputPrompt()
    {
        if (inputPromptText != null && !isJumping)
        {
            if (expectingQ)
            {
                inputPromptText.text = "Press Q";
                inputPromptText.color = Color.yellow;
            }
            else
            {
                inputPromptText.text = "Press E";
                inputPromptText.color = Color.cyan;
            }
            inputPromptText.gameObject.SetActive(true);
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
                if (hasRecordedTakeoff)
                {
                    CalculateJumpDistance();
                    hasRecordedTakeoff = false;
                    
                    if (!isFaulted)
                    {
                        CompleteJump();
                    }
                }
                
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

    void TrackJump()
    {
        if (isJumping && hasRecordedTakeoff && distanceText != null && faultLineTransform != null)
        {
            float currentDistance = CalculateDistanceFromFaultLine(transform.position);
            
            if (currentDistance > 0)
            {
                if (!distanceText.gameObject.activeSelf)
                {
                    distanceText.gameObject.SetActive(true);
                }
                
                float displayDistance = currentDistance * distanceScale;
                distanceText.text = $"Distance: {displayDistance:F2}ft";
            }
        }
    }

    float CalculateDistanceFromFaultLine(Vector3 position)
    {
        

        Vector3 faultLinePos = new Vector3(faultLineTransform.position.x, 0, faultLineTransform.position.z);
        Vector3 checkPos = new Vector3(position.x, 0, position.z);
        
        float distance = Vector3.Distance(faultLinePos, checkPos);
        
        Vector3 directionToPos = (checkPos - faultLinePos).normalized;
        Vector3 forwardDirection = transform.forward;
        
        if (Vector3.Dot(directionToPos, forwardDirection) > 0)
        {
            return distance;
        }
        else
        {
            return 0f;
        }
    }

    void CalculateJumpDistance()
    {
        
        Vector3 landingPosition = transform.position;
        currentJumpDistance = CalculateDistanceFromFaultLine(landingPosition);

        if (currentJumpDistance > bestJumpDistance)
        {
            bestJumpDistance = currentJumpDistance;
        }

        float displayCurrent = currentJumpDistance * distanceScale;
        float displayBest = bestJumpDistance * distanceScale;

        // Debug.Log($"Jump Distance: {displayCurrent:F2}ft | Best: {displayBest:F2}ft");
        
        if (distanceText != null)
        {
            distanceText.text = $"Jump: {displayCurrent:F2}ft | Best: {displayBest:F2}ft";
            distanceText.gameObject.SetActive(true);
        }
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.CompareTag("FaultLine") && !isFaulted)
        {
            // Debug.Log("FAULT! Hit the FaultLine!");
            TriggerFault();
        }
    }

    void TriggerFault()
    {
        isFaulted = true;
        jumpCompleted = true;
        currentSpeed = 0f;
        velocity.y = -2f;
        isJumping = false;
        animator.SetBool("isJump", false);
        
        currentJumpDistance = 0f;
        
        attemptsRemaining--;
        UpdateAttemptsDisplay();
        
        ShowFaultMessage();
        
        if (inputPromptText != null)
        {
            inputPromptText.gameObject.SetActive(false);
        }
    }

    void CompleteJump()
    {
        jumpCompleted = true;
        attemptsRemaining--;
        UpdateAttemptsDisplay();
        ShowSuccessMessage();
        
        if (inputPromptText != null)
        {
            inputPromptText.gameObject.SetActive(false);
        }
    }

    void ShowFaultMessage()
    {
        float displayBest = bestJumpDistance * distanceScale;
        
        if (messageText != null)
        {
            if (attemptsRemaining > 0)
            {
                messageText.text = $"FAULT! Jumped on or before the white line.\nDistance: 0.00ft (Fault) | Best: {displayBest:F2}ft\nAttempts remaining: {attemptsRemaining}\nPress R for next attempt";
            }
            else
            {
                messageText.text = $"FAULT! No attempts remaining.\nFinal jump: 0.00ft (Fault)\nBest jump: {displayBest:F2}ft\nPress R to restart";
            }
            messageText.gameObject.SetActive(true);
        }
    }

    void ShowSuccessMessage()
    {
        float displayCurrent = currentJumpDistance * distanceScale;
        float displayBest = bestJumpDistance * distanceScale;
        
        if (messageText != null)
        {
            if (attemptsRemaining > 0)
            {
                messageText.text = $"\nAttempts remaining: {attemptsRemaining}\nPress R for next attempt";
            }
            else
            {
                messageText.text = $"Final Jump Complete!\nDistance: {displayCurrent:F2}ft\nBest: {displayBest:F2}ft\nPress R to restart";
            }
            messageText.gameObject.SetActive(true);
        }
    }

    void HideFaultMessage()
    {
        if (messageText != null)
        {
            messageText.gameObject.SetActive(false);
        }
    }

    void UpdateAttemptsDisplay()
    {
        if (attemptsText != null)
        {
            attemptsText.text = $"Attempts: {attemptsRemaining}/{maxAttempts}";
        }
    }

    void ResetJump()
    {
        if (attemptsRemaining <= 0)
        {
            // Debug.Log("No attempts remaining! Restarting game...");
            RestartGame();
            return;
        }

        controller.enabled = false;
        
        transform.position = initialPosition;
        transform.rotation = initialRotation;
        
        controller.enabled = true;
        
        currentSpeed = 0f;
        velocity = Vector3.zero;
        isJumping = false;
        isFaulted = false;
        jumpCompleted = false;
        hasRecordedTakeoff = false;
        expectingQ = true; 
        
        animator.SetBool("isJump", false);
        animator.SetFloat("Speed", 0f);
        
        HideFaultMessage();
        UpdateInputPrompt(); 
        
        // Debug.Log($"Ready for attempt {maxAttempts - attemptsRemaining + 1}");
    }

    public void RestartGame()
    {
        attemptsRemaining = maxAttempts;
        bestJumpDistance = 0f;
        currentJumpDistance = 0f;
        jumpCompleted = false;
        isFaulted = false;
        expectingQ = true; 
        
        UpdateAttemptsDisplay();
        
        controller.enabled = false;
        transform.position = initialPosition;
        transform.rotation = initialRotation;
        controller.enabled = true;
        
        currentSpeed = 0f;
        velocity = Vector3.zero;
        isJumping = false;
        hasRecordedTakeoff = false;
        
        animator.SetBool("isJump", false);
        animator.SetFloat("Speed", 0f);
        
        HideFaultMessage();
        UpdateInputPrompt(); 
        
        if (distanceText != null)
        {
            distanceText.gameObject.SetActive(false);
        }
        
        // Debug.Log("Game restarted! Starting fresh with 3 attempts.");
    }
}
