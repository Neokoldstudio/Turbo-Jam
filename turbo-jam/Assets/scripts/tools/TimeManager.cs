using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance { get; private set; }
    void Awake()
    {
        Instance = this;
    }

    public void SlowTimeSmooth(float durationFirstInterpolation, float durationWait, float durationLastInterpolation)
    {
        StartCoroutine(SlowTime(durationFirstInterpolation, durationWait, durationLastInterpolation));
    }

    private IEnumerator SlowTime(float durationFirstInterpolation, float durationWait, float durationLastInterpolation)
    {
        float targetTimeScale = 0.1f;
        float elapsed = 0f;
        float initialTimeScale = Time.timeScale;

        // Gradually decrease time scale
        while (Time.timeScale > targetTimeScale + 0.01f) // Add a small margin to prevent infinite loop
        {
            elapsed += Time.unscaledDeltaTime;
            Time.timeScale = Mathf.Lerp(initialTimeScale, targetTimeScale, elapsed / durationFirstInterpolation);
            yield return null;
        }
        Time.timeScale = targetTimeScale;

        // Wait for real-time duration
        yield return new WaitForSecondsRealtime(durationWait);

        // Reset variables for increasing time scale
        elapsed = 0f;
        initialTimeScale = Time.timeScale;

        // Gradually increase time scale
        while (Time.timeScale < 1f - 0.01f) // Add a small margin to prevent infinite loop
        {
            elapsed += Time.unscaledDeltaTime;
            Time.timeScale = Mathf.Lerp(initialTimeScale, 1f, elapsed / durationLastInterpolation);
            yield return null;
        }
        Time.timeScale = 1f;
        yield return null;
    }
}
