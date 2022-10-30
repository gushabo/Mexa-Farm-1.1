using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ToolsCharacterController : MonoBehaviour
{
    //get the another script of the character
    CharacterController2D characterController2D;
    Character character;
    Rigidbody2D rgbd2D;
    Animator animator;

    //Get the reference for my toolbar
    ToolbarController toolbarController;

    //this OffSetDistance is to interact with the tools
    [SerializeField] float OffSetDistance = 1f;

    //[SerializeField] float SizeOfInteractableArea = 1.2f; i didn't use this var LMAO
    
    [SerializeField] MarkerManager markerManager;
    [SerializeField] TileMapReadController tileMapReadController;
    //this MaxDistance is to use it on the ground
    [SerializeField] float MaxDistance = 1f;

    //this var is to pick up the crops
    [SerializeField] ToolAction onTilePickUp;

    //this is to the highlight of the item placement
    [SerializeField] IconHighlight iconHighlight;

    //this is to know the tile that we select
    Vector3Int selectedTilePosition;
    bool selectable;

    private void Awake()
    {
        character = GetComponent<Character>();
        characterController2D = GetComponent<CharacterController2D>();
        rgbd2D = GetComponent<Rigidbody2D>();
        toolbarController = GetComponent<ToolbarController>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        SelectTile();
        CanSelectCheck();
        Marker();
        //if we press the left mouse button
        if (Input.GetMouseButtonDown(0))
        {
            if (UseToolWorld() == true)
            {
                return;
            }
            UseToolGrid();
        }
    }

    public void SelectTile()
    {
        selectedTilePosition = tileMapReadController.GetGridPosition((Vector2)Input.mousePosition, true);
    }

    void CanSelectCheck()
    {
        Vector2 CharacterPosition = transform.position;
        Vector2 CameraPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        selectable = Vector2.Distance(CharacterPosition, CameraPosition) < MaxDistance;
        markerManager.Show(selectable);
        iconHighlight.CanSelect = selectable;
    }

    private void Marker()
    {
        markerManager.markedCellPosition = selectedTilePosition;
        //ay a ver esto deberia de tener que jalar jaja salu2
        iconHighlight.cellPosition = selectedTilePosition;
    }

    public bool UseToolWorld()
    {
        //gets the distance that the player can interact
        Vector2 position = rgbd2D.position + characterController2D.LastMotionVector * OffSetDistance;

        //gets the item if the player is handling one
        Item item = toolbarController.GetItem;
        if (item == null) { return false; }
        if (item.OnAction == null) { return false; }
        
        //this calls to the cost of energy it'll take to realize an action
        EnergyCost(item.OnAction.energyCost);

        //sets the animator to the animation of act (is unexistant)
        animator.SetTrigger("act");

        //checks if the action has been completed
        bool complete = item.OnAction.OnApply(position);

        if (complete == true)
        {
            if (item.onItemUsed != null)
            {
                item.onItemUsed.OnItemUsed(item, GameManager.instance.InventoryContainer);
            }

        }

        return complete;
    }

    private void EnergyCost(int energyCost)
    {
        character.GetTired(energyCost);

    }

    public void UseToolGrid()
    {

        if (selectable == true)
        {
            Item item = toolbarController.GetItem;
            if (item == null) { 
                PickUpTile();    
                return; 
            }

            if (item.onTileMapAction == null) { return; }

            EnergyCost(item.onTileMapAction.energyCost);

            animator.SetTrigger("act");
            bool complete = item.onTileMapAction.OnApplyToTilemap(selectedTilePosition, tileMapReadController, item);

            if (complete == true)
            {
                if (item.onItemUsed != null)
                {
                    item.onItemUsed.OnItemUsed(item, GameManager.instance.InventoryContainer);
                }

            }

        }
    }

    private void PickUpTile()
    {
        if(onTilePickUp == null){ return;}
        onTilePickUp.OnApplyToTilemap(selectedTilePosition, tileMapReadController, null);
    }
}
