using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableObjectsReferenceManager : MonoBehaviour
{
    public PlaceableObjectsManager placeableObjectsManager;

    public void Place(Item item, Vector3Int positionOnGrid)
    {
        if (!VerifyIntegrity())
            return;

        placeableObjectsManager.Place(item, positionOnGrid);
    }

    public bool Check(Vector3Int position)
    {
        if (!VerifyIntegrity())
            return false;

        return placeableObjectsManager.Check(position);
    }

    internal void PickUp(Vector3Int gridPosition)
    {
        if (!VerifyIntegrity())
            return;

        placeableObjectsManager.PickUp(gridPosition);
    }

    internal bool VerifyIntegrity()
    {
        if (placeableObjectsManager == null)
            return false;        
        return true;
    }
}
