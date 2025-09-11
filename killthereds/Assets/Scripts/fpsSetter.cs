using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fpsSetter : MonoBehaviour
{
    public int fps;

    void Start()
    {
        Application.targetFrameRate = fps;
    }
}
