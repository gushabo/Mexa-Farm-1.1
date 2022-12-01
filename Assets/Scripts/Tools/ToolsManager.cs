using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ToolsManager : MonoBehaviour
{
    //the inventory of the player
    [SerializeField] ItemContainer inventory;
    //the 
    [SerializeField] ItemContainer tools;
    int axe, plow, water, pickaxe, scissors;
    int axelvl, plowlvl, waterlvl = 0, pickaxelvl, scissorslvl;
    //the text of the price
    [SerializeField] TextMeshProUGUI plowprice;
    [SerializeField] TextMeshProUGUI axeprice;
    [SerializeField] TextMeshProUGUI pickaxeprice;
    [SerializeField] TextMeshProUGUI waterprice;
    [SerializeField] TextMeshProUGUI waterCapacity;
    [SerializeField] GameObject wata;
    [SerializeField] TextMeshProUGUI scissorsprice;
    //change all the images of the buttons
    [SerializeField] Image Renderer;
    [SerializeField] Image Renderer1;
    [SerializeField] Image Renderer2;
    [SerializeField] Image Renderer3;
    [SerializeField] Image Renderer4;

    [SerializeField] Sprite x;

    Currency money;

    private void Start()
    {
        money = GetComponent<Currency>();
    }

    public void CheckingPrices()
    {

        axe = -1; plow = -1; water = -1; pickaxe = -1; scissors = -1;

        //pass through all the inventory and looks for the id's of the items
        for (int i = 0; i < 10; i++)
        {

            if (inventory.slots[i].item == null) continue;

            //id's of the plow
            if (inventory.slots[i].item.id == 1)
            {
                plow = i;
                plowlvl = 1;
            }
            else
            {
                if (inventory.slots[i].item.id == 22)
                {
                    plow = i;
                    plowlvl = 2;
                }
                else
                {
                    if (inventory.slots[i].item.id == 26)
                    {
                        plowlvl = 3;
                        plow = i;
                    }
                    else { plowprice.text = "???"; }
                }
            }

            //id's of the axe
            if (inventory.slots[i].item.id == 2)
            {
                axe = i;
                axelvl = 1;
            }
            else
            {
                if (inventory.slots[i].item.id == 23)
                {
                    axe = i;
                    axelvl = 2;
                }
                else
                {
                    if (inventory.slots[i].item.id == 27) { axe = i; axelvl = 3; }
                    else { axeprice.text = "???"; }
                }
            }

            //id's of the pickaxe
            if (inventory.slots[i].item.id == 3)
            {
                pickaxe = i;
                pickaxelvl = 1;
            }
            else
            {
                if (inventory.slots[i].item.id == 24)
                {
                    pickaxe = i;
                    pickaxelvl = 2;
                }
                else
                {
                    if (inventory.slots[i].item.id == 28) { pickaxe = i; pickaxelvl = 3; }
                    else { pickaxeprice.text = "???"; }
                }
            }

            //lvl of the water container
            if (inventory.slots[i].item.id == 4)
            {
                water = i;
                if (waterlvl == 0) { waterlvl = 1; }
                else
                {
                    if (waterlvl == 1) { waterlvl = 2; }
                }
            }
            else
            {
                waterprice.text = "???";
            }

            //id's of the scissors
            if (inventory.slots[i].item.id == 21)
            {
                scissors = i;
                scissorslvl = 1;
            }
            else
            {
                if (inventory.slots[i].item.id == 25)
                {
                    scissors = i;
                    scissorslvl = 2;
                }
                else
                {
                    if (inventory.slots[i].item.id == 29) { scissors = i; scissorslvl = 3; }
                    else { scissorsprice.text = "???"; }
                }
            }

        }

        if (plow != -1)
        {
            if (plowlvl == 1) { Renderer.sprite = tools.slots[0].item.Icon; plowprice.text = "50"; }
            if (plowlvl == 2) { Renderer.sprite = tools.slots[5].item.Icon; plowprice.text = "75"; }
            if (plowlvl == 3) { Renderer.sprite = tools.slots[5].item.Icon; plowprice.text = "max"; }

        }
        if (axe != -1)
        {
            if (axelvl == 1) { Renderer1.sprite = tools.slots[1].item.Icon; axeprice.text = "50"; }
            if (axelvl == 2) { Renderer1.sprite = tools.slots[6].item.Icon; axeprice.text = "75"; }
            if (axelvl == 3) { Renderer1.sprite = tools.slots[6].item.Icon; axeprice.text = "max"; }
        }
        if (pickaxe != -1)
        {
            if (pickaxelvl == 1) { Renderer2.sprite = tools.slots[2].item.Icon; pickaxeprice.text = "50"; }
            if (pickaxelvl == 2) { Renderer2.sprite = tools.slots[7].item.Icon; pickaxeprice.text = "75"; }
            if (pickaxelvl == 3) { Renderer2.sprite = tools.slots[7].item.Icon; pickaxeprice.text = "max"; }
        }
        if (water != -1)
        {
            if (waterlvl == 1) { wata.SetActive(true); waterprice.text = "25"; waterCapacity.text = "10"; }
            if (waterlvl == 2) { wata.SetActive(true); waterprice.text = "50"; waterCapacity.text = "20"; }
            if (waterlvl == 3) { wata.SetActive(true); waterprice.text = "max"; waterCapacity.text = "20"; }
            Renderer3.sprite = tools.slots[3].item.Icon;
        }
        if (scissors != -1)
        {
            if (scissorslvl == 1) { Renderer4.sprite = tools.slots[4].item.Icon; scissorsprice.text = "50"; }
            if (scissorslvl == 2) { Renderer4.sprite = tools.slots[8].item.Icon; scissorsprice.text = "75"; }
            if (scissorslvl == 3) { Renderer4.sprite = tools.slots[8].item.Icon; scissorsprice.text = "max"; }
        }


    }

    public void UpgradePlow()
    {
        int price;
        if (plow == -1) { return; }
        if (inventory.slots[plow].item.id == 1)
        {
            price = 50;
            if (money.Check(price) == true)
            {
                money.Decrease(price);
                inventory.Remove(inventory.slots[plow].item);
                inventory.Add(tools.slots[0].item);
                Renderer.sprite = tools.slots[5].item.Icon;
            }
            else { Debug.Log("no tienes el dinero"); }
        }
        else
        {
            if (inventory.slots[plow].item.id == 22)
            {
                price = 75;
                if (money.Check(price) == true)
                {
                    money.Decrease(price);
                    inventory.Remove(inventory.slots[plow].item);
                    inventory.Add(tools.slots[5].item);
                    plowprice.text = "max";
                }
                else { Debug.Log("no tienes el dinero"); }
            }
        }
    }

    public void UpgradeAxe()
    {
        int price;
        if (axe == -1) { return; }
        if (inventory.slots[axe].item.id == 2)
        {
            price = 50;
            if (money.Check(price) == true)
            {
                money.Decrease(price);
                inventory.Remove(inventory.slots[axe].item);
                inventory.Add(tools.slots[1].item);
                Renderer1.sprite = tools.slots[6].item.Icon;
            }
            else { Debug.Log("no tienes el dinero"); }
        }
        else
        {
            if (inventory.slots[axe].item.id == 23)
            {
                price = 75;
                if (money.Check(price) == true)
                {
                    money.Decrease(price);
                    inventory.Remove(inventory.slots[axe].item);
                    inventory.Add(tools.slots[6].item);
                    axeprice.text = "max";
                }
                else { Debug.Log("no tienes el dinero"); }
            }
        }
    }

    public void UpgradePickaxe()
    {
        int price;
        if (pickaxe == -1) { return; }
        if (inventory.slots[pickaxe].item.id == 3)
        {
            price = 50;
            if (money.Check(price) == true)
            {
                money.Decrease(price);
                inventory.Remove(inventory.slots[pickaxe].item);
                inventory.Add(tools.slots[2].item);
                Renderer2.sprite = tools.slots[7].item.Icon;
            }
            else { Debug.Log("no tienes el dinero"); }
        }
        else
        {
            if (inventory.slots[pickaxe].item.id == 24)
            {
                price = 75;
                if (money.Check(price) == true)
                {
                    money.Decrease(price);
                    inventory.Remove(inventory.slots[pickaxe].item);
                    inventory.Add(tools.slots[7].item);
                    pickaxeprice.text = "max";
                }
                else { Debug.Log("no tienes el dinero"); }
            }
        }
    }

    public void UpgradeWater()
    {
        int price;
        if (water == -1) { return; }
        if (inventory.slots[water].item.id == 4)
        {
            if (waterlvl == 1)
            {
                price = 25;
                if (money.Check(price))
                {
                    money.Decrease(price);
                    inventory.slots[water].item.maxCapacity = 10;
                    inventory.slots[water].item.capacity = 10;
                    waterprice.text = "50"; waterCapacity.text = "20";
                    waterlvl = 2;
                    inventory.slots[water].item.lvl = 2;
                }
            }
            else
            {
                price = 50;
                if (money.Check(price))
                {
                    money.Decrease(price);
                    inventory.slots[water].item.maxCapacity = 20;
                    inventory.slots[water].item.capacity = 20;
                    waterlvl = 3;
                    waterprice.text = "max";
                    inventory.slots[water].item.lvl = 3;
                }
            }
        }
    }

    public void UpgradeScissors()
    {
        int price;
        if (scissors == -1) { return; }
        if (inventory.slots[scissors].item.id == 21)
        {
            price = 50;
            if (money.Check(price) == true)
            {
                money.Decrease(price);
                inventory.Remove(inventory.slots[scissors].item);
                inventory.Add(tools.slots[4].item);
                Renderer4.sprite = tools.slots[8].item.Icon;
            }
            else { Debug.Log("no tienes el dinero"); }
        }
        else
        {
            if (inventory.slots[scissors].item.id == 25)
            {
                price = 75;
                if (money.Check(price) == true)
                {
                    money.Decrease(price);
                    inventory.Remove(inventory.slots[scissors].item);
                    inventory.Add(tools.slots[8].item);
                    scissorsprice.text = "max";
                }
                else { Debug.Log("no tienes el dinero"); }
            }
        }



    }
}
