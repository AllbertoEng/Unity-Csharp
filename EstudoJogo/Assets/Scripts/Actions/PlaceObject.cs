using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName ="Data/Tool Action/Place Object")]
public class PlaceObject : ToolAction
{
    public override bool OnApplyToTileMap(Vector3Int gridPosition, TileMapReadController tileMapReadController, Item item)
    {
        if (tileMapReadController.objectsManager.Check(gridPosition))
            return false;

        tileMapReadController.objectsManager.Place(item, gridPosition);
        return true;
    }

    public override bool OnItemUsed(Item usedItem, ItemContainer inventory)
    {
        inventory.RemoveItem(usedItem);
        return true;
    }
}
