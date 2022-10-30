using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName ="Data/Tool Action/ Plow")]

public class PlowTile : ToolAction
{
    [SerializeField] List<TileBase> CanPlow;
    [SerializeField] AudioClip onPlowUsed;

    public override bool OnApplyToTilemap(Vector3Int grindPosition, TileMapReadController tileMapReadController, Item item)
    {
        TileBase tileToPlow = tileMapReadController.GetTileBase(grindPosition);
        if(CanPlow.Contains(tileToPlow) == false)
        {
            return false;
        }

        tileMapReadController.cropsManager.Plow(grindPosition);
        AudioManager.instance.Play(onPlowUsed);

        return true;
    }
}
