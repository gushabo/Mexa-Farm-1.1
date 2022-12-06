using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherSecondManager : MonoBehaviour
{

    [SerializeField] CropsContainer container;
    [SerializeField] TilemapCropsManager manager;
    [SerializeField] ItemContainer chest;

    List<int> x = new List<int>();
    int random;
    Item removeItem;

    private void Update()
    {
        //what happens when it rains
        if (WeatherManager.instance.rain == true)
        {
            Debug.Log("lluvia");
            foreach (CropTile cropTile in container.crops)
            {
                //add water to the crops
                cropTile.CurrWater++;
                //if the crops are not protected by a fence we are going to eliminate them
                if (!cropTile.rainProtect)
                {
                    cropTile.Harvested();
                    manager.targetTilemap.SetTile(cropTile.position, manager.plowed);
                    continue;
                }
            }
            WeatherManager.instance.rain = false;
        }

        //what happens when it is snowing
        if (WeatherManager.instance.snow == true)
        {
            manager.snowCheck = true;
            Debug.Log("se metio aqui nieve");
            //this advance 1 step for the carrots
            foreach (CropTile cropTile in container.crops)
            {
                if (cropTile.isCarrot)
                {
                    Debug.Log("se metio al otro if");
                    //sets the gameObject in true to see the sprite
                    cropTile.renderer.gameObject.SetActive(true);
                    //set the sprite in the growStage
                    cropTile.renderer.sprite = cropTile.crop.sprites[cropTile.growStage];
                    //add 1 to the growStage
                    cropTile.growStage += 1;
                    continue;
                }
            }

            //this eliminates one item for the chest of you dont have the upgrade
            if (ItemContainerInteractController.upgrade == false)
            {
                for (int i = 0; i < chest.slots.Count; i++)
                {
                    if (chest.slots[i].item == null) continue;
                    if (chest.slots[i].item != null)
                    {
                        x.Add(i);
                    }
                    if (x.Count == 0) { return; }
                }
                if (x.Count == 0) { WeatherManager.instance.snow = false; return; }
                random = UnityEngine.Random.Range(0, x.Count);

                removeItem = chest.slots[x[random]].item;
                if (removeItem.Stackable == true)
                {
                    int count = chest.slots[x[random]].count;
                    chest.Remove(removeItem, count);
                    x.RemoveAt(random);
                }
                else
                {
                    chest.Remove(removeItem);
                    x.RemoveAt(random);
                }

            }
            else { Debug.Log("tienes la mejora tonses todo bien"); }
            WeatherManager.instance.snow = false;

        }

        if (WeatherManager.instance.drought)
        {
            Debug.Log("sequia");
            foreach (CropTile cropTile in container.crops)
            {
                cropTile.CurrWater--;
            }
            WeatherManager.instance.drought = false;
        }

    }

}
