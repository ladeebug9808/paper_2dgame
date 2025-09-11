using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeOnAwake : MonoBehaviour
{
    public ShakeInstance shakeInstance;

    
  
    void Start()
    {
        shakeInstance.shk();
    }

   
}
