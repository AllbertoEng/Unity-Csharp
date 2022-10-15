using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Tool Action/Seed Tile")]
public class SeedTile : ToolAction
{
    public override bool OnApplyToTileMap(Vector3Int gridPosition, TileMapReadController tileMapReadController, Item item)
    {
        if (!tileMapReadController.cropsManager.Check(gridPosition))
            return false;

        tileMapReadController.cropsManager.Seed(gridPosition, item.crop);

        return true;
    }

    public override bool OnItemUsed(Item usedItem, ItemContainer inventory)
    {
        inventory.RemoveItem(usedItem);
        return true;
    }
}
