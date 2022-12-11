using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class DayTimeController : MonoBehaviour
{

    const float secondsInDay = 86400; // this are the seconds on one day
    /*
    if we check every time the spawn in the "trees" this was like a practic to know how to use the time manager
    i'm using the next lines to not check every frame insted i make like a time space when the game is going to
    */

    const float phaseLenght = 900f; //15 min in seconds
    const float phasesInDay = 96f; //Seconds divided by phaseLenght

    [SerializeField] Color NightLightColor;
    [SerializeField] AnimationCurve NightTimeCurve;
    [SerializeField] Color DayLightColor = Color.white;
    public static float time;
    [SerializeField] public Text text;
    [SerializeField] float timeScale = 300f; //timeScale = 300f will make that the day last 5 minutes
    [SerializeField] float startAtTime = 28800f; // this is 8 o'clock in the morning in seconds = 28800f

    [SerializeField] float morningTime = 28800f; //the time that the player is going to wake up

    [SerializeField] Light2D globalLight; //86,400 -14,400
    public static int days;

    public bool cuentaTiempo;

    List<TimeAgent> agents;

    private CortinillaDormir cortinilla;

    private void Awake()
    {
        agents = new List<TimeAgent>();
    }

    private void Start()
    {
        time = startAtTime;
        cortinilla = GameManager.instance.player.GetComponent<Sleep>().cortinilla;
        cuentaTiempo = true;
    }

    public void AddTime(TimeAgent timeAgent)
    {
        agents.Add(timeAgent);
    }

    public void RestTime(TimeAgent timeAgent)
    {
        agents.Remove(timeAgent);
    }

    float Hours
    {
        get { return time / 3600f; }
    }

    float Minutes
    {
        get { return time % 3600f / 60f; }
    }

    private void Update()
    {

        if(cuentaTiempo)
        {
            time += Time.deltaTime * timeScale;
        }

        TimeValueCalcs();
        PutDayLight();

        if (time > secondsInDay)
        {
            cortinilla.aparecerMensaje();
            time = morningTime;
            days++;
            foreach (var item in GameManager.instance.listaCorralMenu)
            {
                item.GenerarProducto();
            }
        }

        TimeAgents();

        if(Input.GetKeyDown(KeyCode.T))
        {
            SkipTime(hours: 4);
        }
        

    }

    private void TimeValueCalcs() // we use this to pass the seconds to minutes and hours
    {
        int hh = (int)Hours;
        int mm = (int)Minutes;
        text.text = hh.ToString("00") + ":" + mm.ToString("00");
    }

    private void PutDayLight() //this checks the time and put some shadows or get the light to make the day n night cycle
    {
        float v = NightTimeCurve.Evaluate(Hours);
        Color c = Color.Lerp(DayLightColor, NightLightColor, v);
        globalLight.color = c;
    }

    int oldPhase = -1; //this is cause we are checking if we have a different time
    private void TimeAgents()// this checks every time if something has to be check by the time to add o substract time
    {
        if(oldPhase == -1)
        {
            oldPhase = CalculatePhase();
        }

        int currentPhase = CalculatePhase();

        while(oldPhase < currentPhase)
        {
            oldPhase += 1;
            for (int i = 0; i < agents.Count; i++)//here is were we add or substract time through the Invoke
            {
                agents[i].Invoke();
            }
        }
        
    }

    private int CalculatePhase()
    {
        return (int)(time / phaseLenght) + (int)(days * phasesInDay);
    }

    public void NextDay()
    {
        time -= secondsInDay;
        days += 1;
    }

    public void SkipTime(float seconds = 0, float minutes = 0, float hours = 0)
    {
        float timetoSkip = seconds;
        timetoSkip += minutes * 60f;
        timetoSkip += hours * 3600f;

        time += timetoSkip;
    }

    internal void SkipToMorning()
    {
        float secondsToSkip = 0f;
        
        if(time > morningTime)
        {
            //this is when u have to sleep in the next day time
            secondsToSkip += secondsInDay - time + morningTime;
        }else{
            //this is when u sleep and wake up in the same day
            secondsToSkip += morningTime - time;
        }

        SkipTime(secondsToSkip);
    }


}
