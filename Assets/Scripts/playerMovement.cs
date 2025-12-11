using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class playerMovement : MonoBehaviour
{
    public float baseSpeed = 5f;
    private float maxSpeedBoost = 1.5f;
    private float normalSpeedBoost = 0.75f;

    private float greenTimer = 0f;
    private float greenDuration = 3f;
    private float greatGreenStart = 0.8f;
    private float greatGreenEnd = 1.79f;
    private float goodGreenStart = 1.8f;
    private float goodGreenEnd = 2.55f;

    private bool isStopped;

    private Vector3 moveDirection;

    private CharacterController cc;
    private Vector3 startingPos = new Vector3(-18.48f, -0.017f, 53.09f);

    public void Start()
    {
        transform.position = startingPos;
        isStopped = false;
        cc = GetComponent<CharacterController>();
    }

    public void StopMovement()
    {
        isStopped = true;
        baseSpeed = 0f;
    }


    public void Update()
    {
        if (isStopped) return;
        {
            moveDirection = transform.forward * baseSpeed;
            cc.Move(moveDirection * Time.deltaTime);

            greenTimer += Time.deltaTime;
            if (greenTimer > greenDuration)
            {
                greenTimer = 0f;
            }
        }
    }

    public void Green(InputAction.CallbackContext context)
    {
        float greened = greenTimer;
        if (context.performed && baseSpeed == 8)
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
        StartCoroutine(TemporarySpeedBoost(maxSpeedBoost, 1.5f));
    }

    private void GoodGreen()
    {
        StartCoroutine(TemporarySpeedBoost(normalSpeedBoost, 0.85f));
    }

    private IEnumerator TemporarySpeedBoost(float boost, float duration)
    {
        baseSpeed += boost;
        yield return new WaitForSeconds(duration);
        baseSpeed -= boost;
    }
}
