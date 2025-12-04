using UnityEngine;
using UnityEngine.UI;

public class RaceStopwatch : MonoBehaviour
{
    [Header("UI References")]
    public Text timerText;
    
    [Header("Settings")]
    public string timeFormat = "mm\\:ss\\:fff"; // mm:ss:milliseconds
    
    private float elapsedTime = 0f;
    private bool isRunning = false;
    
    void Update()
    {
        if (isRunning)
        {
            elapsedTime += Time.deltaTime;
            UpdateTimerDisplay();
        }
    }
    
    // Call this when the umpire says "go"
    public void StartTimer()
    {
        isRunning = true;
        elapsedTime = 0f;
    }
    
    // Call this when player crosses finish line
    public void StopTimer()
    {
        isRunning = false;
    }
    
    public void ResetTimer()
    {
        isRunning = false;
        elapsedTime = 0f;
        UpdateTimerDisplay();
    }
    
    private void UpdateTimerDisplay()
    {
        if (timerText != null)
        {
            System.TimeSpan timeSpan = System.TimeSpan.FromSeconds(elapsedTime);
            timerText.text = timeSpan.ToString(timeFormat);
        }
    }
    
    // Get the final time as a string
    public string GetFinalTime()
    {
        System.TimeSpan timeSpan = System.TimeSpan.FromSeconds(elapsedTime);
        return timeSpan.ToString(timeFormat);
    }
    
    // Get elapsed time as float (in seconds)
    public float GetElapsedTime()
    {
        return elapsedTime;
    }
}