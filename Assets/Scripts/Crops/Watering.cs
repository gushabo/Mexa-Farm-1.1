using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Data/Tool Action/ Watering")]

public class Watering : ToolAction
{
    [SerializeField] CropsContainer crops;
    int index;
    //this makes the things happen on the tilemap
    public override bool OnApplyToTilemap(Vector3Int gridPosition, TileMapReadController tileMapReadController, Item item)
    {
        if (crops.Get(gridPosition) == null)
        {
            return false;
        }
        if (item.capacity <= 0)
        {
            GameManager.instance.EnviarTexto("your water can is empty");
            return false;
        }
        for(int i =0 ; i< crops.crops.Count; i++)
        {
            if(crops.crops[i].position == gridPosition)
            {
                index = i;  
            }
        }
        if(crops.crops[index].crop == null)
        {
            GameManager.instance.EnviarTexto("you cant irrigate a empty crop");
            return false;
        }
        item.capacity--;
        tileMapReadController.cropsManager.Watering(gridPosition);
        return true;
    }


    //this to make something to the item that we are going to rest 1 to the capacity
    public override void OnItemUsed(Item usedItem, ItemContainer inventory)
    {
    }

}
