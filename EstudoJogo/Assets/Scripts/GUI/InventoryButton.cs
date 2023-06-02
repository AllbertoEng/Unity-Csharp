using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] Image icon;
    [SerializeField] Text text;
    [SerializeField] Image highlight;

    int myIndex;

    ItemPanel itemPanel;

    public void SetIndex(int index)
    {
        myIndex = index;
    }

    public void SetItemPanel(ItemPanel source)
    {
        this.itemPanel = source;
    }

    public void Set(ItemSlot slot)
    {
        icon.sprite = slot.item.icon;
        icon.gameObject.SetActive(true);

        if (slot.item.stackable == true)
        {
            text.gameObject.SetActive(true);
            text.text = slot.count.ToString();
        }
        else
        {
            text.gameObject.SetActive(false);
        }
    }

    public void Clean()
    {
        icon.sprite = null;
        icon.gameObject.SetActive(false);
        text.gameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {        
        itemPanel.OnClick(myIndex);
    }

    public void Highlight(bool b)
    {
        highlight.gameObject.SetActive(b);
    }
}
