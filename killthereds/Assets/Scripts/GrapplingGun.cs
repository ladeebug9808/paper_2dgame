using UnityEngine;

public class GrapplingGun : MonoBehaviour
{
    private LineRenderer lr;
    private Vector3 grapplePoint;
    public LayerMask whatIsGrappleable;
    public KeyCode keyCode;
    public Transform gunTip, camera, player;
    public float maxDistance = 100f;
    public float drawRopeSpeed;
    public CameraManager cameraManager;

    [Header("Pull Settings")]
    public float pullForce = 20f;
    public float minDistance = 2f;

    [Header("Rope Displacement Settings")]
    public float displaceAmount = 1f;
    public float sineSpeed = 5f;
    public float timeTillStraight = 1f;

    [SerializeField] private SpringJoint joint;
    private Vector3 currentGrapplePosition;
    private Rigidbody playerRb;
    private float ropeTimer = 0f;

    void Awake()
    {
        lr = GetComponent<LineRenderer>();
        playerRb = player.GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (cameraManager.lockCameraControls) return;
        if (Input.GetKeyDown(keyCode))
        {
            StartGrapple();
        }
        else if (Input.GetKeyUp(keyCode))
        {
            StopGrapple();
        }

        if (joint)
        {
            PullPlayer();
        }
    }

    void LateUpdate()
    {
        DrawRope();
    }

    void StartGrapple()
    {
        RaycastHit hit;
        if (Physics.Raycast(camera.position, camera.forward, out hit, maxDistance, whatIsGrappleable))
        {
            grapplePoint = hit.point;
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplePoint;

            float distanceFromPoint = Vector3.Distance(player.position, grapplePoint);
            joint.maxDistance = distanceFromPoint * 0.8f;
            joint.minDistance = distanceFromPoint * 0.25f;
            joint.spring = 4.5f;
            joint.damper = 7f;
            joint.massScale = 4.5f;

            lr.positionCount = 20; // make rope have more points for sine wave
            currentGrapplePosition = gunTip.position;
            ropeTimer = 0f; // reset timer for sine fade
        }
    }

    void StopGrapple()
    {
        lr.positionCount = 0;
        Destroy(joint);
    }

    void PullPlayer()
    {
        float distance = Vector3.Distance(player.position, grapplePoint);
        if (distance > minDistance)
        {
            Vector3 direction = (grapplePoint - player.position).normalized;
            playerRb.AddForce(direction * pullForce, ForceMode.Acceleration);
        }
    }

    void DrawRope()
    {
        if (!joint) return;

        currentGrapplePosition = Vector3.Lerp(currentGrapplePosition, grapplePoint, Time.deltaTime * drawRopeSpeed);

        Vector3 start = gunTip.position;
        Vector3 end = currentGrapplePosition;

        ropeTimer += Time.deltaTime;
        float t = Mathf.Clamp01(ropeTimer / timeTillStraight);

        for (int i = 0; i < lr.positionCount; i++)
        {
            float alpha = (float)i / (lr.positionCount - 1);
            Vector3 point = Vector3.Lerp(start, end, alpha);

            // Apply sine wave displacement that fades over time
            Vector3 offset = Vector3.up * Mathf.Sin(Time.time * sineSpeed + alpha * Mathf.PI * 2) * displaceAmount * (1f - t);
            lr.SetPosition(i, point + offset);
        }
    }

    public bool IsGrappling() => joint != null;

    public Vector3 GetGrapplePoint() => grapplePoint;
}
