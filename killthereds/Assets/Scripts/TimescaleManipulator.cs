using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.UIElements;

public class TimescaleManipulator : MonoBehaviour
{
    public float lerpSpeed;

    public float slowmoTimescale = 0.4f;

    public float defaultTimescale = 1f;

    public float slowmoDuration;

    public float slowmoCooldown;

    public KeyCode slowmoInput = KeyCode.R;

    public AudioSource slowmoSFX;


    
    [SerializeField] private bool isSlowmoActive;

    private bool isOnCooldown;

    private float targetTimescale;
    // Start is called before the first frame update
void Start()
    {
        targetTimescale = defaultTimescale;
        Time.timeScale = defaultTimescale;
    }

    void Update()
    {
        // Handle input
        if (Input.GetKeyDown(slowmoInput) && !isOnCooldown)
        {
            if (!isSlowmoActive)
            {
                Slowmo();
            }
        }

        // Smoothly lerp timescale
        Time.timeScale = Mathf.Lerp(Time.timeScale, targetTimescale, Time.unscaledDeltaTime * lerpSpeed);

        // Also keep physics consistent
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
    }

    public void Slowmo()
    {
         StartCoroutine(SlowmoRoutine());
    }

    private IEnumerator SlowmoRoutine()
    {
        isSlowmoActive = true;
        targetTimescale = slowmoTimescale;
        slowmoSFX.Play();

        yield return new WaitForSecondsRealtime(slowmoDuration);

        targetTimescale = defaultTimescale;
        isSlowmoActive = false;
        isOnCooldown = true;

        yield return new WaitForSecondsRealtime(slowmoCooldown);
        isOnCooldown = false;
    }
}
