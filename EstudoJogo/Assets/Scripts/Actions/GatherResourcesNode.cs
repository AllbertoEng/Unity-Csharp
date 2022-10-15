using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ResourceNodeType
{
    Undefined,
    Tree,
    Ore
}

[CreateAssetMenu(menuName ="Data/Tool Action/Gather Resource Node")]
public class GatherResourcesNode : ToolAction
{
    [SerializeField] float sizeOfInteractableArea = 1f;
    [SerializeField] List<ResourceNodeType> canHitNodesOfType;

    public override bool OnApply(Vector2 wordPoint)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(wordPoint, sizeOfInteractableArea);

        foreach (Collider2D c in colliders)
        {
            ToolHit hit = c.GetComponent<ToolHit>();
            if (hit != null)
            {
                if (hit.CanBeHit(canHitNodesOfType))
                {
                    hit.Hit();
                    return true;
                }                
            }
        }
        return false;
    }
}
