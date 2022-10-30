using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableObjectsReferenceManager : MonoBehaviour
{
    public PlaceableObjectsManager placeableObjectsManager;
    public void Place(Item item, Vector3Int pos)
    {
        if(placeableObjectsManager == null)
        {
            Debug.LogWarning("no placeable objects reference");
            return;
        }
        placeableObjectsManager.Place(item, pos);
    }

    //checks is the space is available to put an item
    public bool Check(Vector3Int pos)
    {
        if(placeableObjectsManager == null)
        {
            Debug.LogWarning("no esta checado pa");
            return false;
        }
        return placeableObjectsManager.Check(pos);
    }

    internal void PickUp(Vector3Int gridPosition)
    {
        if(placeableObjectsManager == null)
        {
            Debug.LogWarning("ni recoger puedes pa");
            return;
        }
        placeableObjectsManager.PickUp(gridPosition);
    }
}
