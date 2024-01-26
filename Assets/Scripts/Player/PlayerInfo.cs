using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class PlayerInfo : Singleton<PlayerInfo>
{
    [SerializeField] private float health;
    [SerializeField] private Color green;
    [SerializeField] private Color red;
    [SerializeField] private Color yellow;

    private float maxHealth = 10;
    public Image healthBar;
    private float healthOriginSize;
    private void Awake()
    {
        LoadHealthBar();
        //healthRt = healthBar.GetComponent<RectTransform>();
    }

    public void ChangeHealth(int change)
    {
        health += change;
        LoadHealthBar();
    }

    private void LoadHealthBar()
    {
        float value = health / maxHealth;
        //Debug.Log(health + " " + maxHealth + " " + value);
        healthBar.rectTransform.localScale = new Vector3(value, 1, 1);
        ChangeHealthBarColor();
    }
    
    private void ChangeHealthBarColor()
    {
        if (health >= 4)
        {
            healthBar.color = green;
        }
        else if (health >= 2 )
        {
            healthBar.color = yellow;
        }
        else
        {
            healthBar.color = red;
        }
        
    }
}
