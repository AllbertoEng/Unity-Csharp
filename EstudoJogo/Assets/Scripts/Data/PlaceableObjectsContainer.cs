using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlaceableObject
{
    public Item placedItem;
    public Transform targetObject;
    public Vector3Int positionOnGrid;

    public PlaceableObject(Item item, Vector3Int pos)
    {
        this.placedItem = item;
        this.positionOnGrid = pos;
    }

}


[CreateAssetMenu(menuName = "Data/Placeable Objects Container")]
public class PlaceableObjectsContainer : ScriptableObject
{
    public List<PlaceableObject> placeableObjects;

    internal PlaceableObject Get(Vector3Int position)
    {
        return placeableObjects.Find(x => x.positionOnGrid == position);
    }

    internal void Remove(PlaceableObject placedObject)
    {
        placeableObjects.Remove(placedObject);
    }
}

