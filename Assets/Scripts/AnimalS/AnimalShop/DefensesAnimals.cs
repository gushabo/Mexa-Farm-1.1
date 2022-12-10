using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefensesAnimals : Interactable
{

    public override void Interact(Character character)
    {
        AnimalPart1 part1 = character.GetComponent<AnimalPart1>();
        if(part1 == null){ return; }
        part1.OpenMenu();
    }

    private void Update() {
        if(AnimalPart1.instance.buyIt)
        {
            Destroy(gameObject);
        }
    }

}
