using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Collider2D))]
public class ResourceNode : ToolHit
{
    [SerializeField] GameObject pickUpDrop;    
    [SerializeField] float spread = 2.5f;

    [SerializeField] int dropCount = 25;
    [SerializeField] int itemCountInOneDrop = 1;
    [SerializeField] Item item;
    [SerializeField] ResourceNodeType resourceNodeType;

    public override void Hit()
    {
        while (dropCount > 0)
        {
            dropCount -= 1;

            Vector3 position = this.transform.position;
            position.x += (spread * Random.value) - (spread / 2);
            position.y += (spread * Random.value) - (spread / 2);

            ItemSpawManager.instance.SpawnItem(position, item, itemCountInOneDrop);
        }
        Destroy(gameObject);
    }

    public override bool CanBeHit(List<ResourceNodeType> canBeHitBy)
    {
        return canBeHitBy.Contains(resourceNodeType);
    }
}
