using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapReadController : MonoBehaviour
{
    [SerializeField] Tilemap tilemap;
    public CropsManager cropsManager;   
    public PlaceableObjectsReferenceManager objectsManager;

    public Vector3Int GetGridPosition(Vector2 position, bool mousePosition = false)
    {
        if (tilemap == null)        
            tilemap = GameObject.Find("BaseTilemap").GetComponent<Tilemap>();
        if (tilemap == null)
            return Vector3Int.zero;

        Vector3 worldPosition;

        if (mousePosition)
        {
            worldPosition = Camera.main.ScreenToWorldPoint(position);
        }
        else
        {
            worldPosition = position;
        }

        Vector3Int gridPosition = tilemap.WorldToCell(worldPosition);

        return gridPosition;
    }

    public TileBase GetTileBase(Vector3Int gridPosition)
    {
        if (tilemap == null)
            tilemap = GameObject.Find("BaseTilemap").GetComponent<Tilemap>();
        if (tilemap == null)
            return null;

        TileBase tile = tilemap.GetTile(gridPosition);
        return tile;
    }
}
