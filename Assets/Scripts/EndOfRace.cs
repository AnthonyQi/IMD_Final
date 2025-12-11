using TMPro;
using UnityEngine;

public class EndOfRace : MonoBehaviour
{
    public Camera MainCamera;
    public Camera endCamera;
    private Animator anim;

    public TextMeshProUGUI timerBoard;
    public TextMeshProUGUI timer;
    public float setTimer = 1;

    public void Start()
    {
        anim = GetComponent<Animator>();
        anim.enabled = true;
        MainCamera.enabled = true;
        endCamera.enabled = false;
        timerBoard.enabled = false;
        timer.enabled = false;
    }

    private void switchCams()
    {
        MainCamera.enabled = false;
        endCamera.enabled = true;
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("end"))
        {
            switchCams();
            Display();
            GetComponent<playerMovement>().StopMovement();
            anim.enabled = false;
        }
    }

    public void Update()
    {
        setTimer += Time.deltaTime;
    }

    public void Display()
    {
        timer.text = setTimer.ToString("F2");
        timerBoard.enabled = true;
        timer.enabled = true;
    }

}
