using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public void Awake()
    {
        instance = this;
    }

    //here we are calling the scripts or other gameObjects that we are using 
    public GameObject player;
    public ItemContainer InventoryContainer;
    public ItemDragNDropController dragNDropController;
    public DayTimeController timeController;
    public DialogueSystem dialogueSystem;
    public PlaceableObjectsReferenceManager placeableObjects;
    public ScreenTint screenTint;
    public CrowActions crowActions;
    [SerializeField] GameObject PricePanel;

    public List<CorralMenu> listaCorralMenu;

    public List<Cartelito> listaMuseo;
    public int cartelesPuestos;

    private void Update() {
        if(cartelesPuestos == 6)
        {
            Premio();
            cartelesPuestos = 0;
        }
    }

    private void Premio()
    {
        //dar premio al jugador por colocar todos los items
        PricePanel.SetActive(true);
        player.GetComponent<Currency>().Add(100);
    }

    public void CerrarPanel()
    {
        PricePanel.SetActive(false);
    }

}
