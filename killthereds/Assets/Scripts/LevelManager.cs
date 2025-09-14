using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("Level Settings")]
    public Level startingLevel;  

    private Level currentLevel;

    void Start()
    {
        SpawnLevel();
    }

    public void SpawnLevel()
    {
        currentLevel = Instantiate(startingLevel, Vector3.zero, Quaternion.identity, transform);
        currentLevel.transform.SetParent(transform, false);

        if (currentLevel.skybox != null)
        {
            RenderSettings.skybox = currentLevel.skybox;
            DynamicGI.UpdateEnvironment();
        }

        RenderSettings.fog = currentLevel.Fog;
        if (currentLevel.Fog)
        {
            RenderSettings.fogColor = currentLevel.fogColor;
            RenderSettings.fogDensity = currentLevel.FogDensity;

           
            if (currentLevel.useExponential)
            {
                RenderSettings.fogMode = FogMode.Exponential;
            }
            else if (currentLevel.useExponentialSquared)
            {
                RenderSettings.fogMode = FogMode.ExponentialSquared;
            }
            else
            {
                RenderSettings.fogMode = FogMode.Linear;
                RenderSettings.fogStartDistance = currentLevel.fogStartDistance;
                RenderSettings.fogEndDistance = currentLevel.fogEndDistance;
            }
        }
    }
}
