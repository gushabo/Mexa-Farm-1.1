using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterFishGame : Interactable
{

    public override void Interact(Character character)
    {
        FishGameCharacter fishGame = character.GetComponent<FishGameCharacter>();
        if(fishGame == null) {return;}
        fishGame.StartGame();
        
    }

}
