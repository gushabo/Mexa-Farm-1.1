using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopOffersCharacter : MonoBehaviour
{

    //the offer panel and the inventory panel just for UI
    [SerializeField] GameObject offersPanel;
    [SerializeField] GameObject inventoryPanel;
    [SerializeField] TextMeshProUGUI priceText;
    [SerializeField] Image iconRenderer;

    [SerializeField] public ItemContainer container;
    [SerializeField] ItemContainer oneItem;

    [SerializeField] ItemContainer Playerinventory;
    [SerializeField] ItemPanel inventoryItemPanel;

    int day = 0;

    Item DailyItem;
    int price;

    Currency money;

    private void Awake()
    {
        money = GetComponent<Currency>();
        //Debug.Log(DayTimeController.days + 1);
    }

    public void SetDailyItem(ItemContainer container)
    {

        //get the new item of the day
        DailyItem = container.slots[UnityEngine.Random.Range(0, container.slots.Count)].item;
        //erase the item of the day before
        if (oneItem.slots.Count != 0)
        {
            oneItem.slots.Clear();
        }
        //add the item of the day
        oneItem.Add(DailyItem);

        //set the price depending on the item
        if (DailyItem.Name == "Strawberry")
        {
            price = UnityEngine.Random.Range(1, 31);
        }
        if (DailyItem.Name == "Carrots")
        {
            price = UnityEngine.Random.Range(15, 26);
        }
        if (DailyItem.Name == "Radish")
        {
            price = UnityEngine.Random.Range(10, 26);
        }
        if (DailyItem.Name == "Watermelon")
        {
            price = UnityEngine.Random.Range(20, 31);
        }
        if (DailyItem.Name == "MilkCarton")
        {
            price = UnityEngine.Random.Range(7, 18);
        }
        if (DailyItem.Name == "Egg")
        {
            price = UnityEngine.Random.Range(20, 26);
        }

        SetImageNtext();

    }

    private void SetImageNtext()
    {
        priceText.text = price.ToString();
        iconRenderer.sprite = DailyItem.Icon;
    }

    public void begin()
    {
        if (DayTimeController.days > day)
        {
            SetDailyItem(container);
            day = DayTimeController.days;
        }
        offersPanel.SetActive(true);
        inventoryPanel.SetActive(true);
    }

    public void Close()
    {
        offersPanel.SetActive(false);
        inventoryPanel.SetActive(false);
    }

    public void BuyOffer()
    {
        int index = -1;
        bool eggs = false;
        if (DailyItem.Name == "Egg")
        {
            eggs = true;
        }

        for (int i = 0; i < 10; i++)
        {
            if (Playerinventory.slots[i].item == null) continue;

            if (DailyItem.id == Playerinventory.slots[i].item.id)
            {
                index = i;

            }

        }

        if (index != -1)
        {
            //substract the money
            if (eggs == true)
            {
                if (Playerinventory.slots[index].count >= 6)
                {
                    money.Add(price);
                    //adds the item to the inventory
                    Playerinventory.Remove(Playerinventory.slots[index].item);
                    //Update the inventory to see that the items that the player is selling
                    inventoryItemPanel.Show();
                }
                eggs = false;
            }
            else
            {
                money.Add(price);
                //adds the item to the inventory
                Playerinventory.Remove(Playerinventory.slots[index].item, 6);
                //Update the inventory to see that the items that the player is selling
                inventoryItemPanel.Show();
            }

        }

    }

}
