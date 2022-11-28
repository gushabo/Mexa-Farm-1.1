using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MailBoxPay : Interactable
{

    [SerializeField] bool open;
    [SerializeField] GameObject sign;

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
        if(DayTimeController.days < 2){
            sign.GetComponent<SpriteRenderer>().color = Color.green;   
        }
        if(DayTimeController.days < 4 && DayTimeController.days > 1){
            sign.GetComponent<SpriteRenderer>().color = Color.yellow;   
        }
        if(DayTimeController.days >= 4){
            sign.GetComponent<SpriteRenderer>().color = Color.red;   
        }

    }



}
