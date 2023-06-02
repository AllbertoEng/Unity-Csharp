using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trading : MonoBehaviour
{
    [SerializeField] GameObject storePanel;
    [SerializeField] GameObject inventoryPanel;
    
    Store store;
    Currency money;

    ItemStorePanel itemStorePanel;
    [SerializeField] ItemContainer playerInventory;

    [SerializeField] ItemPanel inventoryItemPanel;

    private void Awake()
    {
        money = GetComponent<Currency>();
        itemStorePanel = storePanel.GetComponent<ItemStorePanel>();
    }
    public void BeginTrading(Store store)
    {
        this.store = store;        
        itemStorePanel.SetInventory(store.storeContainer);

        storePanel.SetActive(true);
        inventoryPanel.SetActive(true);       
    }

    internal void BuyItem(int id)
    {
        Item itemToBuy = store.storeContainer.slots[id].item;
        int totalPrice = (int)(itemToBuy.price * store.sellFromPlayerMultip);
        if (money.Check(totalPrice))
        {
            money.Decreace(totalPrice);
            playerInventory.Add(itemToBuy);
            inventoryItemPanel.Show();
        }

    }

    public void StopTrading()
    {
        store = null;

        storePanel.SetActive(false);
        inventoryPanel.SetActive(false);
    }

    public void SellItem()
    {
        if (GameManager.instance.dragAndDropController.CheckForSale())
        {
            ItemSlot itemToSell = GameManager.instance.dragAndDropController.itemSlot;
            int moneyGain = itemToSell.item.stackable ? (int)(itemToSell.item.price * itemToSell.count * store.buyFromPlayerMultip) 
                : (int)(itemToSell.item.price * store.buyFromPlayerMultip);

            money.Add(moneyGain);
            itemToSell.Clear();
            GameManager.instance.dragAndDropController.UpdateIcon();
        }
    }
}
