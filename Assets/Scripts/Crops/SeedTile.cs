using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Data/Tool Action/ Seed Tile")]

public class SeedTile : ToolAction
{
    //this checks if the tile can be seeded
    public override bool OnApplyToTilemap(Vector3Int grindPosition, TileMapReadController tileMapReadController, Item item)
    {
        if(tileMapReadController.cropsManager.Check(grindPosition) == false)
        {
            return false;
        }
        tileMapReadController.cropsManager.Seed(grindPosition, item.crop);
        return true;
    }

    //this use to go to another function to see if an item is used and be if is going to be remove
    public override void OnItemUsed(Item usedItem, ItemContainer inventory)
    {
        inventory.Remove(usedItem);
    }
}
