using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopOffersCharacter : MonoBehaviour
{

    //the offer panel and the inventory panel just for UI
    [SerializeField] GameObject offersPanel;
    [SerializeField] GameObject inventoryPanel;
    //get the shop
    GameObject go;
    //samanabitch i just want to fking die already

    int days = 0;
    bool changeDay;

    ItemContainer container;
    [SerializeField] ItemContainer oneItem;
    ItemStorePanel storePanel;

    Item DailyItem;

    Currency money;


    private void Awake()
    {
        storePanel = offersPanel.GetComponent<ItemStorePanel>();
        money = GetComponent<Currency>();
    }


    void Update()
    {
        go = GameObject.Find("MorritaDOfertas");
        if (go == null) { return; }
        if (DayTimeController.days > days)
        {
            go.SetActive(true);
            days++;
            HideShop();
        }
    }

    IEnumerator HideShop()
    {
        yield return new WaitForSeconds(60);
    }

    public void SetDailyItem(ShopOffers shopOffers)
    {

        //get the new item of the day
        container = shopOffers.itemContainer;
        DailyItem = container.slots[UnityEngine.Random.Range(0, container.slots.Count - 1)].item;
        Debug.Log("nombre del item: " + DailyItem.Name);
        //erase the item of the day before
        if (oneItem.slots.Count != 0)
        {
            oneItem.slots.Clear();
            Debug.Log("numero de items: " + oneItem.slots.Count);
        }
        //add the item of the day
        oneItem.Add(DailyItem);
        storePanel.SetInventory(oneItem);
    }

    public void begin(ShopOffers shopOffers)
    {
        if(!changeDay){SetDailyItem(shopOffers);}
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

    }



}
