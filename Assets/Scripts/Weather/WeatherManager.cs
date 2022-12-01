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

    bool weatherFlag = false;

    public static WeatherManager instance;
    public bool snow;
    public bool rain;
    public bool drought;

    private void Awake()
    {
        if (instance == null)
        {
            WeatherManager.instance = this;
        }
        else { Destroy(gameObject); }
    }

    IEnumerator esperaDeTiempo()
    {
        weatherFlag = true;
        yield return new WaitForSeconds(0.5f);
    }

    private void Update()
    {

        if (days < DayTimeController.days)
        {
            days = DayTimeController.days;
            StartCoroutine(esperaDeTiempo());
        }

        if (weatherFlag)
        {
            weatherFlag = false;
            RandomWeatherChangeCheck();
            flag = true;
        }

        if (flag == true)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                RainHappens();
                SnowHappens();
                DroughtHappens();
                flag = false;
                timer = 1.5f;
            }

        }

    }

    private void SnowHappens()
    {
        if (currentWeatherState == WeatherStates.Snow)
        {
            snow = true;
        }
    }

    private void RainHappens()
    {
        if (currentWeatherState == WeatherStates.Rain)
        {
            rain = true;
        }
    }

    private void DroughtHappens()
    {
        if(currentWeatherState == WeatherStates.Drought)
        {
            drought = true;
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
