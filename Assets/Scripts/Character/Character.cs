using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// i'll do this script to have a character that can die to use it if i wanted
/// but in the game to the school project cause i dont have any enemies this char
/// is never going to die, but there is the posibility :D
/// </summary>

[Serializable]
public class Stat
{
    public int maxVal;
    public int currVal;

    public Stat(int curr, int max)
    {
        maxVal = max;
        currVal = curr;
    }

    internal void subtract(int amount)
    {
        currVal -= amount;
        if(currVal < 0)
        {
            currVal = 0;
        }
    }

    internal void Add(int amount)
    {
        currVal += amount;
        if(currVal > maxVal)
        {
            currVal = maxVal;
        }
    }

    internal void SetToMax(int amount)
    {
        currVal = maxVal;
    }
    
}

public class Character : MonoBehaviour
{
    [SerializeField] StatusBar staminaBar;

    public Stat stamina;

    public bool isDead;
    public bool isExhausted;

    DisableControls disableControls;

    private void Start() {
        UpdateStaminaBar();
    }

    public void GetTired(int amount)
    {
        stamina.subtract(amount);
        if(stamina.currVal <= 0)
        {
            Exhausted();
        }
        UpdateStaminaBar();
    }

    private void Exhausted()
    {
        isExhausted = true;
        disableControls.DisableTools();
    }

    private void UpdateStaminaBar()
    {
        staminaBar.Set(stamina.currVal, stamina.maxVal);
    }

    public void Rest(int amount)
    {
        stamina.Add(amount);
        UpdateStaminaBar();
    }

    public void FullRest(int amount)
    {
        stamina.SetToMax(amount);
        UpdateStaminaBar();
    }

    private void Update() {

        if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            GetTired(10);
        }

        if(Input.GetKeyDown(KeyCode.Alpha4))
        {
            Rest(10);
        }
    }

}
