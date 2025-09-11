using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeTester : MonoBehaviour
{
    public GameObject shakePrefab;
    public Vector2 spawnRate = new Vector2(1f, 3f); // min, max seconds

    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            // Wait a random time between min and max
            float waitTime = Random.Range(spawnRate.x, spawnRate.y);
            yield return new WaitForSeconds(waitTime);

            // Spawn the prefab at this object's position/rotation
            Instantiate(shakePrefab, transform.position, transform.rotation);
        }
    }
}
