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
}
