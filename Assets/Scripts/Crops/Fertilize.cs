using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Tool Action/ Fertilize")]
public class Fertilize : ToolAction
{
    CropsContainer container;

    public override bool OnApplyToTilemap(Vector3Int gridPosition, TileMapReadController tileMapReadController, Item item)
    {   

        if (tileMapReadController.cropsManager.Check(gridPosition))
        {
            tileMapReadController.cropsManager.Fertilize(gridPosition);
            return true;

        }
        return false;
    }

    //this use to go to another function to see if an item is used and be if is going to be remove
    public override void OnItemUsed(Item usedItem, ItemContainer inventory)
    {
        inventory.Remove(usedItem);
    }

}
