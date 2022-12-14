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

    private void Update() {
        if(!GameManager.instance.player.GetComponent<FishGameCharacter>().startGame)
        {
            gameObject.SetActive(true);
        }else
        {
            gameObject.SetActive(false);
        }

    }

}
