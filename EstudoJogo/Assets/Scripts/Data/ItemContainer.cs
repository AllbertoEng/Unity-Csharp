using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemSlot
{
    public Item item;
    public int count;

    public void Copy(ItemSlot slot)
    {
        item = slot.item;
        count = slot.count;
    }

    public void Set(Item item, int count)
    {
        this.item = item;
        this.count = count;
    }

    public void Clear()
    {
        item = null;
        count = 0;
    }
}

[CreateAssetMenu(menuName = "Data/Item Container")]
public class ItemContainer : ScriptableObject
{
    public List<ItemSlot> slots;

    public void Add(Item item, int count = 1)
    {
        if (item.stackable == true)
        {
            ItemSlot itemSlot = slots.Find(slot => slot.item == item);
            if (itemSlot != null)
            {
                itemSlot.count += count;
            }
            else
            {
                itemSlot = slots.Find(slot => slot.item == null);
                if (itemSlot != null)
                {
                    itemSlot.item = item;
                    itemSlot.count = count;
                }
            }
        }
        else
        {
            //add non stackable
            ItemSlot itemSlot = slots.Find(slot => slot.item == null);
            if (itemSlot != null)
            {
                itemSlot.item = item;
            }
        }
    }

    public void RemoveItem(Item itemToRemove, int count = 1)
    {
        if (itemToRemove.stackable)
        {
            ItemSlot itemSlot = slots.Find(x => x.item == itemToRemove);
            if (itemSlot == null)
                return;

            itemSlot.count -= count;
            if (itemSlot.count <= 0)
            {
                itemSlot.Clear();
            }
        }
        else
        {
            while (count > 0)
            {
                count -= 1;

                ItemSlot itemSlot = slots.Find(x => x.item == itemToRemove);
                if(itemSlot == null)
                    return;

                itemSlot.Clear();
            }
        }
    }
}
