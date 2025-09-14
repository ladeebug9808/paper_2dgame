using UnityEngine;

public class ItemActor : MonoBehaviour
{
    [Header("Rotation Settings")]
    public bool rotateX;
    public bool rotateY;
    public bool rotateZ;
    public float rotationSpeed = 30f;
    public bool addNoise;
    public float noiseIntensity = 15f;
    public float noiseSpeed = 1f;

    [Space(10f)]
    [Header("Pickup Settings")]
    public PlayerMovement plr; // Keep reference to player
    public GameObject inventoryItemPrefab;

    void Start()
    {
        // Assign player if not already assigned
        if (plr == null)
            plr = FindObjectOfType<PlayerMovement>();
    }

    void Update()
    {
        RotateItem();
    }

    /// <summary>
    /// Handles item rotation with optional Perlin noise.
    /// </summary>
    private void RotateItem()
    {
        Vector3 rotation = Vector3.zero;

        if (rotateX) rotation.x += rotationSpeed * Time.deltaTime;
        if (rotateY) rotation.y += rotationSpeed * Time.deltaTime;
        if (rotateZ) rotation.z += rotationSpeed * Time.deltaTime;

        if (addNoise)
        {
            float t = Time.time * noiseSpeed;
            if (rotateX) rotation.x += Mathf.PerlinNoise(t, 0f) * noiseIntensity - (noiseIntensity * 0.5f);
            if (rotateY) rotation.y += Mathf.PerlinNoise(0f, t) * noiseIntensity - (noiseIntensity * 0.5f);
            if (rotateZ) rotation.z += Mathf.PerlinNoise(t, t) * noiseIntensity - (noiseIntensity * 0.5f);
        }

        transform.Rotate(rotation, Space.Self);
    }

    /// <summary>
    /// Handles item pickup when player collides with it.
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        PlayerMovement player = other.GetComponent<PlayerMovement>();
        if (player == null || player.isHoldingItem) return;

        // Use plr if assigned, otherwise fall back to collision
        PlayerMovement pickupPlayer = plr != null ? plr : player;

        pickupPlayer.isHoldingItem = true;

        if (inventoryItemPrefab != null)
            Instantiate(inventoryItemPrefab, pickupPlayer.transform.position, Quaternion.identity, pickupPlayer.transform);


        Destroy(gameObject);
    }
}
