using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public ItemActor[] items;
    private ItemActor spawnedItem;

    void Start()
    {
        int index = Random.Range(0, items.Length);
        spawnedItem = Instantiate(items[index], transform.position, transform.rotation, transform);
    }
}
