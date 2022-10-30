using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapReadController : MonoBehaviour
{
    [SerializeField] Tilemap tilemap;
    public CropsManager cropsManager;
    public PlaceableObjectsReferenceManager objectsManager;

    //this gets the tilemap in general
    public Vector3Int GetGridPosition(Vector2 Position, bool mousePosition)
    {

        if(tilemap == null)
        {
            tilemap = GameObject.Find("BaseTilemap").GetComponent<Tilemap>();
        }

        if(tilemap == null){return Vector3Int.zero;}
        
        Vector3 worldPosition;

        if(mousePosition)
        {
            worldPosition = Camera.main.ScreenToWorldPoint(Position);
        }else{
            worldPosition = Position;
        }
        Vector3Int gridPosition = tilemap.WorldToCell(worldPosition);

        return gridPosition;
    }

    //this gets the type of tile
    public TileBase GetTileBase(Vector3Int gridPosition)
    {
        if(tilemap == null)
        {
            tilemap = GameObject.Find("BaseTilemap").GetComponent<Tilemap>();
        }

        if(tilemap == null){return null;}

        TileBase tile = tilemap.GetTile(gridPosition);

        return tile;
    }

}
