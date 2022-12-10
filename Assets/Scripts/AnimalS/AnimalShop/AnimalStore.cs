using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalStore : Interactable
{

    public override void Interact(Character character)
    {
        tradingAnimals trading = character.GetComponent<tradingAnimals>();
        if(trading == null){return;}
        trading.OpenMenu();
    }

}
