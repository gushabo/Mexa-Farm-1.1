using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuyFences : MonoBehaviour
{
    //call the another script
    DestroyFences destroyFences;
    //call the gameObject of the panel
    [SerializeField] GameObject FencePanel;
    //we call the text of the button
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] TextMeshProUGUI titulo;
    //call the text of no money
    [SerializeField] GameObject noMoney;
    //gets the position of the fences
    Transform fences;

    //the max distance of interaction
    [SerializeField] float maxDistance = 0.5f;

    //this is the game object where we are looking for
    GameObject go;

    //this array of booleans is going to check if we had destroy the fences
    public bool[] fencesD = new bool[3];
    //the gameObject i'll use to find the fences and destroy it at the start
    GameObject find;

    //we save in this var the price
    int price;
    //get the money of the player
    Currency money;

    float i = 1.5f;
    bool nono = false;

    private void Awake()
    {
        money = GetComponent<Currency>();
    }

    private void Update()
    {
        //Activates the timer to see a mesage
        if (nono == true)
        {
            i -= Time.deltaTime;
            Debug.Log(i);
            if (i <= 0)
            {
                noMoney.SetActive(false);
                i = 1.5f;
                nono = false;
            }
        }
        //this checks the distance between the player and the fence to close the menu
        if (fences != null)
        {
            float distance = Vector2.Distance(fences.position, transform.position);
            if (distance > maxDistance)
            {
                fences.GetComponent<DestroyFences>().Close(GetComponent<Character>());
            }
        }

        //the fences that be bought in the past stay destroy
        if (fencesD[0] == true) { find = GameObject.Find("ChestFences"); Destroy(find); }
        if (fencesD[1] == true) { find = GameObject.Find("TerrainFenceslvl1"); Destroy(find); }
        if (fencesD[2] == true) { find = GameObject.Find("TerrainFenceslvl2"); Destroy(find); }

    }

    public void OpenMenu(DestroyFences destroy, Transform transform)
    {
        //sets the variable 
        this.destroyFences = destroy;
        //save the price that we are going to pay
        price = destroyFences.priceToDestroy;
        //sets the price in the panel
        text.text = price.ToString();
        //shows the panel
        FencePanel.SetActive(true);
        //get the transform of the fence
        fences = transform;
        //it checks what price it is to check what fence are u buying
        if (price == 50)
        {
            //puts the distance of interaction of the player
            maxDistance = 5f;
            //sets the text of the panel
            titulo.text = "silo purchase";
            //gets the object that is going to be destroy
            go = GameObject.Find("ChestFences");

        }
        else
        {
            if (price == 100)
            {
                //puts the distance of interaction of the player
                maxDistance = 7f;
                //sets the text of the panel
                titulo.text = "land expansion lvl1";
                //gets the object that is going to be destroy
                go = GameObject.Find("TerrainFenceslvl1");

            }
            else
            {
                if (price == 150)
                {
                    //puts the distance of interaction of the player
                    maxDistance = 7f;
                    //sets the text of the panel
                    titulo.text = "land expansion lvl2";
                    //gets the object that is going to be destroy
                    go = GameObject.Find("TerrainFenceslvl2");

                }
            }
        }
    }

    public void BuyFence()
    {
        //if u have the money buy it
        if (money.Check(price) == true)
        {
            //removes the money of the player pocket
            money.Decrease(price);
            if (price == 50) { fencesD[0] = true; }
            if (price == 100) { fencesD[1] = true; }
            if (price == 150) { fencesD[2] = true; }
            //destroys the object
            Destroy(go);
            //hides the panel
            FencePanel.SetActive(false);

        }
        else
        {
            //puts a text showing that u dont have the money
            noMoney.SetActive(true);
            //sets the timer to show the text
            nono = true;
        }

    }

    //here we close the trading panel
    public void CloseMenu()
    {
        //put invisible all the panels
        FencePanel.SetActive(false);
    }



}
