using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Trading : MonoBehaviour
{
    //have the store and the inventory panel
    [SerializeField] GameObject storePanel;
    [SerializeField] GameObject inventoryPanel;
    [SerializeField] GameObject SellPanel;
    //inventory of the player(container)
    [SerializeField] ItemContainer Playerinventory;
    //inventory of the player(panels)
    [SerializeField] ItemPanel inventoryItemPanel;

    [SerializeField] GameObject noMoney;
    [SerializeField] GameObject noObj;
    [SerializeField] GameObject NoSpace;

    //this is the thing that u can buy
    Store store;
    //the money of the player
    Currency money;

    int days = 0;

    bool nono = false;
    float x = 1.5f;

    bool noobj = false;
    bool nospa = false;

    int sellPrice;

    ItemStorePanel itemStorePanel;
    ItemStorePanel sellStorePanel;

    public List<Button> botones;
    public int[] ventaDiaria = new int[4];

    private void Awake()
    {
        money = GetComponent<Currency>();
        itemStorePanel = storePanel.GetComponent<ItemStorePanel>();
        sellStorePanel = SellPanel.GetComponent<ItemStorePanel>();
    }

    private void Update()
    {
        //check if the day pass to put in 0 the buys per day
        if (days < DayTimeController.days)
        {
            days = DayTimeController.days;
            for(int i=0; i < 4; i++)
            {
                ventaDiaria[i] = 0;
                botones[i].interactable = true;
            }
        }

        if (nono == true)
        {
            x -= Time.deltaTime;
            if (x <= 0)
            {
                noMoney.SetActive(false);
                x = 1.5f;
                nono = false;
            }
        }
        if (noobj == true)
        {
            x -= Time.deltaTime;
            if (x <= 0)
            {
                noObj.SetActive(false);
                x = 1.5f;
                noobj = false;
            }
        }

        if (nospa == true)
        {
            x -= Time.deltaTime;
            if (x <= 0)
            {
                NoSpace.SetActive(false);
                x = 1.5f;
                nospa = false;
            }
        }

    }

    public void BeginTrading(Store store)
    {
        //starts the variable of store like the one we have in this same script
        this.store = store;

        itemStorePanel.SetInventory(store.storeContent);
        sellStorePanel.SetInventory(store.SellContent);

        //when u start to trade show the panels
        storePanel.SetActive(true);
        inventoryPanel.SetActive(true);

    }

    public void BuyItem(int id)
    {
        bool freeSpace = false;
        bool sameItem = false;
        //save the item and his price
        //gets the item that is selected of the container
        Item itemToBuy = store.storeContent.slots[id].item;
        int totalPrice = itemToBuy.priceToBuy;
        //go through the inventory searching if we have the same item we wanna buy or if we same space in the inventory
        for (int i = 0; i < GameManager.instance.InventoryContainer.slots.Count; i++)
        {
            if (GameManager.instance.InventoryContainer.slots[i].item == null)
            {
                freeSpace = true;
                continue;
            }
            if (itemToBuy == GameManager.instance.InventoryContainer.slots[i].item)
            {
                sameItem = true;
                continue;
            }
        }
        //if we dont have space or the item is not the same that we have in our inventory we return
        if (!freeSpace && !sameItem) { NoSpace.SetActive(true); nospa = true; return; }

        //if we had money to but the item
        if (money.Check(totalPrice) == true)
        {
            //substract the money
            money.Decrease(totalPrice);
            //adds the item to the inventory
            Playerinventory.Add(itemToBuy);
            //Update the inventory to see that the items that the player is buying
            inventoryItemPanel.Show();

        }
        else
        {
            noMoney.SetActive(true);
            nono = true;
        }

    }

    //here we close the trading panel
    public void StopTrading()
    {
        //gets the var of the store to null
        store = null;
        //put invisible all the panels
        storePanel.SetActive(false);
        inventoryPanel.SetActive(false);
    }

    //here we sell the item
    public void SellItem(int id)
    {
        int index = -1;
        Item itemToSell = store.SellContent.slots[id].item;

        for (int i = 0; i < 10; i++)
        {
            if (Playerinventory.slots[i].item == null) continue;

            if (itemToSell.id == Playerinventory.slots[i].item.id)
            {
                index = i;
            }

        }

        if (index != -1)
        {
            //obtains the real price for all the items of one type
            sellPrice = itemToSell.priceToSell;
            //substract the money
            money.Add(sellPrice);
            //adds the item to the inventory
            Playerinventory.Remove(Playerinventory.slots[index].item);
            //Update the inventory to see that the items that the player is selling
            inventoryItemPanel.Show();
            switch (id)
            {
                case 0:
                    ventaDiaria[0]++;
                    if(ventaDiaria[0] == 15)
                    {
                        botones[0].interactable = false;
                    }
                    break;
                case 1:
                    ventaDiaria[1]++;
                    if(ventaDiaria[1] == 15)
                    {
                        botones[1].interactable = false;
                    }
                    break;
                case 2:
                    ventaDiaria[2]++;
                    if(ventaDiaria[2] == 15)
                    {
                        botones[2].interactable = false;
                    }
                    break;
                case 3:
                    ventaDiaria[3]++;
                    if(ventaDiaria[3] == 15)
                    {
                        botones[3].interactable = false;
                    }
                    break;

                default: break;
            }


        }
        else
        {
            noObj.SetActive(true);
            noobj = true;
        }

    }

}
