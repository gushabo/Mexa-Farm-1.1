using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlaceableObject
{
    public Item placeItem;
    public Transform targetObject;
    public Vector3Int positionOnGrid;
    /// <summary>
    /// this is a serailize JSON string which will contain the state of the objects 
    /// (at the moment we will not be using this)
    /// to know how to serialize a object check the ep 28
    /// </summary>
    public string objectState;

    //the constructor for the placeable objects
    public PlaceableObject(Item item, Vector3Int pos)
    {
        placeItem = item;
        positionOnGrid = pos;
    }
}

[CreateAssetMenu(menuName ="Data/Placeable Objects Container")]
public class PlaceableObjectsContainer : ScriptableObject
{
    public List<PlaceableObject> placeableObjects;

    internal PlaceableObject Get(Vector3Int position)
    {
        return placeableObjects.Find(x => x.positionOnGrid == position);
    }

    internal void Remove(PlaceableObject placedObject)
    {
        placeableObjects.Remove(placedObject);
    }
}
