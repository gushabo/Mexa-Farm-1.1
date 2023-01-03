using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MailBox : MonoBehaviour
{
    //get the panel of the mail box
    [SerializeField] GameObject MailBoxPanel;
    //this is the price
    [SerializeField] int price = 50;
    int actualprice;
    //gets the position of the mailbox
    Transform mail;

    //the max distance of interaction
    [SerializeField] float maxDistance = 0.5f;
    //this gets the info of the text in the panel
    [SerializeField] TextMeshProUGUI text;
    //this gets a text that is shown when u dont have the money to pay
    [SerializeField] GameObject noMoney;
    //gets the money of the player
    Currency money;

    [SerializeField] GameObject gameOverPanel;

    //to check if the user have the money
    bool nono = false;
    //timer of the message
    float i = 1.5f;

    //number of days
    int days = 0;
    int daysPass = 0;
    [SerializeField] TextMeshProUGUI daystext;

    //the script that help us to disable the control of the player
    DisableControls disableControls;

    private void Awake()
    {
        money = GetComponent<Currency>();
        disableControls = GetComponent<DisableControls>();
    }

    private void Update()
    {

        //sumar los dias pasados
        if (days < DayTimeController.days) { days = DayTimeController.days; daysPass++; }
        //revisar si la var de no tener dinero se activa para tener el contador
        if (nono == true)
        {
            i -= Time.deltaTime;
            if (i <= 0)
            {
                noMoney.SetActive(false);
                i = 1.5f;
                nono = false;
            }
        }

        //esto es para revisar la distancia entre jugador y objeto para cerrar el menu
        if (mail != null)
        {
            float distance = Vector2.Distance(mail.position, transform.position);
            if (distance > maxDistance)
            {
                mail.GetComponent<MailBoxPay>().Close(GetComponent<Character>());
            }
        }

        //this is for the price of the mailBox 
        if (daysPass <= 4)
        { actualprice = price; }

        if (daysPass > 4)
        { actualprice = price * 2; }

        if (daysPass > 9)
        {
            GameOver();
        }

    }

    public void OpenBox(Transform transform)
    {
        MailBoxPanel.SetActive(true);
        mail = transform;
        text.text = actualprice.ToString();
        daystext.text = "Days past: " + (daysPass + 1).ToString();

    }

    public void PayMailBox()
    {
        if (money.Check(price) == true)
        {
            //actions that happens when u pay XD
            money.Decrease(price);
            MailBoxPanel.SetActive(false);
            daysPass = 0;
        }
        else
        {
            noMoney.SetActive(true);
            nono = true;
        }
    }

    //here we close the mailBox panel
    public void CloseMenu()
    {
        //put invisible all the panels
        MailBoxPanel.SetActive(false);
    }

    public void GameOver()
    {
        //disable all the controls of the player
        disableControls.DisableControl();
        gameOverPanel.SetActive(true);
    }


}
