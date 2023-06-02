using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Currency : MonoBehaviour
{
    [SerializeField] int amount;
    [SerializeField] TMPro.TextMeshProUGUI text;

    private void Start()
    {
        amount = 1000;
        UpdateText();
    }

    private void UpdateText()
    {
        text.text = amount.ToString();
    }

    public void Add(int moneyGain)
    {
        amount += moneyGain;
        UpdateText();
    }

    internal bool Check(int totalPrice)
    {
        return amount >= totalPrice;
    }

    internal void Decreace(int totalPrice)
    {
        amount -= totalPrice;
        if (amount < 0)
            amount = 0;
        UpdateText();
    }
}
