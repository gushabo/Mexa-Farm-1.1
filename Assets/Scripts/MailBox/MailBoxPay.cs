using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MailBoxPay : Interactable
{

    [SerializeField] bool open;

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

}
