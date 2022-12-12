using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName ="Data/Tool Action/ Watering")]

public class Watering : ToolAction
{
    [SerializeField] CropsContainer crops;

    //this makes the things happen on the tilemap
    public override bool OnApplyToTilemap(Vector3Int gridPosition, TileMapReadController tileMapReadController, Item item)
    {
        if(crops.Get(gridPosition) == null){
                
            return false;
        }
        if(item.capacity <= 0){return false;}
        tileMapReadController.cropsManager.Watering(gridPosition);
        return true;
    }

    //this to make something to the item that we are going to rest 1 to the capacity
    public override void OnItemUsed(Item usedItem, ItemContainer inventory)
    {
        usedItem.capacity -= 1;
    }

}
