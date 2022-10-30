using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPanel : MonoBehaviour
{
    //this gets our inventary and the buttons that are the slots of the inventory
    public ItemContainer inventory;
    public List<InventoryButton> buttons;

    private void Start() 
    {
        Init();    
    }

    public void Init()
    {
        SetSourcePanel();
        SetIndex();
        Show();
    }

    private void SetSourcePanel()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].SetItemPanel(this);
        }
    }

    private void OnEnable() {
        Clear();
        Show();    
    }

    //this makes the update of the toolbar everytime that we pick an item
    private void LateUpdate() 
    {
        //if the program dont find any inventary i'll return
        if(inventory == null){return;}

        // to update everytime some item has been added or use in the inventory
        if(inventory.isDirty)
        {
            Show();
            inventory.isDirty = false;
        }    
    }

    //Gives the number in the index to the slot
    private void SetIndex()
    {
        for(int i=0; i<buttons.Count;i++)
        {
            buttons[i].SetIndex(i);
        }
    }

    //Shows and clean the slots that are not in use
    public virtual void Show()
    {
        //if the program dont find any inventary i'll return
        if(inventory == null){return;}
        for(int i=0;i<inventory.slots.Count && i<buttons.Count;i++)
        {
            if(inventory.slots[i].item == null)
            {
                buttons[i].Clean();
            }else {
                buttons[i].Set(inventory.slots[i]);
            }
        }
    }

    //Clears all the spaces in the inventory, shop, etc
    public void Clear()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].Clean();
        }
    }

    //set the inventory as some item container that we create
    public void SetInventory(ItemContainer newInventory)
    {
        inventory = newInventory;
    }
    
    public virtual void OnClick(int id){}

}
