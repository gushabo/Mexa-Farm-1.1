using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tradingAnimals : MonoBehaviour
{
    //the panel of the shop
    [SerializeField] GameObject principalPanel;
    //the items that the player can sell
    [SerializeField] ItemContainer sellItems;
    //the items that the player can buy
    [SerializeField] ItemContainer buyItems;
    //the money of the player
    Currency money;
    //the inventory of the player
    [SerializeField] ItemContainer inventory;

    private void Start()
    {
        money = GetComponent<Currency>();
    }

    public void OpenMenu()
    {
        principalPanel.SetActive(true);
        gameObject.GetComponent<InventoryController>().panel.SetActive(true);
        gameObject.GetComponent<InventoryController>().toolbarPanel.SetActive(false);
    }

    public void CloseMenu()
    {
        principalPanel.SetActive(false);
        gameObject.GetComponent<InventoryController>().panel.SetActive(false);
        gameObject.GetComponent<InventoryController>().toolbarPanel.SetActive(true);
    }

    public void BuyItem(int id)
    {
        Item item = sellItems.slots[id].item;

        if (money.Check(item.priceToBuy))
        {
            //remove the money from the inventory
            money.Decrease(item.priceToBuy);
            //adds the item to the inventory
            inventory.Add(item);
        }


    }

    public void SellItems(int id)
    {
        //the id it's obtain from the button and it is the position of the container
        int index = -1;
        //the item that we are going to sell
        Item item = buyItems.slots[id].item;
        //the price of the item
        int sellPrice;
        //checking all the space in the inventory
        for (int i = 0; i < 10; i++)
        {
            if (inventory.slots[i].item == null) { continue; }
            if (inventory.slots[i].item.id == item.id)
            {
                index = i;
            }
        }

        //if we get item we do this if
        if (index != -1)
        {
            //obtains the real price for all the items of one type
            sellPrice = item.priceToSell;
            //the id = 1 is from the egg that a special case
            if (id == 1)
            {
                //checks if we have 6 or more eggs
                if (inventory.slots[index].count >= 6)
                {
                    //removes the item to the inventory
                    inventory.Remove(inventory.slots[index].item, 6);
                    //substract the money
                    money.Add(sellPrice);
                }
            }
            else
            {
                //removes the item to the inventory
                inventory.Remove(inventory.slots[index].item);
                //substract the money
                money.Add(sellPrice);
            }


        }


    }

}
