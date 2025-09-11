using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SFX : MonoBehaviour
{
    private AudioSource audioSource;

    [Header("Audio Settings")]
    public bool playOnAwake = false;

    [Header("Randomization")]
    public bool randomPitch = false;
    public bool randomVolume = false;
    public Vector2 pitchRange = new Vector2(0.8f, 1.2f);
    public Vector2 volumeRange = new Vector2(0.8f, 1.2f);

    [Header("Fading")]
    public bool useFade = false;
    public float fadeInDuration = 0.1f;
    public float fadeOutDuration = 0.1f;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        if (!audioSource.clip)
            Debug.LogWarning($"No AudioClip assigned to {gameObject.name}'s AudioSource.");

        if (playOnAwake)
        {
            PlaySFX(); // Call your custom PlaySFX method
        }
    }

    public void PlaySFX()
    {
        if (!audioSource || !audioSource.clip) return;

        float pitch = randomPitch ? Random.Range(pitchRange.x, pitchRange.y) : audioSource.pitch;
        float volume = randomVolume ? Random.Range(volumeRange.x, volumeRange.y) : audioSource.volume;

        audioSource.pitch = pitch;

        if (useFade)
        {
            StartCoroutine(FadePlay(volume));
        }
        else
        {
            audioSource.PlayOneShot(audioSource.clip, volume);
        }
    }

    private IEnumerator FadePlay(float targetVolume)
    {
        audioSource.volume = 0f;
        audioSource.PlayOneShot(audioSource.clip);
        float elapsed = 0f;

        // Fade in
        while (elapsed < fadeInDuration)
        {
            elapsed += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(0f, targetVolume, elapsed / fadeInDuration);
            yield return null;
        }

        audioSource.volume = targetVolume;

        // Wait for clip duration minus fadeOut
        yield return new WaitForSeconds(audioSource.clip.length - fadeOutDuration);

        // Fade out
        elapsed = 0f;
        while (elapsed < fadeOutDuration)
        {
            elapsed += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(targetVolume, 0f, elapsed / fadeOutDuration);
            yield return null;
        }

        audioSource.volume = 0f;
    }
}
