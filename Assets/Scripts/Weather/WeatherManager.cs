using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeatherStates
{
    Clear,
    Rain,
    Drought,
    Snow
}

public class WeatherManager : MonoBehaviour
{
    //the chances to get a new state
    [Range(0f, 1f)][SerializeField] float chanceToChangeWeather = 0.02f;
    //make the state clear
    public WeatherStates currentWeatherState = WeatherStates.Clear;
    //the days that have to pass to change weather
    public int days = 9;

    [SerializeField] ParticleSystem rainObject;
    [SerializeField] ParticleSystem SnowObject;
    [SerializeField] ParticleSystem DroughtObject;

    //Rain vars
    //the container when all the crops are
    CropsContainer cropsW;
    //the gameObject that get the crops object
    public GameObject goCrops;

    //Snow vars
    //the gameObject that get the lootContainer 
    public GameObject chestContainer;
    //the container from the silo
    ItemContainer itemContainer;
    int x = 0;
    Item removeItem;

    private void Start()
    {
        goCrops = GameObject.Find("CropsTilemap");
        cropsW = goCrops.GetComponent<TilemapCropsManager>().container;
        chestContainer = GameObject.Find("ChestObject");
        itemContainer = chestContainer.GetComponent<LootContainerInteract>().itemContainer;

    }

    private void Update()
    {

        if (days < DayTimeController.days)
        {
            days = DayTimeController.days;
            RandomWeatherChangeCheck();
            RainHappens();
            SnowHappens();
        }

    }

    private void SnowHappens()
    {
        if (currentWeatherState == WeatherStates.Snow)
        {
            if (chestContainer.GetComponent<LootContainerInteract>().upgrade == false)
            {
                for (int i = 0; i < itemContainer.slots.Count; i++)
                {
                    if (itemContainer.slots[i].item == null) continue;
                    x++;
                }
                Debug.Log("x: " + x);
                Debug.Log("rango random de 0 a x: " + UnityEngine.Random.Range(0, x - 1));
                removeItem = itemContainer.slots[UnityEngine.Random.Range(0, x - 1)].item;
                itemContainer.Remove(removeItem);
                x--;
            }

        }
    }

    private void RainHappens()
    {
        if (currentWeatherState == WeatherStates.Rain)
        {
            foreach (CropTile cropTile in cropsW.crops)
            {
                //cropTile.CurrWater++;
            }
            Debug.Log("se rego :D");

        }
    }


    //check is some random value is more than the chance to change
    public void RandomWeatherChangeCheck()
    {
        if (UnityEngine.Random.value < chanceToChangeWeather)
        {
            RandomWeatherChange();
        }
    }

    //get the new weather
    private void RandomWeatherChange()
    {
        WeatherStates newWheatherState = (WeatherStates)UnityEngine.Random.Range(0, Enum.GetNames(typeof(WeatherStates)).Length);
        ChangeWeather(newWheatherState);
    }

    private void ChangeWeather(WeatherStates newWheatherState)
    {
        //get the new weather in the current state
        currentWeatherState = newWheatherState;
        Debug.Log(currentWeatherState);
        UpdateWeather();
    }

    //this is the switch case to activate or deactivate the particle system
    private void UpdateWeather()
    {
        switch (currentWeatherState)
        {
            case WeatherStates.Clear:
                rainObject.gameObject.SetActive(false);
                SnowObject.gameObject.SetActive(false);
                DroughtObject.gameObject.SetActive(false);
                break;
            case WeatherStates.Rain:
                rainObject.gameObject.SetActive(true);
                SnowObject.gameObject.SetActive(false);
                DroughtObject.gameObject.SetActive(false);
                break;
            case WeatherStates.Snow:
                SnowObject.gameObject.SetActive(true);
                rainObject.gameObject.SetActive(false);
                DroughtObject.gameObject.SetActive(false);
                break;
            case WeatherStates.Drought:
                SnowObject.gameObject.SetActive(false);
                rainObject.gameObject.SetActive(false);
                DroughtObject.gameObject.SetActive(true);
                break;
        }
    }

}
