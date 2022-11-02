using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using TMPro;

public class TilemapCropsManager : MonoBehaviour
{
    //the type of tile that we can have
    [SerializeField] TileBase plowed;
    [SerializeField] TileBase seeded;
    [SerializeField] TileBase Dirt;
    //the tilemap we are working on
    Tilemap targetTilemap;
    //the days that have passed
    public int days = 0;

    //the container when is going to have all the crops
    [SerializeField] public CropsContainer container;

    //the crop whole object
    [SerializeField] GameObject cropsSpritePrefab;

    private void Start()
    {

        GameManager.instance.GetComponent<CropsManager>().cropsManager = this;
        targetTilemap = GetComponent<Tilemap>();
        VisualizeMap();
    }

    private void Update()
    {

        //this is to checks that the player can't water the plant twice in a range of 30 seconds
        foreach (var cropTile in container.crops)
        {
            if (cropTile.watered == true)
            {
                cropTile.waterTime -= Time.deltaTime;
                if (cropTile.waterTime <= 0)
                {
                    cropTile.watered = false;
                    cropTile.waterTime = 30;
                }
            }
        }

        //checks if we have some tile map that we are looking the crops
        if (targetTilemap == null) { return; }

        foreach (CropTile cropTile in container.crops)
        {
            if (cropTile.crop == null) { continue; }

            //0.02 * 50 ticks it will cause that the crop die
            if (cropTile.damage >= 1f)
            {
                cropTile.Harvested();
                targetTilemap.SetTile(cropTile.position, plowed);
                continue;
            }

            if (cropTile.Complete)
            {
                Debug.Log("It's completely grown");
                continue;
            }

            //this checks when the time is 00:00 on the clock we check the time
            if (DayTimeController.days > days)
            {

                //checks if the crop has been irrigate at least once but not all the times
                if (cropTile.CurrWater < cropTile.crop.MaxWater && cropTile.CurrWater > 0)
                {
                    //sets the damage in 0
                    cropTile.damage = 0;
                    cropTile.fertilize = false;

                }

                //this is to get damage to the crop when the player don't water the plant
                else if (cropTile.CurrWater == 0)
                {
                    cropTile.damage += 0.5f;
                    cropTile.fertilize = false;
                }

                //this is when the player water the crops the necesary times
                else if (cropTile.CurrWater >= cropTile.crop.MaxWater)
                {
                    //sets the gameObject in true to see the sprite
                    cropTile.renderer.gameObject.SetActive(true);
                    //set the sprite in the growStage
                    cropTile.renderer.sprite = cropTile.crop.sprites[cropTile.growStage];
                    //add 1 to the growStage
                    cropTile.growStage += 1;

                    //check if it is fertilized
                    if (cropTile.fertilize == true)
                    {
                        //set the sprite in the growStage
                        cropTile.renderer.sprite = cropTile.crop.sprites[cropTile.growStage];
                        cropTile.growStage += 1;
                        cropTile.fertilize = false;

                    }


                }
                //this puts all the current water in 0 every day and the fertilize var
                cropTile.CurrWater = 0;
                cropTile.waterTime = 0;
                cropTile.watered = false;

            }

        }
        days = DayTimeController.days;
        

    }

    private void VisualizeMap()
    {
        for (int i = 0; i < container.crops.Count; i++)
        {
            VisualizeTile(container.crops[i]);
        }
    }

    private void OnDestroy()
    {
        for (int i = 0; i < container.crops.Count; i++)
        {
            container.crops[i].renderer = null;
        }
    }

