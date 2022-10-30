using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableControls : MonoBehaviour
{
    CharacterController2D characterController2D;
    ToolsCharacterController toolsCharacterController;
    InventoryController inventoryController;
    ToolbarController toolbarController;
    ItemContainerInteractController itemContainerInteractController;
    
    //Get all the components of the scripts at the first time
    private void Awake() {
        characterController2D =  GetComponent<CharacterController2D>();
        toolsCharacterController = GetComponent<ToolsCharacterController>();
        inventoryController = GetComponent<InventoryController>();
        toolbarController = GetComponent<ToolbarController>();
        itemContainerInteractController = GetComponent<ItemContainerInteractController>();
    }

    public void DisableControl()
    {
        characterController2D.enabled = false;
        toolbarController.enabled = false;
        inventoryController.enabled = false;
        toolsCharacterController.enabled = false;
        itemContainerInteractController.enabled = false;
    }

    public void DisableTools()
    {
        toolsCharacterController.enabled = false;
    }

    public void EnableControl()
    {
        characterController2D.enabled = true;
        toolbarController.enabled = true;
        inventoryController.enabled = true;
        toolsCharacterController.enabled = true;
        itemContainerInteractController.enabled = true;
    }

}
