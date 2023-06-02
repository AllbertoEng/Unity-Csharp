using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Stat
{
    public int maxVal;
    public int currVal;

    public Stat(int curr, int max)
    {
        this.maxVal = max;
        this.currVal = curr;
    }

    internal void Subtract(int amount)
    {
        currVal -= amount;
    }

    internal void Add(int amount)
    {
        currVal += amount;
        if (currVal > maxVal)
            currVal = maxVal;
    }

    internal void SetToMax()
    {
        currVal = maxVal;
    }
}

public class Character : MonoBehaviour, IDamageable
{
    public Stat hp;
    public Stat stamina;
    [SerializeField] StatusBar hpBar;
    [SerializeField] StatusBar staminaBar;

    public bool isDead;
    public bool isExhausted;

    DisableControl disableControl;
    PlayerRespawn playerRespawn;

    private void Awake()
    {
        disableControl = GetComponent<DisableControl>();
        playerRespawn = GetComponent<PlayerRespawn>();
    }
    public void Start()
    {
        UpdateHPBar();
        UpdateStaminaBar();
    }

    private void UpdateStaminaBar()
    {
        staminaBar.Set(stamina.currVal, stamina.maxVal);
    }

    public void TakeDamage(int amount)
    {
        if (isDead)
            return;

        hp.Subtract(amount);
        if (hp.currVal <= 0)
            Dead();

        UpdateHPBar();
    }

    private void Dead()
    {
        isDead = true;
        disableControl.DisableControls();
        playerRespawn.StartRespawn();
    }

    private void UpdateHPBar()
    {
        hpBar.Set(hp.currVal, hp.maxVal);
    }

    public void Heal(int amount)
    {
        hp.Add(amount);
        UpdateHPBar();
    }

    public void FullHeal()
    {
        hp.SetToMax();
        UpdateHPBar();
    }

    public void GetTired(int amount)
    {
        if (isExhausted)
            return;

        stamina.Subtract(amount);
        if (stamina.currVal <= 0)
            Exhausted();

        UpdateStaminaBar();

    }

    private void Exhausted()
    {
        isExhausted = true;
        disableControl.DisableControls();
        playerRespawn.StartRespawn();
    }

    public void Rest(int amount)
    {
        stamina.Add(amount);
        UpdateStaminaBar();
    }

    public void FullRest()
    {
        stamina.SetToMax();
        UpdateStaminaBar();
    }

    public void CalculateDanage(ref int damage)
    {
        
    }

    public void ApplyDamage(int damage)
    {
        TakeDamage(damage);
    }

    public void CheckState()
    {
        
    }
}
