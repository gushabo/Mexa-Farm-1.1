using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyFences : Interactable
{

    public int priceToDestroy;
    [SerializeField] bool open;

    public override void Interact(Character character)
    {
        if (open == false)
        {
            Open(character);
        }
        else
        {
            Close(character);
        }
    }


    public void Open(Character character)
    {
        open = true;
        BuyFences buyFences = character.GetComponent<BuyFences>();
        if (buyFences == null) {return; }
        buyFences.OpenMenu(this, transform);
    }

    public void Close(Character character)
    {
        BuyFences buyFences = character.GetComponent<BuyFences>();
        if (buyFences == null) {return; }
        buyFences.CloseMenu();
        open = false;
    }

}
