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
