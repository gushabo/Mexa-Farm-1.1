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
    //the text of the price
    [SerializeField] TextMeshProUGUI plowprice;
    [SerializeField] TextMeshProUGUI axeprice;
    [SerializeField] TextMeshProUGUI pickaxeprice;
    [SerializeField] TextMeshProUGUI waterprice;
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


        for (int i = 0; i < 10; i++)
        {

            if (inventory.slots[i].item == null) continue;
            if (inventory.slots[i].item.id == 1)
            {
                plow = i;
            }
            else
            {
                plowprice.text = "???";
                Renderer.sprite = x;
            }
            if (inventory.slots[i].item.id == 2)
            {
                axe = i;
            }
            else
            {
                axeprice.text = "???";
                Renderer1.sprite = x;
            }
            if (inventory.slots[i].item.id == 3)
            {
                pickaxe = i;
            }
            else
            {
                pickaxeprice.text = "???";
                Renderer2.sprite = x;
            }
            if (inventory.slots[i].item.id == 4)
            {
                water = i;
            }
            else
            {
                waterprice.text = "???";
                Renderer3.sprite = x;
            }
            if (inventory.slots[i].item.id == 21)
            {
                Debug.Log("tienes el item");
                scissors = i;
                Debug.Log("scissors" + scissors);
            }
            else
            {
                Debug.Log("no tienes el item");
                scissorsprice.text = "???";
                Debug.Log("scissors" + scissors);
                //Renderer4.sprite = x;
            }

        }

        if (plow != -1)
        {
            plowprice.text = "50";
            Renderer.sprite = tools.slots[0].item.Icon;
        }
        if (axe != -1)
        {
            axeprice.text = "50";
            Renderer1.sprite = tools.slots[1].item.Icon;
        }
        if (pickaxe != -1)
        {
            pickaxeprice.text = "50";
            Renderer2.sprite = tools.slots[2].item.Icon;
        }
        if (water != -1)
        {
            waterprice.text = "25";
            Renderer3.sprite = tools.slots[3].item.Icon;
        }
        if (scissors != -1)
        {
            scissorsprice.text = "50";
            Renderer4.sprite = tools.slots[4].item.Icon;
        }


    }

    public void UpgradePlow()
    {
        int price;
        if(plow == -1){return;}
        if (inventory.slots[plow].item.id == 1)
        {
            price = 50;
            if (money.Check(price) == true)
            {
                inventory.Remove(inventory.slots[plow].item);
                inventory.Add(tools.slots[0].item);
            }
            else { Debug.Log("no tienes el dinero"); }
        }
        else { Debug.Log("no es el indice"); }
    }

    public void UpgradeAxe()
    {
        int price;
        if(axe == -1){return;}
        if (inventory.slots[axe].item.id == 2)
        {
            price = 50;
            if (money.Check(price) == true)
            {
                inventory.Remove(inventory.slots[axe].item);
                inventory.Add(tools.slots[1].item);
            }
            else { Debug.Log("no tienes el dinero"); }
        }
        else { Debug.Log("no es el indice"); }
    }

    public void UpgradePickaxe()
    {
        int price;
        if(pickaxe == -1){return;}
        if (inventory.slots[pickaxe].item.id == 3)
        {
            price = 50;
            if (money.Check(price) == true)
            {
                inventory.Remove(inventory.slots[pickaxe].item);
                inventory.Add(tools.slots[2].item);
            }
            else { Debug.Log("no tienes el dinero"); }
        }
        else { Debug.Log("no es el indice"); }
    }

    public void UpgradeScissors()
    {
        int price;
        if(scissors == -1){return;}
        if (inventory.slots[scissors].item.id == 21)
        {
            price = 50;
            if (money.Check(price) == true)
            {
                inventory.Remove(inventory.slots[scissors].item);
                inventory.Add(tools.slots[4].item);
            }
            else { Debug.Log("no tienes el dinero"); }
        }
        else { Debug.Log("no es el indice"); }
    }



}
