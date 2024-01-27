using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private Color green = new Color32(23, 199, 0 , 255);
    [SerializeField] private Color red = new Color32(178, 7, 7, 255);
    [SerializeField] private Color yellow = new Color32(233, 242, 0, 255);
    
    
    public Image healthBar;

    private void Awake()
    {
        LoadHealthBar();
        EventSystem.Instance.AddListener(EEvent.OnPlayerHealthChange, LoadHealthBar);
    }

    private void LoadHealthBar()
    {
        float value = PlayerInfo.Instance.health / PlayerInfo.Instance.maxHealth;
        //Debug.Log(PlayerInfo.Instance.health + " " + PlayerInfo.Instance.maxHealth + " " + value);
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
