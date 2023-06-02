using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Tool Action/Remove Plow")]
public class RemovePlow : ToolAction
{
    [SerializeField] List<TileBase> canRemovePlow;
    [SerializeField] AudioClip onRemovePlowUsed;
    public override bool OnApplyToTileMap(Vector3Int gridPosition, TileMapReadController tileMapReadController, Item item)
    {
        TileBase tileToPlow = tileMapReadController.GetTileBase(gridPosition);

        if (!canRemovePlow.Contains(tileToPlow))
        {
            return false;
        }

        tileMapReadController.cropsManager.RemovePlow(gridPosition);

        AudioManager.instance.Play(onRemovePlowUsed);

        return true;
    }
}
