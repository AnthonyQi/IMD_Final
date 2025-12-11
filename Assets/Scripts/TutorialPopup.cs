using UnityEngine;

public class TutorialPopup : MonoBehaviour
{
    public GameObject tutorialCanvas;

    public void Show()
    {
        tutorialCanvas.SetActive(true);
    }

    public void Hide()
    {
        tutorialCanvas.SetActive(false);
    }
}
