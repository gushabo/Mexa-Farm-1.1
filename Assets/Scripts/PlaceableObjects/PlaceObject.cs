using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "Data/Tool Action/Place Object")]
public class PlaceObject : ToolAction
{


    public override bool OnApplyToTilemap(Vector3Int gridPosition, TileMapReadController tileMapReadController, Item item)
    {


        if (tileMapReadController.objectsManager.Check(gridPosition) == true)
        {
            return false;
        }

        tileMapReadController.objectsManager.Place(item, gridPosition);

        Debug.Log(tileMapReadController.objectsManager.Check(gridPosition));

        return true;


    }

    //when u use an item this removes 1 from ur inventory
    public override void OnItemUsed(Item usedItem, ItemContainer inventory)
    {
        //inventory.Remove(usedItem);
    }
}
