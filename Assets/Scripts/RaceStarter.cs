using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using Random = UnityEngine.Random;


public class RaceStarter : MonoBehaviour
{
    [Header("Audio")]
    public AudioClip gunFireClip;
    
    [Header("UI")]
    public Text countdownText; // For legacy Text
    // OR use: public TextMeshProUGUI countdownText; // For TextMeshPro
    
    [Header("Timing (in seconds)")]
    public float delayBeforeReady = 1f;
    public float delayBeforeSet = 1f;
    public float delayBeforeGo = 1f;
    public float textDisplayDuration = 0.5f;
    public float gunSoundDelay = 3.6f;
    
    [Header("References")]
    public RaceStopwatch stopwatch;
    
    private AudioSource audioSource;
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        
        // Hide text at start
        if (countdownText != null)
        {
            countdownText.text = "";
        }
        
        // Start countdown automatically
        StartCountdown();
    }
    
    public void StartCountdown()
    {
        StartCoroutine(CountdownSequence());
    }
    
    private IEnumerator CountdownSequence()
    {
        
        float rand = Random.Range(1f, 3f);
        yield return new WaitForSeconds(delayBeforeReady);
        // Show "READY"
        if (countdownText != null)
        {
            countdownText.text = "ON YOUR MARKS";
        }
        
        yield return new WaitForSeconds(delayBeforeSet);
        
        if (countdownText != null)
        {
            countdownText.text = "GET SET";
        }

        if (gunFireClip != null)
        {
            audioSource.PlayOneShot(gunFireClip);
        }

        yield return new WaitForSeconds(gunSoundDelay);
    
        if (stopwatch != null)
        {
            countdownText.text = "GO!";
            stopwatch.StartTimer();
        }
        
        // Hide text after a moment
        yield return new WaitForSeconds(textDisplayDuration);
        if (countdownText != null)
        {
            countdownText.text = "";
        }
    }
}