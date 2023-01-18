using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuseumChar : MonoBehaviour
{

    public static MuseumChar instance;

    //public bool buyIt;
    public int numBttons;

    [SerializeField] GameObject panel;

    [SerializeField] Button coins;
    [SerializeField] Button watermalon;
    [SerializeField] Button carrots;
    [SerializeField] Button milk;
    [SerializeField] Button egg;

    int id_item;
    int amount;

    private void Awake()
    {
        if (instance == null)
        {
            MuseumChar.instance = this;
        }
        else { Destroy(gameObject); }
    }

    private void Update()
    {
        if (numBttons == 5)
        {
            CloseMenu();
            Destroy(GameObject.Find("MuseumFences"));
            numBttons = 0;
            //buyIt = true;
            
        }
    }

    public void OpenMenu()
    {
        panel.SetActive(true);
        gameObject.GetComponent<InventoryController>().panel.SetActive(true);
        gameObject.GetComponent<InventoryController>().toolbarPanel.SetActive(false);
    }

    public void CloseMenu()
    {
        panel.SetActive(false);
        gameObject.GetComponent<InventoryController>().panel.SetActive(false);
        gameObject.GetComponent<InventoryController>().statusPanel.SetActive(false);
        gameObject.GetComponent<InventoryController>().toolbarPanel.SetActive(true);
    }


    public void Buy(int id)
    {
        switch (id)
        {
            case 0:
                {
                    if (GameManager.instance.player.GetComponent<Currency>().Check(150))
                    {
                        GameManager.instance.player.GetComponent<Currency>().Decrease(150);
                        coins.interactable = false;
                        numBttons++;
                    }
                    break;
                }

            case 1:
                {
                    id_item = 18;
                    amount = 10;
                    break;
                }

            case 2:
                {
                    id_item = 16;
                    amount = 20;
                    break;
                }

            case 3:
                {
                    id_item = 19;
                    amount = 5;
                    break;
                }

            case 4:
                {
                    id_item = 20;
                    amount = 12;
                    break;
                }

            default:
                break;
        }

        if (id != 0)
        {
            for (int i = 0; i < 10; i++)
            {
                if (GameManager.instance.InventoryContainer.slots[i].item == null) { continue; }
                if (GameManager.instance.InventoryContainer.slots[i].item.id == id_item)
                {
                    GameManager.instance.InventoryContainer.Remove(GameManager.instance.InventoryContainer.slots[i].item, amount);
                    numBttons++;
                    switch (id)
                    {
                        case 1:
                            {
                                watermalon.interactable = false;
                                break;
                            }

                        case 2:
                            {
                                carrots.interactable = false;
                                break;
                            }

                        case 3:
                            {
                                milk.interactable = false;
                                break;
                            }

                        case 4:
                            {
                                egg.interactable = false;
                                break;
                            }

                        default:
                            break;
                    }

                }

            }

        }

    }

}
