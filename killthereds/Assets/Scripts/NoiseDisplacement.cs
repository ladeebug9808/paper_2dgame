using UnityEngine;

public class NoiseDisplacement : MonoBehaviour
{
    [Header("Noise Settings")]
    public float speed = 1f;           // How fast the rotation changes
    public float displaceAmount = 10f; // Maximum rotation offset in degrees

    private Vector3 originalRotation;

    private void Start()
    {
        // Store the original rotation
        originalRotation = transform.eulerAngles;
    }

    private void Update()
    {
        float time = Time.time * speed;

        // Calculate noise-based rotation offsets for each axis
        float xOffset = (Mathf.PerlinNoise(time, 0f) - 0.5f) * 2f * displaceAmount;
        float yOffset = (Mathf.PerlinNoise(0f, time) - 0.5f) * 2f * displaceAmount;
        float zOffset = (Mathf.PerlinNoise(time, time) - 0.5f) * 2f * displaceAmount;

        // Apply the rotation
        transform.localRotation = Quaternion.Euler(originalRotation + new Vector3(xOffset, yOffset, zOffset));
    }
}
