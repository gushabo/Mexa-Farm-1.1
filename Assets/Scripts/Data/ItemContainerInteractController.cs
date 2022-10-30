using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemContainerInteractController : MonoBehaviour
{
    //get the items in the item container
    ItemContainer targetItemContainer;
    //gets our inventory
    InventoryController inventoryController;
    //get the panel of the container
    [SerializeField] ItemContainerPanel itemContainerPanel;
    //gets the position of the chest
    Transform openedChest;
    //the max distance of interaction
    [SerializeField] float maxDistance = 0.5f;


    private void Awake()
    {
        inventoryController = GetComponent<InventoryController>();    
    }

    private void Update() {
        if(openedChest != null)
        {
            float distance = Vector2.Distance(openedChest.position, transform.position);
            if(distance > maxDistance)
            {
                openedChest.GetComponent<LootContainerInteract>().Close(GetComponent<Character>());
            }
        }
    }

    public void Open(ItemContainer itemContainer, Transform _openedChest)
    {
        targetItemContainer = itemContainer;
        itemContainerPanel.inventory = targetItemContainer;
        inventoryController.Open();
        itemContainerPanel.gameObject.SetActive(true);
        openedChest = _openedChest;
    }

    public void Close()
    {
        inventoryController.Close();
        itemContainerPanel.gameObject.SetActive(false);
        openedChest = null;

    }

    
}
