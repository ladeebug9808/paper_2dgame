using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemActor_GrappleHook : MonoBehaviour
{
    [Header("Target parent object name inside PlayerRigidbody hierarchy")]
    public string parentName;

    [Header("Tags used for assignment (in prefab root)")]
    public string cameraManagerTag;
    public string cameraTag;

    [Header("Objects to reparent (auto-filled if left empty)")]
    public GameObject[] hooks;

    private GameObject desiredParent;
    private Transform playerRigidbody;
    private CameraManager camManager;
    private Transform cam;

    void Start()
    {
        playerRigidbody = transform.parent;
        if (playerRigidbody == null)
        {
            Debug.LogWarning($"[ItemActor_GrappleHook] {gameObject.name} has no PlayerRigidbody parent.");
            return;
        }

        // Step 1: Find the target parent inside PlayerRigidbody
        desiredParent = FindChildByName(playerRigidbody, parentName);
        if (desiredParent == null)
        {
            Debug.LogWarning($"[ItemActor_GrappleHook] Could not find '{parentName}' under {playerRigidbody.name}");
            return;
        }

        // Step 2: Find camera & camera manager once
        Transform prefabRoot = playerRigidbody.parent;
        if (prefabRoot != null)
        {
            GameObject camObj = GameObject.FindGameObjectWithTag(cameraTag);
            if (camObj != null) cam = camObj.transform;

            GameObject camManagerObj = GameObject.FindGameObjectWithTag(cameraManagerTag);
            if (camManagerObj != null) camManager = camManagerObj.GetComponent<CameraManager>();
        }

        // Step 3: Auto-fill hooks if empty
        if (hooks == null || hooks.Length == 0)
        {
            hooks = playerRigidbody.GetComponentsInChildren<GrapplingGun>(true)
                                   .Select(g => g.gameObject).ToArray();
        }

        // Step 4: Reparent all hooks and assign references
        foreach (GameObject hook in hooks)
        {
            if (hook == null) continue;

            // Fix: adopt parent’s local space for correct alignment
            hook.transform.SetParent(desiredParent.transform, false);

            GrapplingGun gun = hook.GetComponent<GrapplingGun>();
            if (gun != null)
            {
                gun.player = playerRigidbody;
                gun.camera = cam;
                gun.cameraManager = camManager;
            }
        }
    }

    /// <summary>
    /// Helper function to find a child GameObject by name in the hierarchy.
    /// </summary>
    private GameObject FindChildByName(Transform parent, string name)
    {
        foreach (Transform t in parent.GetComponentsInChildren<Transform>(true))
        {
            if (t.name == name) return t.gameObject;
        }
        return null;
    }
}
