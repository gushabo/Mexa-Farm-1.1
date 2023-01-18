using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MailBoxPay : Interactable
{

    [SerializeField] bool open;
    [SerializeField] GameObject sign;

    public int dayPass = 0;
    public int dayInMail = 0;

    public override void Interact(Character character)
    {
        if (open == false)
        {
            Open(character);
        }
        else
        {
            Close(character);
        }
    }

    public void Open(Character character)
    {
        MailBox mailBox = character.GetComponent<MailBox>();
        if (mailBox == null) {return;}
        mailBox.OpenBox(transform);
    }

    public void Close(Character character)
    {
        MailBox mailBox = character.GetComponent<MailBox>();
        if (mailBox == null) {return;}
        mailBox.CloseMenu();
    }

    private void Update() {

        if(DayTimeController.days > dayPass)
        {
            dayPass = DayTimeController.days;
            dayInMail++;
        }

        if(dayInMail < 2){
            sign.GetComponent<SpriteRenderer>().color = Color.green;   
        }
        if(dayInMail < 4 && dayInMail > 1){
            sign.GetComponent<SpriteRenderer>().color = Color.yellow;
        }
        if(dayInMail >= 4){
            sign.GetComponent<SpriteRenderer>().color = Color.red;   
        }

    }



}
