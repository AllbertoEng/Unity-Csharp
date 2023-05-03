using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapCropsManager : TimeAgent
{
    [SerializeField] TileBase plowed;
    [SerializeField] TileBase seeded;
    Tilemap targetTilemap;
    Tilemap TargetTilemap
    {
        get
        {
            GameObject go = GameObject.Find("CropsTilemap");
            if (go == null)
                return null;

            targetTilemap = go.GetComponent<Tilemap>();
            return targetTilemap;
        }

    }

    [SerializeField] GameObject cropsSpritePrefab;
    [SerializeField] CropsContainer container;

    private void Start()
    {
        GameManager.instance.GetComponent<CropsManager>().cropsManager = this;
        targetTilemap = GetComponent<Tilemap>();
        onTimeTick += Tick;
        Init();
    }

    public void Tick()
    {
        
        if (targetTilemap == null)
            return;

        foreach (CropTile cropTile in container.crops)
        {
            if (cropTile.crop == null)
                continue;

            cropTile.damage += 0.02f;

            if (cropTile.damage > 1f)
            {
                cropTile.Harvested();
                targetTilemap.SetTile(cropTile.position, plowed);
                continue;
            }

            if (cropTile.Complete)
            {
                continue;
            }

            cropTile.growTimer += 1;

            if (cropTile.growTimer >= cropTile.crop.growthStageTime[cropTile.growStage])
            {                
                cropTile.renderer.gameObject.SetActive(true);
                cropTile.renderer.sprite = cropTile.crop.sprites[cropTile.growStage];

                cropTile.growStage += 1;
            }
                   
        }
    }

    internal bool Check(Vector3Int position)
    {
        return container.Get(position) != null;
    }

    public void Plow(Vector3Int position)
    {
        CreatePlowedTile(position);
    }

    public void Seed(Vector3Int position, Crop toSeed)
    {
        CropTile tile = container.Get(position);

        if (tile == null)
            return;

        targetTilemap.SetTile(position, seeded);

        tile.crop = toSeed;
    }

    private void CreatePlowedTile(Vector3Int position)
    {
        CropTile crop = new CropTile();
        container.Add(crop);

        GameObject go = Instantiate(cropsSpritePrefab);
        go.transform.position = targetTilemap.CellToWorld(position);
        go.transform.position -= Vector3.forward * 0.01f;
        go.SetActive(false);
        crop.renderer = go.GetComponent<SpriteRenderer>();

        crop.position = position;

        targetTilemap.SetTile(position, plowed);
    }

    internal void PickUp(Vector3Int gridPosition)
    {
        Vector2Int position = (Vector2Int)gridPosition;

        CropTile tile = container.Get(gridPosition);
        if (tile == null)
            return;

        if (tile.Complete)
        {
            ItemSpawManager.instance.SpawnItem(targetTilemap.CellToWorld(gridPosition), tile.crop.yield, tile.crop.count);

            targetTilemap.SetTile(gridPosition, plowed); 
            tile.Harvested();
        }
    }
}
