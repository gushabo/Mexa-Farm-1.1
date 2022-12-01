using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefillBottle : Interactable
{
    public override void Interact(Character character)
    {

        for (int i = 0; i < 10; i++)
        {
            if(GameManager.instance.InventoryContainer.slots[i].item == null){continue;}
            if (GameManager.instance.InventoryContainer.slots[i].item.id == 4)
            {
                GameManager.instance.InventoryContainer.slots[i].item.capacity = GameManager.instance.InventoryContainer.slots[i].item.maxCapacity;
            }
        }
    }
}
