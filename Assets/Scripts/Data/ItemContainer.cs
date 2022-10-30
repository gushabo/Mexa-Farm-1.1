using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// the itemSlot have 2 vars that are item and count
[Serializable]
public class ItemSlot
{
    public Item item;
    public int count;

    //this literally copy the characteristics of an item
    public void Copy(ItemSlot slot)
    {
        item = slot.item;
        count = slot.count;
    }

    //this function is going to put in certain slot the info that we want
    public void Set(Item item, int count)
    {
        this.item = item;
        this.count = count;
    }

    //this is the function that clear an item
    public void Clear()
    {
        item = null;
        count = 0;
    }
}

//Create a new type of asset call item container in the data menu
[CreateAssetMenu(menuName = "Data/Item Container")]

public class ItemContainer : ScriptableObject
{
    public List<ItemSlot> slots;
    //this var is to check any time we grab an item
    public bool isDirty;

    public void Add(Item item, int count = 1)
    {
        isDirty = true;

        if (item.Stackable == true)
        {
            //add if there are stackable items
            ItemSlot itemSlot = slots.Find(x => x.item == item);
            if (itemSlot != null)
            {
                itemSlot.count += count;
            }
            else
            {
                itemSlot = slots.Find(x => x.item == null);
                if (itemSlot != null)
                {
                    itemSlot.item = item;
                    itemSlot.count = count;
                }
            }
        }
        else
        {
            //add a non stackable item
            ItemSlot itemSlot = slots.Find(x => x.item == null);
            if (itemSlot != null)
            {
                itemSlot.item = item;
            }
        }

    }

    //this Removes an item when it reaches to 0
    public void Remove(Item itemToRemove, int count = 1)
    {
        isDirty = true;
        if(itemToRemove.Stackable == true)
        {
            ItemSlot itemSlot = slots.Find(x => x.item == itemToRemove);
            if(itemSlot == null){ return;}
            itemSlot.count -= count;
            if(itemSlot.count <= 0)
            {
                itemSlot.Clear();
            }
        }
        else {
            while(count > 0)
            {
                count -= 1;
                ItemSlot itemSlot = slots.Find(x => x.item == itemToRemove);
                if(itemSlot == null){ return;}
                itemSlot.Clear();
            }
        }
    }

    internal bool CheckFreeSpace()
    {
        //this pass through all the inventory checking if we have any empty space
        for(int i = 0; i < slots.Count; i++)
        {
            if(slots[i].item == null)
            {
                return true;
            }
        }
        return false;
    }

    internal bool CheckItem(ItemSlot checkingItem)
    {
        ItemSlot itemSlot = slots.Find(x => x.item == checkingItem.item);
        if(itemSlot == null){ return false;}
        if(checkingItem.item.Stackable){ return itemSlot.count >= checkingItem.count;}
        return true;
    }
    
}
