using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStorePanel : ItemPanel
{
    //goes for the script of trading
    [SerializeField] Trading trading;

    //checks for the click
    public override void OnClick(int id)
    {
        //trading proccess
        // if the player is dragging any item that is going to get selled if not is going to buy
        /*if(GameManager.instance.dragNDropController.itemSlot.item == null)
        {
            BuyItem(id);
        }else{
           SellItem(id); 
        }*/
        
        Show();
    }

    private void BuyItem(int id)
    {
        trading.BuyItem(id);
    }

    //this is like an override for the SellItem function in trading
    private void SellItem(int id)
    {
        trading.SellItem(id);
    }

}
