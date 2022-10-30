using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlaceableObjectsManager : MonoBehaviour
{
    [SerializeField] PlaceableObjectsContainer placeableObjects;
    [SerializeField] Tilemap targetTilemap;

    private void Start() {
        GameManager.instance.GetComponent<PlaceableObjectsReferenceManager>().placeableObjectsManager = this;
        VisualizeMap();
    }

    private void OnDestroy() {
        for(int i = 0; i < placeableObjects.placeableObjects.Count; i++)
        {
            placeableObjects.placeableObjects[i].targetObject = null;
        }
    }

    //check all the placeable objects in the map
    private void VisualizeMap()
    {
        for(int i = 0; i < placeableObjects.placeableObjects.Count; i ++)
        {
            VisualizeItem(placeableObjects.placeableObjects[i]);
        }
    }

    //gets the info of the item
    private void VisualizeItem(PlaceableObject placeableObject)
    {
        GameObject go = Instantiate(placeableObject.placeItem.itemPrefab);
        Vector3 position = targetTilemap.CellToWorld(placeableObject.positionOnGrid) + targetTilemap.cellSize / 2;
        position -= Vector3.forward * 0.1f;
        go.transform.position = position;
        placeableObject.targetObject = go.transform;
    }

    internal void PickUp(Vector3Int gridPosition)
    {
        PlaceableObject placedObject = placeableObjects.Get(gridPosition);
        if(placedObject == null)
        {
            return;
        }
        ItemSpawnManager.instance.SpawnItem(targetTilemap.CellToWorld(gridPosition), placedObject.placeItem, 1);
        Destroy(placedObject.targetObject.gameObject);
        placeableObjects.Remove(placedObject);
    }

    //check if the position in the map is available
    public bool Check(Vector3Int position)
    {
        return placeableObjects.Get(position) != null; 
    }

    //puts the item in the mnap
    public void Place(Item item, Vector3Int positionGrid)
    {
        if(Check(positionGrid) == true){ return;}
        PlaceableObject placeableObject = new PlaceableObject(item, positionGrid);
        VisualizeItem(placeableObject);
        placeableObjects.placeableObjects.Add(placeableObject);
    }
}
