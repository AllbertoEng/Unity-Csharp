using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Data/Tool Action/Plow")]
public class PlowTile : ToolAction
{
    [SerializeField] List<TileBase> canPlow;
    [SerializeField] AudioClip onPlowUsed;
    public override bool OnApplyToTileMap(Vector3Int gridPosition, TileMapReadController tileMapReadController, Item item)
    {
        TileBase tileToPlow = tileMapReadController.GetTileBase(gridPosition);

        if (!canPlow.Contains(tileToPlow))
        {
            return false;
        }

        tileMapReadController.cropsManager.Plow(gridPosition);

        AudioManager.instance.Play(onPlowUsed);

        return true;
    }
}
