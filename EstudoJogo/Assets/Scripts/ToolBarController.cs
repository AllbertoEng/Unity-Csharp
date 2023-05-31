using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolBarController : MonoBehaviour
{
    [SerializeField] int toolbarSize = 12;
    int selectedTool;

    public Action<int> onChange;
    [SerializeField] IconHighlight iconHighlight;

    public ItemSlot GetItemSlot
    {
        get
        {
            return GameManager.instance.inventoryContainer.slots[selectedTool];
        }
    }
    public Item GetItem
    {
        get
        {
            return GameManager.instance.inventoryContainer.slots[selectedTool].item;
        }
    }

    private void Start()
    {
        onChange += UpdateHighlightIcon;
        UpdateHighlightIcon(selectedTool);
    }
    private void Update()
    {
        float delta = Input.mouseScrollDelta.y;

        //VERIFICAR UMA FORMA MELHOR DE FAZER ISSO
        if (Input.anyKeyDown)
        {
            string inputTeclado = Input.inputString;
            if (!String.IsNullOrEmpty(inputTeclado))
            {
                foreach (char c in inputTeclado)
                {
                    if (!char.IsDigit(c) || c == '0')
                        return;
                }

                selectedTool = Convert.ToInt32(inputTeclado) - 1;
                selectedTool = (selectedTool >= toolbarSize ? 0 : selectedTool);
                onChange?.Invoke(selectedTool);
            }            
        }
            

        if (delta != 0)
        {
            if (delta > 0)
            {
                selectedTool -= 1;
                selectedTool = (selectedTool <= 0 ? toolbarSize - 1 : selectedTool);                
            }
            else
            {
                selectedTool += 1;
                selectedTool = (selectedTool >= toolbarSize ? 0 : selectedTool);
            }
            onChange?.Invoke(selectedTool);
        }
    }

    internal void Set(int id)
    {
        selectedTool = id;
    }

    public void UpdateHighlightIcon(int id = 0)
    {
        Item item = GetItem;
        if (item == null || !item.iconHighlight)
        {
            iconHighlight.Show = false;
            return;
        }

        if (item.iconHighlight)
        {            
            iconHighlight.Set(item.icon);
            iconHighlight.Show = item.iconHighlight;
        }
    }
}
