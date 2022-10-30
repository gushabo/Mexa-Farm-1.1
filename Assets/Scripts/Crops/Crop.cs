using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName ="Data/Crop")]

public class Crop : ScriptableObject
{
    /*
    whenever i need a thing that know time from the world not only a certain item 
    it we called an agent, that's why we have the "time agent"
    */

    //this will helps us to know how the crops will grow

    public int timeToGrow = 2;
    public Item yield;
    public int count = 1;

    public List<Sprite> sprites;
    public List<int> growthStageTime;

    public int MaxWater = 5;
    public int daysWithOutWater = 0;

}
