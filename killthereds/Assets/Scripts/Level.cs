using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [Header("Visual Settings")]
    public Material skybox;
    public string Name;
    public Texture2D cover;

    [Header("Fog Settings")]
    public bool Fog = false;
    public Color fogColor = Color.gray;
    [Range(0f, 1f)] public float FogDensity = 0.01f;

    [Header("Fog Mode (choose one)")]
    public bool useLinear = true;
    public bool useExponential = false;
    public bool useExponentialSquared = false;

    [Header("Linear Fog Settings")]
    public float fogStartDistance = 10f;
    public float fogEndDistance = 200f;
}
