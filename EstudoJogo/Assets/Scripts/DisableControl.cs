using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableControl : MonoBehaviour
{
    CharacterControler2D characterControler2D;
    ToolsCharacterController toolsCharacter;
    InventoryController inventoryController;
    ToolBarController toolBarController;
    ItemContainerInteractController itemContainerInteractController;
    private void Awake()
    {
        characterControler2D = GetComponent<CharacterControler2D>();
        toolsCharacter = GetComponent<ToolsCharacterController>();
        inventoryController = GetComponent<InventoryController>();
        toolBarController = GetComponent<ToolBarController>();
        itemContainerInteractController = GetComponent<ItemContainerInteractController>();
    }

    public void DisableControls()
    {
        characterControler2D.enabled = false;
        toolsCharacter.enabled = false;
        inventoryController.enabled = false;
        toolBarController.enabled = false;
        itemContainerInteractController.enabled = false;
    }

    public void EnableControls()
    {
        characterControler2D.enabled = true;
        toolsCharacter.enabled = true;
        inventoryController.enabled = true;
        toolBarController.enabled = true;
        itemContainerInteractController.enabled = true;
    }
}
