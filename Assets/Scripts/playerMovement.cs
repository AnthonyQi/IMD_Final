using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerMovement : MonoBehaviour
{
    public float baseSpeed = 5f;
    private float maxSpeedBoost = 2.5f;
    private float normalSpeedBoost = 1f;

    private float greenTimer = 0f;
    private float greenDuration = 3f;
    private float greatGreenStart = 0.8f;
    private float greatGreenEnd = 1.79f;
    private float goodGreenStart = 1.8f;
    private float goodGreenEnd = 2.55f;

    private Vector3 moveDirection;

    private CharacterController cc;
    private Vector3 startingPos = new Vector3(-19.8f, 0.02f, 53.09f);

    public void Start()
    {
        transform.position = startingPos;
        cc = GetComponent<CharacterController>();
    }

    public void Update()
    {
        moveDirection = transform.forward * baseSpeed;
        cc.Move(moveDirection * Time.deltaTime);

        greenTimer += Time.deltaTime;
        if (greenTimer > greenDuration)
        {
            greenTimer = 0f;
        }

    }

    public void Green(InputAction.CallbackContext context)
    {
        float greened = greenTimer;
        if (context.performed)
        {
            if (greened >= greatGreenStart && greened <= greatGreenEnd)
            {
                GreatGreen();
            }
            else if (greened >= goodGreenStart && greened <= goodGreenEnd)
            {
                GoodGreen();
            }
        }
    }

    private void GreatGreen()
    {
        StartCoroutine(TemporarySpeedBoost(maxSpeedBoost, 2f));
    }

    private void GoodGreen()
    {
        StartCoroutine(TemporarySpeedBoost(normalSpeedBoost, 1.5f));
    }

    private IEnumerator TemporarySpeedBoost(float boost, float duration)
    {
        baseSpeed += boost;
        yield return new WaitForSeconds(duration);
        baseSpeed -= boost;
    }
}
