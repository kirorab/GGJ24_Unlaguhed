using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class PlayerInfo : Singleton<PlayerInfo>
{
    public float health;
    public float maxHealth = 10;
    private float healthOriginSize;

    private void Awake()
    {
        health = maxHealth;
    }

    public void ChangeHealth(int change)
    {
        health += change;
        EventSystem.Instance.Invoke(EEvent.OnPlayerHealthChange);
    }

    
}
