using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemDragNDropController : MonoBehaviour
{
    [SerializeField] public ItemSlot itemSlot;
    [SerializeField] GameObject ItemIcon;
    RectTransform iconTransform;
    Image itemIconImage;

    private void Start()
    {
        itemSlot = new ItemSlot();
        iconTransform = ItemIcon.GetComponent<RectTransform>();
        itemIconImage = ItemIcon.GetComponent<Image>();
    }

    internal bool CheckForSale()
    {
        //we checks if we have the item and if it can be sold
        if(itemSlot.item == null){return false;}
        if(itemSlot.item.canBeSold == false){return false;}
        return true;
    }

    private void Update()
    {
        //if the itemIcon is visible its true
        if (ItemIcon.activeInHierarchy == true)
        {
            //this gives the icon the mouse position
            iconTransform.position = Input.mousePosition;
            
            if (Input.GetMouseButtonDown(0))
            {
                //this checks if we are clicking in anything that is a UI
                if (EventSystem.current.IsPointerOverGameObject() == false)
                {
                    Vector3 WorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    WorldPosition.z = 0;
                    ItemSpawnManager.instance.SpawnItem(WorldPosition, itemSlot.item, itemSlot.count);

                    itemSlot.Clear();
                    ItemIcon.SetActive(false);
                }
            }
        }
    }

    internal void OnClick(ItemSlot itemSlot)
    {
        //when we click on one object in the slots of the inventory
        if (this.itemSlot.item == null)
        {
            //we will copy the object to pass it to another slot and clear the slot were it was
            this.itemSlot.Copy(itemSlot);
            itemSlot.Clear();
        }
        else
        {
            //we pass it to the same slot
            Item item = itemSlot.item;
            int count = itemSlot.count;

            itemSlot.Copy(this.itemSlot);
            this.itemSlot.Set(item, count);
        }
        UpdateIcon();
    }

    //this only takes the sprite of the icon and activates
    public void UpdateIcon()
    {
        if (itemSlot.item == null)
        {
            ItemIcon.SetActive(false);
        }
        else
        {
            ItemIcon.SetActive(true);
            itemIconImage.sprite = itemSlot.item.Icon;
        }
    }
}
