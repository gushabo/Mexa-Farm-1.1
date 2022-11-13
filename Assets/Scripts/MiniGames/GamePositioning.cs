using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePositioning : MonoBehaviour
{

    //variables out the functionality of the minigame
    //position of the camera
    GameObject cameraPos;
    Vector3 finalPosition;
    //player
    GameObject player;
    //Reward of the player
    Currency money;
    //inventory of the player
    [SerializeField] ItemContainer Playerinventory;

    // Start is called before the first frame update
    void Start()
    {
        cameraPos = GameObject.Find("Main Camera");
        player = GameObject.Find("MainCharacter");
        if(player == null){return;}
        player.GetComponent<DisableControls>().DisableControl();
        finalPosition = cameraPos.transform.position;
        finalPosition.z = 0;
        gameObject.transform.position = finalPosition;
        money = player.GetComponent<Currency>();
        
    }

    
}
