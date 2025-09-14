using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleRotate : MonoBehaviour
{

    public bool x;
    public bool y;
    public bool z;


    public float speed = 30f;


    public bool noise;
    public float noiseIntensity = 15f;
    public float noiseSpeed = 1f;

    void Update()
    {
        Vector3 rotation = Vector3.zero;

  
        if (x) rotation.x += speed * Time.deltaTime;
        if (y) rotation.y += speed * Time.deltaTime;
        if (z) rotation.z += speed * Time.deltaTime;


        if (noise)
        {
            float t = Time.time * noiseSpeed;
            if (x) rotation.x += Mathf.PerlinNoise(t, 0f) * noiseIntensity - (noiseIntensity * 0.5f);
            if (y) rotation.y += Mathf.PerlinNoise(0f, t) * noiseIntensity - (noiseIntensity * 0.5f);
            if (z) rotation.z += Mathf.PerlinNoise(t, t) * noiseIntensity - (noiseIntensity * 0.5f);
        }


        transform.Rotate(rotation, Space.Self);
    }
}
