using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;
using System;

public class CrowActions : MonoBehaviour
{

    public static CrowActions instance;

    public float prob = 0.02f;
    public int days = 10;
    float rand;
    public bool validation;

    private void Awake()
    {
        if (instance == null)
        {
            CrowActions.instance = this;
        }
        else { Destroy(gameObject); }
    }

    private void Update()
    {
        if (DayTimeController.days > days)
        {
            rand = UnityEngine.Random.value;
            //this put if the crow is going to activate or not
            if (rand <= prob)
            {
                //puts the bool in true so we know it pass the chances
                validation = true;
            }
            else
            {
                validation = false;
            }
            days = DayTimeController.days;
        }
    }

}
