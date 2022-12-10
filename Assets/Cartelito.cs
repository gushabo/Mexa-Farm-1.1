using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cartelito : MonoBehaviour
{

    [SerializeField] GameObject items;
    public int id_objeto = -1;
    public bool find;
    [SerializeField] GameObject radish;
    [SerializeField] GameObject carrot;
    [SerializeField] GameObject watermelon;
    [SerializeField] GameObject strawberry;
    [SerializeField] GameObject cow;
    [SerializeField] GameObject hen;

    [SerializeField] Button rabano;
    [SerializeField] Button zana;
    [SerializeField] Button melon;
    [SerializeField] Button fresa;
    [SerializeField] Button vaca;
    [SerializeField] Button gallina;


    private void Start()
    {
        GameManager.instance.listaMuseo.Add(this);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            Debug.Log("entra al collider");
            if (Input.GetKeyDown(KeyCode.F))
            {
                Debug.Log("Presiona f");
                for (var i = 0; i < GameManager.instance.listaMuseo.Count; i++)
                {
                    if(GameManager.instance.listaMuseo[i].id_objeto == 15)
                    {
                        rabano.interactable = false;
                    }
                    if(GameManager.instance.listaMuseo[i].id_objeto == 16)
                    {
                        zana.interactable = false;
                    }
                    if(GameManager.instance.listaMuseo[i].id_objeto == 18)
                    {
                        melon.interactable = false;
                    }
                    if(GameManager.instance.listaMuseo[i].id_objeto == 17)
                    {
                        fresa.interactable = false;
                    }
                    if(GameManager.instance.listaMuseo[i].id_objeto == 30)
                    {
                        vaca.interactable = false;
                    }
                    if(GameManager.instance.listaMuseo[i].id_objeto == 31)
                    {
                        gallina.interactable = false;
                    }
                }
                items.SetActive(!items.activeSelf);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            items.SetActive(false);
        }
    }

    public void AgregarItem(int id)
    {
        items.SetActive(false);
        switch (id)
        {
            case 0:
                {
                    id_objeto = 15;
                    break;
                }
            case 1:
                {
                    id_objeto = 16;
                    break;
                }

            case 2:
                {
                    id_objeto = 18;
                    break;
                }

            case 3:
                {
                    id_objeto = 17;
                    break;
                }

            case 4: 
            {
                id_objeto = 30;
                break;
            }

            case 5:
            {
                id_objeto = 31;
                break;
            }

            default:break;
        }

        for (int i = 0; i < 10; i++)
        {
            if(GameManager.instance.InventoryContainer.slots[i].item == null){continue;}
            if(GameManager.instance.InventoryContainer.slots[i].item.id == id_objeto)
            {
                GameManager.instance.InventoryContainer.Remove(GameManager.instance.InventoryContainer.slots[i].item);
                find = true;
                GameManager.instance.cartelesPuestos++;
            }
        }

        if(!find){return;}

        switch (id)
        {
            case 0:
                {
                    radish.SetActive(true);
                    break;
                }

            case 1:
                {
                    Debug.Log("entra aqui");
                    carrot.SetActive(true);
                    break;
                }

            case 2:
                {
                    watermelon.SetActive(true);
                    break;
                }

            case 3:
                {
                    strawberry.SetActive(true);
                    break;
                }

            case 4:
                {
                    cow.SetActive(true);
                    break;
                }

            case 5:
                {
                    hen.SetActive(true);
                    break;
                }

            default:
                {
                    break;
                }
        }
    }
}
