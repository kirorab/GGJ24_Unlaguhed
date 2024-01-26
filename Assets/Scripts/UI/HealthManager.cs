using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : Singleton<HealthManager>
{
    [SerializeField] private Color green;
    [SerializeField] private Color red;
    [SerializeField] private Color yellow;
    
    
    public Image healthBar;

    private void Start()
    {
        LoadHealthBar();
        EventSystem.Instance.AddListener(EEvent.OnPlayerHealthChange, LoadHealthBar);
    }

    private void LoadHealthBar()
    {
        float value = PlayerInfo.Instance.health / PlayerInfo.Instance.maxHealth;
        Debug.Log(PlayerInfo.Instance.health + " " + PlayerInfo.Instance.maxHealth + " " + value);
        healthBar.rectTransform.localScale = new Vector3(value, 1, 1);
        ChangeHealthBarColor(PlayerInfo.Instance.health);
    }
    
    private void ChangeHealthBarColor(float health)
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
