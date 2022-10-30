using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Currency : MonoBehaviour
{
    [SerializeField] int amount;
    [SerializeField] TextMeshProUGUI text;

    private void Start() {
        UpdateText();
    }

    private void UpdateText()
    {
       text.text = amount.ToString();
    }

    public void Add(int moneyGain)
    {
        amount += moneyGain;
        if(amount >= 9999){amount = 9999;}
        UpdateText();
    }

    internal bool Check(int totalPrice)
    {
        return amount >= totalPrice;
    }

    internal void Decrease(int totalPrice)
    {
        amount-= totalPrice;
        if(amount < 0){amount = 0;}
        UpdateText();
    }
}
