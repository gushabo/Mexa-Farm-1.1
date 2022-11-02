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

    float timer = 1.5f;
    bool flag = false;

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
    List<int> x = new List<int>();
    int random;
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
            //flag = true;
        }

        if (flag == true)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                RainHappens();
                SnowHappens();
                flag = false;
                timer = 1.5f;
            }

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
                    if (itemContainer.slots[i].item != null)
                    {
                        x.Add(i);
                    }
                    if(x.Count == 0){return;}
                }
                random = UnityEngine.Random.Range(0, x.Count - 1);
                removeItem = itemContainer.slots[random].item;
                itemContainer.Remove(removeItem);
                x.RemoveAt(random);
            }

        }
    }

    private void RainHappens()
    {
        if (currentWeatherState == WeatherStates.Rain)
        {
            foreach (CropTile cropTile in cropsW.crops)
            {
                cropTile.CurrWater++;
            }

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
