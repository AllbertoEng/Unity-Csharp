using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawManager : MonoBehaviour
{
    public static ItemSpawManager instance;

    private void Awake()
    {
        instance = this;
    }

    [SerializeField] GameObject pickUpItemPrefab;

    public void SpawnItem(Vector3 position, Item item, int count)
    {
        GameObject o = Instantiate(pickUpItemPrefab, position, Quaternion.identity);
        o.GetComponent<PickUpItem>().Set(item, count);
    }
}
