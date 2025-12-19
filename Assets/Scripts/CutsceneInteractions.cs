using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneInteractions : MonoBehaviour
{
    public TextMeshProUGUI player;
    public TextMeshProUGUI player1;
    public TextMeshProUGUI coach;
    public TextMeshProUGUI coach1;
    public TextMeshProUGUI coach2;

    void Start()
    {
        player.enabled = false;
        coach.enabled = true;
        player1.enabled = false;
        coach1.enabled = false;
        coach2.enabled = false;
        StartCoroutine(CutsceneSequence());
    }

    private IEnumerator CutsceneSequence()
    {
        coach.enabled = true;
        player.enabled = false;
        player1.enabled = false;
        coach1.enabled = false;
        coach2.enabled = false;
        yield return new WaitForSeconds(4);

        coach.enabled = false;
        player.enabled = true;
        yield return new WaitForSeconds(4);

        player.enabled = false;
        coach1.enabled = true;
        yield return new WaitForSeconds(4);

        coach1.enabled = false;
        coach2.enabled = true;
        yield return new WaitForSeconds(4);

        coach2.enabled = false;
        player1.enabled = true;
        yield return new WaitForSeconds(4);

        SceneManager.LoadScene("track_fola");

    }
}