using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ShakeInstance : MonoBehaviour
{
    public float shakeAmt = 8f;

    public float shakeDuration = 0.7f;

    public float shakeRampUpSpeed = 3f;

    public CameraManager cameraManager;

    void Start()
    {
        if (cameraManager == null)
        {
            cameraManager = FindObjectOfType<CameraManager>();
        }
    }

    public void shk()
    {
        cameraManager.ShakeCamera(shakeAmt, shakeDuration, shakeRampUpSpeed);
    }

}
