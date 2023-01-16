using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Data/Tool Action/Place Object")]
public class PlaceObject : ToolAction
{
    
    [SerializeField] List<TileBase> CanPlow;

    public override bool OnApplyToTilemap(Vector3Int gridPosition, TileMapReadController tileMapReadController, Item item)
    {

        TileBase tileToPlow = tileMapReadController.GetTileBase(gridPosition);
        if(CanPlow.Contains(tileToPlow) == false)
        {
            return false;
        }

        if (tileMapReadController.objectsManager.Check(gridPosition) == true)
        {
            return false;
        }

        tileMapReadController.objectsManager.Place(item, gridPosition);

        return true;

    }

}
