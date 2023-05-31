using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlaceableObjectsManager : MonoBehaviour
{
    [SerializeField] PlaceableObjectsContainer placeableObjectsContainer;
    [SerializeField] Tilemap targetTilemap;

    private void Start()
    {
        if (GameManager.instance != null)
            GameManager.instance.GetComponent<PlaceableObjectsReferenceManager>().placeableObjectsManager = this;
        VizualizeMap();
    }

    private void OnDestroy()
    {
        for (int i = 0; i < placeableObjectsContainer.placeableObjects.Count; i++)
        {
            if (placeableObjectsContainer.placeableObjects[i].targetObject == null)
                continue;

            IPersistent persistent = placeableObjectsContainer.placeableObjects[i].targetObject.GetComponent<IPersistent>();
            if (persistent != null)
            {
                string jsonString = persistent.Read();
                placeableObjectsContainer.placeableObjects[i].objectState = jsonString;
            }

            placeableObjectsContainer.placeableObjects[i].targetObject = null;
        }
    }

    private void VizualizeMap()
    {
        for (int i = 0; i < placeableObjectsContainer.placeableObjects.Count; i++)
        {
            Vizualizeitem(placeableObjectsContainer.placeableObjects[i]);
        }
    }

    internal void PickUp(Vector3Int gridPosition)
    {
        PlaceableObject placedObject = placeableObjectsContainer.Get(gridPosition);

        if (placedObject == null)
            return;

        ItemSpawManager.instance.SpawnItem(targetTilemap.CellToWorld(gridPosition), placedObject.placedItem, 1);

        Destroy(placedObject.targetObject.gameObject);

        placeableObjectsContainer.Remove(placedObject);
    }

    private void Vizualizeitem(PlaceableObject placeableObject)
    {
        GameObject go = Instantiate(placeableObject.placedItem.itemPrefab);
        go.transform.parent = transform;


        go.transform.position = (targetTilemap.CellToWorld(placeableObject.positionOnGrid) + targetTilemap.cellSize / 2)
            - Vector3.forward * 0.1f;

        placeableObject.targetObject = go.transform;

        IPersistent persistent = go.GetComponent<IPersistent>();
        if (persistent != null)
        {
            persistent.Load(placeableObject.objectState);
        }
    }

    public bool Check(Vector3Int position)
    {
        return placeableObjectsContainer.Get(position) != null;
    }

    public void Place(Item item, Vector3Int positionOnGrid)
    {
        if (Check(positionOnGrid))
            return;

        PlaceableObject placeableObject = new PlaceableObject(item, positionOnGrid);
        Vizualizeitem(placeableObject);
        placeableObjectsContainer.placeableObjects.Add(placeableObject);
    }
}
