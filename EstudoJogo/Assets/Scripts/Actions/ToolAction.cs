using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolAction : ScriptableObject
{
    public int energyCost = 0;
    public virtual bool OnApply(Vector2 wordPoint)
    {
        Debug.LogWarning("OnApply is not implemented");
        return true;
    }

    public virtual bool OnApplyToTileMap(Vector3Int gridPosition, TileMapReadController tileMapReadController, Item item)
    {
        Debug.LogWarning("OnApplyToTileMap is not implemented");
        return true;
    }

    public virtual bool OnItemUsed(Item usedItem, ItemContainer inventory)
    {
        Debug.LogWarning("OnItemUsed is not implemented");
        return true;
    }
}
