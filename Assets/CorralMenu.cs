using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorralMenu : MonoBehaviour
{

    public CorralMenu miCorral;
    public enum animal { vaca, gallina };
    public int recursosOtorgados;
    public GameObject UI;
    public GameObject botonesMejora;

    public SpriteRenderer[] sprites;
    public Sprite vaca;
    public Sprite pollo;

    [SerializeField] List<Item> item;

    public int miAnimal = 0;
    public int cantidadAnimales;
    public int id_animal;
    public bool agrega;

    //mejora alimentador
    public bool alimentador;
    public int alimentoActual;
    public int id_alimento;
    public int alimentoMax;
    //mejora almacenamiento
    public bool almacenamiento;

    public bool topeAlimento;

    void Start()
    {
        GameManager.instance.listaCorralMenu.Add(this);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                UI.SetActive(!UI.activeSelf);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            UI.SetActive(false);
        }
    }

    public void AddAnimal(int ID_animal)
    {

        if (miAnimal == 0)
        { miAnimal = ID_animal; }

        if (miAnimal == ID_animal && cantidadAnimales < 3)
        {

            miAnimal = ID_animal;
            if(miAnimal == 1)
            {
                id_animal = 31;
            }
            else{
                id_animal = 30;
            }
            for(int i = 0; i < 10; i++)
            {
                if(GameManager.instance.InventoryContainer.slots[i].item == null){continue;}
                if(GameManager.instance.InventoryContainer.slots[i].item.id == id_animal)
                {
                    if(id_animal == 31){
                        GameManager.instance.InventoryContainer.Remove(item[2]);
                    }
                    else{
                        GameManager.instance.InventoryContainer.Remove(item[3]);
                    }
                    agrega = true;
                }

            }

            if(!agrega){return;}

            agrega = false;

            cantidadAnimales++;
            alimentoMax++;

            if (cantidadAnimales == 3)
            {
                botonesMejora.SetActive(true);
            }

            if (miAnimal == 1)
            {
                sprites[cantidadAnimales - 1].sprite = pollo;
            }
            else
            {
                sprites[cantidadAnimales - 1].sprite = vaca;
            }
            for (int i = 0; i < sprites.Length; i++)
            {
                if (sprites[i].enabled == false)
                {
                    sprites[i].enabled = true;
                    return;
                }
            }
        }
        else
        {
            Debug.Log("no se puede meter el animal");
        }

    }

    public void MejoraAlimentador()
    {
        if (GameManager.instance.player.GetComponent<Currency>().Check(50))
        {
            GameManager.instance.player.GetComponent<Currency>().Decrease(50);
            alimentoMax = 15;
            alimentador = true;
        }
    }

    public void MejoraAlmacenamiento()
    {
        if (GameManager.instance.player.GetComponent<Currency>().Check(50))
        {
            GameManager.instance.player.GetComponent<Currency>().Decrease(50);
            almacenamiento = true;
        }
    }

    public void RecogerProducto()
    {
        if (miAnimal == 1)
        {
            GameManager.instance.InventoryContainer.Add(item[0], recursosOtorgados);
        }
        else
        {
            GameManager.instance.InventoryContainer.Add(item[1], recursosOtorgados);
        }
        recursosOtorgados -= recursosOtorgados;

    }

    public void GenerarProducto()
    {
        if (!almacenamiento)
        {
            recursosOtorgados = alimentoActual;
            alimentoActual = 0;
        }
        else
        {
            recursosOtorgados += alimentoActual;
            alimentoActual = 0;
            if (recursosOtorgados > 25)
            {
                recursosOtorgados = 25;
            }
        }

    }

    //esto va en el boton de alimentar :D
    public void Alimentar()
    {
        if (miAnimal == 1)
        {
            id_alimento = 15;
        }
        else
        {
            id_alimento = 16;
        }

        for (int i = 0; i < 10; i++)
        {
            if (GameManager.instance.InventoryContainer.slots[i].item == null) { continue; }

            if (GameManager.instance.InventoryContainer.slots[i].item.id == id_alimento)
            {
                RevisionDeAlimentador();
                if (topeAlimento) { return; }
                topeAlimento = false;
                GameManager.instance.InventoryContainer.Remove(GameManager.instance.InventoryContainer.slots[i].item);
            }

        }

    }

    private void RevisionDeAlimentador()
    {

        alimentoActual++;
        if (!alimentador)
        {
            if (alimentoActual > cantidadAnimales)
            {
                alimentoActual = cantidadAnimales;
                topeAlimento = true;
                return;
            }
            topeAlimento = false;
        }
        else
        {
            if (alimentoActual > 15)
            {
                alimentoActual = 15;
                topeAlimento = true;
                return;
            }
            topeAlimento = false;
        }
    }
}
