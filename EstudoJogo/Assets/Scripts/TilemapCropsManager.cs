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

    private void OnDestroy()
    {
        for (int i = 0; i < container.crops.Count; i++)
        {
            container.crops[i].renderer = null;
        }
    }

    private void Start()
    {
        if (GameManager.instance != null)
            GameManager.instance.GetComponent<CropsManager>().cropsManager = this;
        targetTilemap = GetComponent<Tilemap>();
        onTimeTick += Tick;
        Init();
        VisualizeMap();
    }

    private void VisualizeMap()
    {
        for (int i = 0; i < container.crops.Count; i++)
        {
            VisualizeTile(container.crops[i]);
        }
    }

    public void Tick()
    {
        
        if (targetTilemap == null)
            return;

        foreach (CropTile cropTile in container.crops)
        {
            if (cropTile.crop == null)
                continue;

            cropTile.damage += 0f;//0.02f;

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
        if (Check(position))
            return;
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

    public void VisualizeTile(CropTile cropTile)
    {
        targetTilemap.SetTile(cropTile.position, cropTile.crop != null ? seeded : plowed);

        if (cropTile.renderer == null)
        {

            GameObject go = Instantiate(cropsSpritePrefab, transform);
            go.transform.position = targetTilemap.CellToWorld(cropTile.position) + targetTilemap.cellSize / 2;
            go.transform.position -= Vector3.forward * 0.01f;
            go.SetActive(false);
            cropTile.renderer = go.GetComponent<SpriteRenderer>();
        }

        bool growing = cropTile.crop != null && cropTile.growTimer >= cropTile.crop.growthStageTime[0];


        
        if (growing)
        {            
            cropTile.renderer.sprite = cropTile.crop.sprites[cropTile.growStage - 1];
            cropTile.renderer.gameObject.SetActive(true);
        }
            
    }

    private void CreatePlowedTile(Vector3Int position)
    {
        CropTile crop = new CropTile();
        container.Add(crop);        

        crop.position = position;

        VisualizeTile(crop);

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

            tile.Harvested();
            VisualizeTile(tile);
        }
    }
}
