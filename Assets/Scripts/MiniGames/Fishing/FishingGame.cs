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
    [SerializeField] Transform topBoundFish; // obviosly this are the bounds of the game
    [SerializeField] Transform botBoundFish;

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

    bool firstTime = false;
    bool end = false;

    float endTimer = 1.5f;

    //Reward of the player
    Currency money;

    //Get the character
    GameObject go;
    //know if we start the game
    bool playerStart;
    //get the camera to know the position
    GameObject cameraPos;
    //New position of the mini game
    Vector3 finalPosition;
    

    private void Start()
    {
        go = GameObject.Find("MainCharacter");
        cameraPos = GameObject.Find("Main Camera");
        if(go == null){return;}
        finalPosition = cameraPos.transform.position;
        finalPosition.z = -1.1f;
        gameObject.transform.position = finalPosition;
        money = go.GetComponent<Currency>();
        playerStart = go.GetComponent<FishGameCharacter>().startGame;
        if (playerStart)
        {
            pause = true;
            firstTime = true;
        }
        hookProgress = 0.2f;

    }

    private void Update()
    {
        //si es que es la primera vez entra aqui
        if (firstTime)
        {
            InitGame();
        }
        //si es que se acaba el juego se entra aqui
        if (end)
        {
            //bajamos el timer
            endTimer -= Time.deltaTime;
            Debug.Log(endTimer);
            //revisamos cuando sea menor que cero
            if (endTimer <= 0)
            {
                //Debug.Log("cambio de escenas");
                Debug.Log("se acabo el tiempo XD del timer");
            }
            ti = Mathf.FloorToInt(endTimer);
            text.text = ti.ToString();
        }
        //si es que no estamos en pausa y podemos jugar pasa este if
        if (pause) { return; }
        //decimos que no es la primera vez jugando
        firstTime = false;
        //en esta el pez se mueve dentro de los limites especificados
        MoveFish();
        //damos la posibilidad de poder mover el anzuelo
        MoveHook();
        //aqui revisamos si es que hemos ganado o perdido
        CheckProgress();

    }

    //inicia el juego mostrando el timer
    private void InitGame()
    {
        panel.SetActive(true);
        text_.SetActive(true);
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
            //calcula la variable del tiempo para pasarla solamente a entero
            ti = Mathf.FloorToInt(timer);
            //pone el texto de ti en el texto 
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
        //wintext.text = "U WIN!!!";
        //winPanel.SetActive(true);
        //panel.SetActive(true);
        //end = true;

    }

    private void Lost()
    {
        pause = true;
        //wintext.text = "U LOSE :c";
        //winPanel.SetActive(true);
        //panel.SetActive(true);
        //end = true;
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
        fish.position = Vector3.Lerp(botBoundFish.position, topBoundFish.position, fishPosition);

    }

}
