using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterFishingGame : MonoBehaviour
{
    [SerializeField] GameObject pj;
    public Vector3 position;
    public GameObject go;

    private void Awake()
    {
        getPosition();
        SearchGame();
    }

    private void getPosition()
    {
        position = pj.GetComponent<Transform>().position;
    }

    private void Update()
    {
        getPosition();
        if (go != null)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                EnterGame();
            }
        }
    }

    private void EnterGame()
    {
        go.SetActive(true);
    }

    private void SearchGame()
    {
        go = GameObject.Find("FishingMiniGame");
        if (go == null)
        {
            Debug.Log("no se encontro el minijuego :c");
            return;
        }
        go.transform.position = position;

    }

}
