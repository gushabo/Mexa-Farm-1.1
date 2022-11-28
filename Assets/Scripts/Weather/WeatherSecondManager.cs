using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherSecondManager : MonoBehaviour
{

    [SerializeField] CropsContainer container;
    [SerializeField] ItemContainer chest;

    List<int> x = new List<int>();
    int random;
    Item removeItem;

    private void Update()
    {
        //what happens when it rains
        if(WeatherManager.instance.rain == true)
        {
            Debug.Log("lluvia");
            foreach (CropTile cropTile in container.crops)
            {
                cropTile.CurrWater++;
            }
            WeatherManager.instance.rain = false;
        }

        //what happens when it is snowing
        if(WeatherManager.instance.snow == true)
        {
            Debug.Log("se metio aqui nieve");
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
                random = UnityEngine.Random.Range(0, x.Count);

                removeItem = chest.slots[x[random]].item;
                chest.Remove(removeItem);
                x.RemoveAt(random);
            }
            else { Debug.Log("tienes la mejora tonses todo bien"); }

            WeatherManager.instance.snow = false;

        }

    }

}
