using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

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


    //for the text
    [SerializeField] GameObject panel;
    [SerializeField] GameObject text_;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] GameObject winPanel;
    [SerializeField] TextMeshProUGUI wintext;
    float timer = 4f;
    int ti;

    bool firstTime = true;
    bool end = false;

    float endTimer = 1.5f;

    //Change scenes
    [SerializeField] string FarmSceneName;
    [SerializeField] string EssentialSceneName;

    private void Start()
    {
        pause = true;
        hookProgress = 0.2f;
    }

    private void Update()
    {
        if (firstTime)
        {
            InitGame();
        }
        if (end == true)
        {
            endTimer -= Time.deltaTime;
            Debug.Log(endTimer);
            if (endTimer <= 0)
            {
                Debug.Log("cambio de escenas");
                ChangeScene();
            }
        }
        if (pause) { return; }
        firstTime = false;
        MoveFish();
        MoveHook();
        CheckProgress();

    }

    private void InitGame()
    {
        if (pause)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                timer = 0;
                pause = false;
                panel.SetActive(false);
                text_.SetActive(false);
            }
            ti = Mathf.FloorToInt(timer);
            text.text = ti.ToString();

        }


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
        wintext.text = "U WIN!!!";
        winPanel.SetActive(true);
        panel.SetActive(true);
        end = true;

    }

    private void ChangeScene()
    {
        SceneManager.LoadScene(FarmSceneName, LoadSceneMode.Single);
        SceneManager.LoadScene(EssentialSceneName, LoadSceneMode.Additive);
    }

    private void Lost()
    {
        pause = true;
        wintext.text = "U LOSE :c";
        winPanel.SetActive(true);
        panel.SetActive(true);
        end = true;
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
