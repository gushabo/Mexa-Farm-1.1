using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arcade : Interactable
{
    public override void Interact(Character character)
    {
        //getting the script of the character
        UsingArcade enter = character.GetComponent<UsingArcade>();
        if(enter == null){return;}
        //enter the function of the other script in the character
        enter.OpenMenu();
    }
}
