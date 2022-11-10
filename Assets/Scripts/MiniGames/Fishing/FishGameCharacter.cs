using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FishGameCharacter : MonoBehaviour
{

    //all the prefab of the minigame
    // public GameObject miniGame;
    // public GameObject grandson;

    // [SerializeField] GameObject cam;

    // Vector3 pos;

    // //for the text
    // [SerializeField] GameObject panel;
    // [SerializeField] GameObject text_;
    // [SerializeField] TextMeshProUGUI text;
    // [SerializeField] GameObject winPanel;
    // [SerializeField] TextMeshProUGUI wintext;
    // float timer = 4f;
    // int ti;

    // bool firstTime = true;
    // bool end = false;
    // bool enterGame = false;

    // float endTimer = 1.5f;
    // public bool pause = false;

    DisableControls disable;
    public bool startGame = false;
    [SerializeField] string FishScene;

    void Start()
    {
        disable = GetComponent<DisableControls>();
        //pause = true;
        //miniGame = GameObject.Find("FishingGameAppearing");
        //grandson = miniGame.GetComponent<FishGamePart1>().grandson;
    }

    /*public void Update()
    {

        if (firstTime && enterGame)
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

            }
        }
        if (pause) { return; }
        firstTime = false;
        pos.x = cam.transform.position.x;
        pos.y = cam.transform.position.y;
        pos.z = grandson.transform.position.z;
        grandson.transform.position = pos;
        grandson.SetActive(true);
    }*/

    public void StartGame()
    {
        disable.DisableControl();
        startGame = true;
        SceneManager.LoadScene(FishScene, LoadSceneMode.Additive);
        //enterGame = true;
        //panel.SetActive(true);
        //text_.SetActive(true);
    }

    /*private void InitGame()
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
    }*/

}
