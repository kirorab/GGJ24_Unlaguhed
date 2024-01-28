using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PokemonUI : MonoBehaviour
{

    private TMP_Text name;
    public Image pokemonImage;
    public Image healthBar;
    public GameObject energyBar;
    public GameObject energyPointPrefab;
    
    

    public void Init(String nameText, Sprite s, int maxEnergy)
    {
        name = GetComponentInChildren<TMP_Text>();
        name.text = nameText;
        pokemonImage.sprite = s;
        
        AddEnergy(maxEnergy);
    }

    
    // Update is called once per frame
    

    public void AddEnergy(int mount)
    {
        for (int i = 0; i < mount; i++)
        {
            EnergyPlus();
        }
    }

    public void RemoveEnergy(int mount)
    {
        for (int i = 0; i < mount; i++)
        {
            EnergyMinus();
        }
    }

    private void EnergyPlus()
    {
        
        Instantiate(energyPointPrefab, energyBar.transform);
    }
    

    private void EnergyMinus()
    {
        
        var parentTrans = energyBar.transform;
        Destroy(parentTrans.GetChild(parentTrans.childCount - 1).gameObject);
    }

    public void UpdateHealthBar(float curHp, float maxHp)
    {
        healthBar.transform.localScale = new Vector3(curHp / maxHp, 1, 1);
    }
}
