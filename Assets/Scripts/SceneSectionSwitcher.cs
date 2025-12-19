using UnityEngine;

public class SceneSectionSwitcher : MonoBehaviour
{
    public GameObject mainMenuArea;
    public GameObject hundredMeterArea;
    public GameObject longJumpArea;

    public Camera menuCam;
    public Camera hundredCam;
    public Camera longJumpCam;

    public void GoToMainMenu()
    {
        Switch(mainMenuArea, menuCam);
    }

    public void GoToHundredMeter()
    {
        Switch(hundredMeterArea, hundredCam);
    }

    public void GoToLongJump()
    {
        Switch(longJumpArea, longJumpCam);
    }

    private void Switch(GameObject targetArea, Camera targetCam)
    {
        //Disable all areas
        mainMenuArea.SetActive(false);
        hundredMeterArea.SetActive(false);
        longJumpArea.SetActive(false);

        //Disable all cameras
        menuCam.gameObject.SetActive(false);
        hundredCam.gameObject.SetActive(false);
        longJumpCam.gameObject.SetActive(false);

        //Activate the chosen area and camera
        targetArea.SetActive(true);
        targetCam.gameObject.SetActive(true);
    }
}
