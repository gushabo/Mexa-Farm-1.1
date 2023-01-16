using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField] Button btonCorral;
    public int numCorrales = 0;
    public int maxCorrales;

    bool nono = false;
    float x = 1.5f;

    bool noobj = false;
    bool nospa = false;

    int days;

    [SerializeField] GameObject noMoney;
    [SerializeField] GameObject noObj;
    [SerializeField] GameObject NoSpace;

    public List<Button> botones;
    public int[] ventaDiaria = new int[2];

    private void Start()
    {
        money = GetComponent<Currency>();
    }

    private void Update()
    {
        //check if the day pass to put in 0 the buys per day
        if (days < DayTimeController.days)
        {
            days = DayTimeController.days;
            for (int i = 0; i < 2; i++)
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

    public void OpenMenu()
    {
        principalPanel.SetActive(true);
        gameObject.GetComponent<InventoryController>().panel.SetActive(true);
        gameObject.GetComponent<InventoryController>().toolbarPanel.SetActive(false);
        if (gameObject.GetComponent<BuyFences>().fencesD[1] == true && gameObject.GetComponent<BuyFences>().fencesD[2] == false)
        {
            btonCorral.interactable = true;
            maxCorrales = 4;
        }
        else
        {
            btonCorral.interactable = false;
            maxCorrales = 0;
        }

        if (gameObject.GetComponent<BuyFences>().fencesD[2] == true)
        {
            btonCorral.interactable = true;
            maxCorrales = 9;
        }
    }

    public void CloseMenu()
    {
        principalPanel.SetActive(false);
        gameObject.GetComponent<InventoryController>().panel.SetActive(false);
        gameObject.GetComponent<InventoryController>().toolbarPanel.SetActive(true);
    }

    public void BuyItem(int id)
    {

        bool freeSpace = false;
        bool sameItem = false;

        Item item = sellItems.slots[id].item;
        //go through the inventory searching if we have the same item we wanna buy or if we same space in the inventory
        for (int i = 0; i < GameManager.instance.InventoryContainer.slots.Count; i++)
        {
            if (GameManager.instance.InventoryContainer.slots[i].item == null)
            {
                freeSpace = true;
                continue;
            }
            if (item == GameManager.instance.InventoryContainer.slots[i].item)
            {
                sameItem = true;
                continue;
            }
        }

        //if we dont have space or the item is not the same that we have in our inventory we return
        if (!freeSpace && !sameItem) { NoSpace.SetActive(true); nospa = true; return; }

        if (money.Check(item.priceToBuy))
        {
            if (id == 2)
            {
                numCorrales++;
            }
            if (numCorrales > maxCorrales) { numCorrales = maxCorrales; return; }
            //remove the money from the inventory
            money.Decrease(item.priceToBuy);
            //adds the item to the inventory
            inventory.Add(item);
        }
        else
        {
            noMoney.SetActive(true);
            nono = true;
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
                    ventaDiaria[1]++;
                    if(ventaDiaria[1] == 15)
                    {
                        botones[1].interactable = false;
                    }
                }
                else
                {
                    noObj.SetActive(true);
                    noobj = true;
                }
            }
            else
            {
                if (id == 0)
                {
                    //removes the item to the inventory
                    inventory.Remove(inventory.slots[index].item);
                    //substract the money
                    money.Add(sellPrice);
                    ventaDiaria[0]++;
                    if(ventaDiaria[0] == 15)
                    {
                        botones[0].interactable = false;
                    }
                }
                else
                {
                    noObj.SetActive(true);
                    noobj = true;
                }
            }
        }
        else
        {
            noObj.SetActive(true);
            noobj = true;
        }


    }

}
