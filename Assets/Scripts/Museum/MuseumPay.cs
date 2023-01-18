using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuseumPay : Interactable
{
    public override void Interact(Character character)
    {
        MuseumChar part1 = character.GetComponent<MuseumChar>();
        if(part1 == null){ return; }
        part1.OpenMenu();
    }

    // private void Update() {
    //     if(MuseumChar.instance.buyIt)
    //     {
    //         Destroy(gameObject);
    //     }
    // }
}
