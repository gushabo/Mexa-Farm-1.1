using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingGame : MonoBehaviour
{
    //Make the fish move
    [Header("Fishing Area")]
    [SerializeField] Transform topBound; // obviosly this are the bounds of the game
    [SerializeField] Transform botBound;

    [Header("Fish Settings")]
    [SerializeField] Transform fish; //Position of the fish
    [SerializeField] float smoothMotion = 1f;
    [SerializeField] float timerMultiplicator = 3f; // how often the fish is going to move
    float fishPosition;
    float fishTargetPosition;
    float fishSpeed;
    float fishTimer;

    [Header("Hook Settings")]
    [SerializeField] Transform hook;
    [SerializeField] float hookSize = 0.1f;
    [SerializeField] float hookPower = 0.3f;
    [SerializeField] float hookPullPower = 0.01f;
    [SerializeField] float hookGravity = .005f;
    [SerializeField] float progressBarDecay = 0.1f;
    float hookPosition;
    float hookProgress;
    float hookPullVelocity;

    [Header("Progrees Bar Settings")]
    [SerializeField] Transform progressBarContainer;

    public bool pause = false;
    [SerializeField] float failTimer = 10;
    

    private void Start() {
        hookProgress = 0.2f;
    }

    private void Update()
    {
        if(pause){return;}
        MoveFish();
        MoveHook();
        CheckProgress();
    }

    private void CheckProgress()
    {

        Vector3 ls = progressBarContainer.localScale;
        ls.y = hookProgress;
        progressBarContainer.localScale = ls; //Update the value of the scale

        float min = hookPosition - hookSize / 2;
        float max = hookPosition + hookSize / 2;

        if (min < fishPosition && fishPosition < max)
        {
            hookProgress += hookPower * Time.deltaTime;
            if (hookProgress >= 1)
            {
                //won the game
                Win();
                Debug.Log("U win");
            }
        }
        else
        {
            hookProgress -= progressBarDecay * Time.deltaTime;
            failTimer -= Time.deltaTime;
            if (failTimer < 0f)
            {   
                //lose the game
                Lost();
                Debug.Log("U lose");
            }
        }
        hookProgress = Mathf.Clamp(hookProgress, 0f, 1f);

    }

    private void Win()
    {
        pause = true;    
    }

    private void Lost()
    {
        pause = true;        
    }

    private void MoveHook()
    {
        //gets if the mouse is being dpress
        if (Input.GetMouseButton(0))
        {
            //increase the pull velocity
            hookPullVelocity += hookPullPower * Time.deltaTime;//this raises the hook
        }
        hookPullVelocity -= hookGravity * Time.deltaTime;//this pulls down the hook

        hookPosition += hookPullVelocity; //fix velocity to never get stuck at the top or at the bottom

        if (hookPosition - hookSize / 2 <= 0 && hookPullVelocity < 0)
        {
            hookPullVelocity = 0;
        }
        if (hookPosition + hookSize / 2 >= 1 && hookPullVelocity > 0)
        {
            hookPullVelocity = 0;
        }


        hookPosition = Mathf.Clamp(hookPosition, hookSize / 2, 1 - hookSize / 2);// this is to get the size correctly and dont pass the limits
        hook.position = Vector3.Lerp(botBound.position, topBound.position, hookPosition);
    }

    private void MoveFish()
    {
        fishTimer -= Time.deltaTime;
        if (fishTimer < 0f)
        {
            fishTimer = UnityEngine.Random.value * timerMultiplicator;
            fishTargetPosition = UnityEngine.Random.value;
        }

        fishPosition = Mathf.SmoothDamp(fishPosition, fishTargetPosition, ref fishSpeed, smoothMotion);
        fish.position = Vector3.Lerp(botBound.position, topBound.position, fishPosition);

    }

}
