using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CharacterMovement : MonoBehaviour
{
    //the text to put the points
    [SerializeField] TextMeshProUGUI text;

    [SerializeField] float speed = 2f;
    public float points = 0f;
    Rigidbody2D rgbd;
    Vector2 motionVector;

    //get the currency of the player
    GameObject go;
    Currency money;

    //properties that activate when u win or lose
    [SerializeField] GameObject WinPanel;
    [SerializeField] GameObject coinPanel;
    [SerializeField] TextMeshProUGUI winText;
    float timer = 1.3f;
    bool end = false;

    void Start()
    {
        rgbd = GetComponent<Rigidbody2D>();
        go = GameObject.Find("MainCharacter");
        money = go.GetComponent<Currency>();
        points = 0;
    }

    void Update()
    {
        SetFixedPoints();
        float Horizontal = Input.GetAxisRaw("Horizontal");
        motionVector = new Vector2(Horizontal, 0);
        if (end)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                WinPanel.SetActive(false);
                coinPanel.SetActive(false);
                go.GetComponent<UsingArcade>().FinishGame(true);
                SceneManager.UnloadScene("CatchFruitMiniGame");

            }
        }

    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        rgbd.MovePosition(rgbd.position + motionVector * speed * Time.fixedDeltaTime);
    }

    public void SetFixedPoints()
    {
        text.text = points.ToString() + " / 20";
    }

    public void AddPoints()
    {
        points = points + 3;
    }

    public void doYouWin()
    {
        if (points >= 20)
        {
            end = true;
            WinPanel.SetActive(true);
            winText.text = "YOU WIN!!!";
            coinPanel.SetActive(true);
            go.GetComponent<Currency>().Add(3);
        }
        else
        {
            end = true;
            WinPanel.SetActive(true);
            winText.text = "YOU LOSE :c";
            coinPanel.SetActive(false);
        }
    }


}
