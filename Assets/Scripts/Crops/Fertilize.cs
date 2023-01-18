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
            tileMapReadController.cropsManager.Fertilize(gridPosition, item);
            return true;

        }
        return false;
    }

}
