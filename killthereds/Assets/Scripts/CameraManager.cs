using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    public CinemachineVirtualCamera cinemachineManager;
    public PlayerMovement playerMovement;
    public float slideCameraShakeMultiplier;

    private CinemachineBasicMultiChannelPerlin noise;
    private Coroutine shakeCoroutine;

    public bool lockCameraControls;

    // Store the base amplitude gain so we can restore it later


    void Start()
    {
        noise = cinemachineManager.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        
    }

    void Update()
    {
        if (noise == null || lockCameraControls) return;

        if (playerMovement.isSliding)
        {
            // Boost amplitude gain while sliding
            noise.m_AmplitudeGain = slideCameraShakeMultiplier;
        }
        else
        {
            // Reset back to normal when not sliding
            noise.m_AmplitudeGain = 0f;
        }
    }

    /// <summary>
    /// Call this from other scripts to shake the camera.
    /// </summary>
    /// <param name="amount">Amplitude gain (intensity of shake)</param>
    /// <param name="duration">Duration of the shake in seconds</param>
    /// <param name="rampSpeed">How quickly the shake ramps up and down</param>
    public void ShakeCamera(float amount, float duration, float rampSpeed)
    {
        if (noise == null) return;

        // Stop any current shake
        if (shakeCoroutine != null)
            StopCoroutine(shakeCoroutine);

        shakeCoroutine = StartCoroutine(DoShake(amount, duration, rampSpeed));
    }

    private IEnumerator DoShake(float amount, float duration, float rampSpeed)
    {
        float elapsed = 0f;

        // Ramp up
        while (elapsed < rampSpeed && elapsed < duration / 2f)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / rampSpeed;
            noise.m_AmplitudeGain = Mathf.Lerp(0f, amount, t);
            noise.m_FrequencyGain = Mathf.Lerp(1f, amount, t);
            yield return null;
        }

        // Maintain shake until duration minus ramp down
        float holdTime = duration - (rampSpeed * 2f);
        if (holdTime > 0f)
        {
            float timer = 0f;
            while (timer < holdTime)
            {
                timer += Time.deltaTime;
                noise.m_AmplitudeGain = amount;
                noise.m_FrequencyGain = amount;
                yield return null;
            }
        }

        // Ramp down
        elapsed = 0f;
        while (elapsed < rampSpeed)
        {
            elapsed += Time.deltaTime;
            float t = 1f - (elapsed / rampSpeed);
            noise.m_AmplitudeGain = Mathf.Lerp(0f, amount, t);
            noise.m_FrequencyGain = Mathf.Lerp(1f, amount, t);
            yield return null;
        }

        // Reset values
        
        noise.m_FrequencyGain = 1f;
        shakeCoroutine = null;
    }
}
