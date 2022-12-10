using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimalPart1 : MonoBehaviour
{

    //the panel
    [SerializeField] GameObject principalPanel;
    //the inventory of the player
    [SerializeField] ItemContainer playerInventory;
    //the instance to use the variables in other scripts
    public static AnimalPart1 instance;
    //the quantity of buttons that have been paid
    int bttnPay = 0;
    //all the bttns
    [SerializeField] Button StrawBttn;
    [SerializeField] Button WaterBttn;
    [SerializeField] Button RadishBttn;
    [SerializeField] Button moneyBttn;

    //the variable that know if all the buttons have been paid
    public bool buyIt;
    //the money of the player
    Currency money;

    private void Awake()
    {
        if (instance == null)
        {
            AnimalPart1.instance = this;
        }
        else { Destroy(gameObject); }
    }

    private void Update()
    {
        if (bttnPay == 4)
        {
            buyIt = true;
            principalPanel.SetActive(false);
            gameObject.GetComponent<InventoryController>().panel.SetActive(false);
            gameObject.GetComponent<InventoryController>().toolbarPanel.SetActive(true);
        }
    }

    void Start()
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

    public void BuyBttn(int id)
    {
        int index = -1;
        int count = -1;

        if (id == 0)
        {
            index = 17;
            count = 20;
        }

        if (id == 1)
        {
            index = 18;
            count = 5;
        }

        if (id == 2)
        {
            index = 15;
            count = 10;
        }


        if (id != 3)
        {
            for (int i = 0; i < 10; i++)
            {
                if (playerInventory.slots[i].item == null) { continue; }

                if (playerInventory.slots[i].item.id == index)
                {
                    if (playerInventory.slots[i].count >= count)
                    {
                        playerInventory.Remove(playerInventory.slots[i].item, count);
                        if (id == 0)
                        {
                            StrawBttn.interactable = false;
                            bttnPay++;
                        }
                        if (id == 1)
                        {
                            WaterBttn.interactable = false;
                            bttnPay++;
                        }
                        if (id == 2)
                        {
                            RadishBttn.interactable = false;
                            bttnPay++;
                        }
                    }
                }
            }
        }
        else
        {
            if (money.Check(25))
            {
                money.Decrease(25);
                moneyBttn.interactable = false;
                bttnPay++;
            }
        }

    }

}
