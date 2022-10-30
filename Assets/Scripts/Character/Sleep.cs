using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sleep : MonoBehaviour
{
    
    DisableControls disableControls;
    Character character;
    DayTimeController dayTime;

    private void Awake() {
        disableControls = GetComponent<DisableControls>();
        character = GetComponent<Character>();
        dayTime = GameManager.instance.timeController;
    }

    internal void DoSleep()
    {
        //start the coroutine that is below 
        StartCoroutine(SleepRoutine());
    }

    //create this coroutine to give the actions of what is going to happen
    //when the player sleeps
    IEnumerator SleepRoutine()
    {
        ScreenTint screenTint = GameManager.instance.screenTint;
        //disable the controls of the character
        disableControls.DisableControl();

        //tint the scene by 2 seconds
        screenTint.Tint();
        yield return new WaitForSeconds(2f);

        //full the stamina of the character
        character.FullRest(0);

        //obviosly this make that u skip to the next morning
        dayTime.SkipToMorning();

        //Untint in 2 seconds
        screenTint.UnTint();
        yield return new WaitForSeconds(1f);
        
        //enable the controls
        disableControls.EnableControl();

        yield return null;
    }

}
