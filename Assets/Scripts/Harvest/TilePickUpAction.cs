using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Data/Tool Action/Harvest")]

public class TilePickUpAction : ToolAction
{
    //this is overrided cause i want to keep it like this if i generate another player we can or cant give it to a new character
    public override bool OnApplyToTilemap(Vector3Int gridPosition, TileMapReadController tileMapReadController, Item item)
    {
        //this pick up the crops
        tileMapReadController.cropsManager.PickUp(gridPosition);

        //this is to pick up the placeable items
        tileMapReadController.objectsManager.PickUp(gridPosition);
        return true;
    }
}
