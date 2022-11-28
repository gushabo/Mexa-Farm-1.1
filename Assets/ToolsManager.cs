using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ToolsManager : MonoBehaviour
{

    [SerializeField] ItemContainer inventory;
    [SerializeField] ItemContainer tools;
    int axe, plow, water, pickaxe, scissors;
    [SerializeField] TextMeshProUGUI plowprice;
    [SerializeField] TextMeshProUGUI axeprice;
    [SerializeField] TextMeshProUGUI pickaxeprice;
    [SerializeField] TextMeshProUGUI waterprice;
    [SerializeField] Image Renderer;
    [SerializeField] Image Renderer1;
    [SerializeField] Image Renderer2;
    [SerializeField] Image Renderer3;

    [SerializeField] Sprite x;

    public void CheckingPrices()
    {

        axe = 0; plow = 0; water = 0; pickaxe = 0; scissors = 0;
        
        for (int i = 0; i < 10; i++)
        {

            if (inventory.slots[i].item == null) continue;
            if (inventory.slots[i].item.id == 1)
            {
                plow = 1;
            }
            else
            {
                plowprice.text = "???";
                Renderer.sprite = x;
            }
            if (inventory.slots[i].item.id == 2)
            {
                axe = 1;
            }
            else
            {
                axeprice.text = "???";
                Renderer1.sprite = x;
            }
            if (inventory.slots[i].item.id == 3)
            {
                pickaxe = 1;
            }
            else
            {
                pickaxeprice.text = "???";
                Renderer2.sprite = x;
            }
            if (inventory.slots[i].item.id == 4)
            {
                water = 1;
            }
            else
            {
                waterprice.text = "???";
                Renderer3.sprite = x;
            }
        }

        if (plow == 1)
        {
            plowprice.text = "50";
            Renderer.sprite = tools.slots[0].item.Icon;
        }
        if (axe == 1)
        {
            axeprice.text = "50";
            Renderer1.sprite = tools.slots[1].item.Icon;
        }
        if (pickaxe == 1)
        {
            pickaxeprice.text = "50";
            Renderer2.sprite = tools.slots[2].item.Icon;
        }
        if (water == 1)
        {
            waterprice.text = "25";
            Renderer3.sprite = tools.slots[3].item.Icon;
        }


    }

    public void UpgradePlow()
    {
        int index = -1;

        for (int i = 0; i < 10; i++)
        {
            if (inventory.slots[i].item == null) continue;


            index = i;
            Debug.Log("index del inventario: " + index);
            Debug.Log("se encontro");


        }
    }



}