    internal void Fertilize(Vector3Int position)//this is to add the fertilize to the crops
    { //the way that this function works is just a copy of the watering system only change the variable that affects

        //the index works for not reaching o reaching certain crop for fertilize
        int index = -1;
        //this goes to every crop until someone on the list is equal to the one that are we searching
        for (int i = 0; i < container.crops.Count; i++)
        {
            if (container.crops[i].position == position)
            {
                //this gets the exact index of the crop
                index = i;
            }
        }
        if (index == -1)
        {
            return;
        }
        if (container.crops[index].dayPlanted != DayTimeController.days)
        {
            return;
        }
        else
        {
            //this adds the fetilize to the crop
            if (container.crops[index].fertilize == false)
            {
                container.crops[index].fertilize = true;
            }
        }

    }

    internal void Watering(Vector3Int position)
    {

        //the index works for not reaching o reaching certain crop for water the specific
        int index = -1;
        //this goes to every crop until someone on the list is equal to the one that are we searching
        for (int i = 0; i < container.crops.Count; i++)
        {
            if (container.crops[i].position == position)
            {
                //this gets the exact index of the crop
                index = i;
            }
        }
        if (index == -1)
        {
            return;
        }
        //this adds one to the current water

        if (container.crops[index].watered == false)
        {
            container.crops[index].watered = true;
            container.crops[index].CurrWater += 1;
        }

    }

    //check if we use that spot for some crops (for not having two crops in the same space)
    public bool Check(Vector3Int position)
    {
        return container.Get(position) != null;
    }

    //this plow the tile
    public void Plow(Vector3Int position)
    {
        if (Check(position) == true) { return; }
        CreatePlowedTile(position);
    }

    //this changue to a seeded tile
    public void Seed(Vector3Int position, Crop toSeed)
    {
        CropTile tile = container.Get(position);
        if (tile == null) { return; }
        targetTilemap.SetTile(position, seeded);
        tile.crop = toSeed;
    }

    public void VisualizeTile(CropTile cropTile)
    {

        //this will destroy the crops if u harvest
        if (cropTile.harvest == true)
        {
            targetTilemap.SetTile(cropTile.position, Dirt);
            container.Substract(cropTile);
        }

        //this is the change of the tile state in the info
        //this checks if its plowed or seeded its depends if its null or not
        targetTilemap.SetTile(cropTile.position, cropTile.crop != null ? seeded : Dirt);

        //check if it has a sprite and if this doesn't it will give him one
        if (cropTile.renderer == null)
        {
            //this changes the states of the crop (just the sprites) - to seeded to a grown ass crop
            GameObject go = Instantiate(cropsSpritePrefab, transform);
            go.transform.position = targetTilemap.CellToWorld(cropTile.position);
            go.transform.position -= Vector3.forward * 0.01f;
            cropTile.renderer = go.GetComponent<SpriteRenderer>();
        }

        //this checks if the position is seeded and if it past the first stage of growing (i will have to change the second part)
        bool growing = cropTile.crop != null && cropTile.growStage >= cropTile.crop.growthStageTime[0];

        //this is to show the sprite
        cropTile.renderer.gameObject.SetActive(growing);
        if (growing == true)
        {
            cropTile.renderer.sprite = cropTile.crop.sprites[cropTile.growStage - 1];
        }
    }

    //this creates the plow tile
    private void CreatePlowedTile(Vector3Int position)
    {

        //this only add the position and the state of the tile to the dictionary
        CropTile crop = new CropTile();
        container.Add(crop);

        crop.position = position;

        VisualizeTile(crop);

        //this changes the view of the tile
        targetTilemap.SetTile(position, plowed);
    }

    //this is pickUp the crops
    internal void PickUp(Vector3Int gridPosition)
    {
        Vector2Int position = (Vector2Int)gridPosition;
        CropTile tile = container.Get(gridPosition);
        if (tile == null) { return; }

        //this is use to Spawn the item getting the info of the crop ':D
        if (tile.Complete)
        {
            //this spawn the crop
            ItemSpawnManager.instance.SpawnItem(targetTilemap.CellToWorld(gridPosition), tile.crop.yield, tile.crop.count);
            //this sets all values in 0

            tile.Harvested();
            VisualizeTile(tile);

        }
    }


}
