using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[Serializable]
//this is going to give us all the information about the crops
public class CropTile  //this will keep the info of the tiles crops in each instance 
{
    //this "Crop" only have the basic info of the crops but CropTile have the current state of the crops
    [SerializeField] public Crop crop;
    //public int growTimer;
    public int growStage;
    public SpriteRenderer renderer;
    public float damage;
    public Vector3Int position;
    //this is a var to check if has been watered once
    public bool watered = false;
    //to keep track of time to deny the player to irrigate the crops
    public float waterTime = 30;
    //to check if it's harvested and remove it from the space
    public bool harvest = false;
    //isFertilize
    public bool fertilize = false;
    //day that the crop has been planted
    public int dayPlanted = DayTimeController.days;

    public int CurrWater=0;
    //check if it's protected by a scareCrow
    public bool crowProtect;
    //checks if it's protected by a fence
    public bool rainProtect;
    //check if it's a carrot
    public bool isCarrot;

    //this var is to check if the crop is fully grown
    public bool Complete{
        get{
            if(crop == null){return false;}
            return growStage >= crop.timeToGrow;
        }
    }

    //it sets the crop as a new one
    internal void Harvested()
    {
        growStage = 0;
        crop = null;
        renderer.gameObject.SetActive(false);
        damage = 0;
        harvest = true;
        CurrWater = 0;
        isCarrot = false;
    }
}

public class CropsManager : MonoBehaviour
{
    
    public TilemapCropsManager cropsManager;

    public void PickUp(Vector3Int position)
    {
        if(cropsManager == null)
        {
            Debug.LogWarning("No crops manager attached");
            return;
        }
        cropsManager.PickUp(position);
    }

    public bool Check(Vector3Int position)
    {
        if(cropsManager == null)
        {
            Debug.LogWarning("No crops manager attached");
            return false;
        }
        return cropsManager.Check(position);
    }

    public void Seed(Vector3Int position, Crop toSeed)
    {
        if(cropsManager == null)
        {
            Debug.LogWarning("No crops manager attached");
            return;
        }
        cropsManager.Seed(position, toSeed);
    }

    public void Plow(Vector3Int position)
    {
        if(cropsManager == null)
        {
            Debug.LogWarning("No crops manager attached");
            return;
        }
        cropsManager.Plow(position);
    }

    internal void Watering(Vector3Int position)
    {
        if(cropsManager == null)
        {
            Debug.LogWarning("No crops manager attached");
            return;
        }
        cropsManager.Watering(position);
    }

    internal void Fertilize(Vector3Int position, Item item)
    {
        if(cropsManager == null)
        {
            Debug.LogWarning("No crops manager attached");
            return;
        }
        cropsManager.Fertilize(position, item);
    }
    
}
