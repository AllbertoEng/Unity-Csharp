using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[Serializable]
public class CropTile
{
    public int growTimer;
    public int growStage;
    public Crop crop;
    public SpriteRenderer renderer;
    public float damage;
    public Vector3Int position;    

    public bool Complete
    {
        get 
        {
            if (crop == null)            
                return false;
            
            return growTimer >= crop.timeToGrow;
        }
    }

    internal void Harvested()
    {
        growTimer = 0;
        growStage = 0;
        crop = null;
        renderer.gameObject.SetActive(false);
        damage = 0;
    }
}

public class CropsManager : MonoBehaviour
{
    public TilemapCropsManager cropsManager;
    public void PickUp(Vector3Int position)
    {
        if (cropsManager == null)
        {
            Debug.LogWarning("No tilemap crops manager are referenced in the crops manager");
            return;
        }
        cropsManager.PickUp(position);
    }

    public bool Check(Vector3Int position)
    {
        if (cropsManager == null)
        {
            Debug.LogWarning("No tilemap crops manager are referenced in the crops manager");
            return false;
        }
        return cropsManager.Check(position);
    }

    public void Seed(Vector3Int position, Crop toSeed)
    {
        if (cropsManager == null)
        {
            Debug.LogWarning("No tilemap crops manager are referenced in the crops manager");
            return;
        }
        cropsManager.Seed(position, toSeed);
    }

    public void Plow(Vector3Int position)
    {
        if (cropsManager == null)
        {
            Debug.LogWarning("No tilemap crops manager are referenced in the crops manager");
            return;
        }
        cropsManager.Plow(position);
    }

    public void RemovePlow(Vector3Int position)
    {
        if (cropsManager == null)
        {
            Debug.LogWarning("No tilemap crops manager are referenced in the crops manager");
            return;
        }
        cropsManager.RemovePlow(position);
    }
}
