using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PokemonUI : MonoBehaviour
{

    private TMP_Text nameText;
    public Image pokemonImage;
    public Image healthBar;
    public GameObject energyBar;
    public GameObject energyPointPrefab;
    public PokemonInfo _PokemonInfo;
    
    protected virtual void Awake()
    {
        Init();
        EventSystem.Instance.AddListener<Pokemon, float>(EEvent.OnPokemonHealthChange, UpdateHealthBar);
        EventSystem.Instance.AddListener<Pokemon, int>(EEvent.OnPokemonEnergyChange, ChangeEnergy);
    }

    public virtual void Init()
    {
        nameText = GetComponentInChildren<TMP_Text>();
        nameText.text = _PokemonInfo.name;
        pokemonImage.sprite = _PokemonInfo.pokemonImage;
        
        //AddEnergy(0);
    }


    public virtual void JudgeEnergy(int cur)
    {
        
    }

    public virtual void EnableSkill() { }
    public virtual void DisableSkill() { }
    
    
    // Update is called once per frame
    public void ChangeEnergy(Pokemon p, int change)
    {
        if (p != _PokemonInfo.pokemon)
        {
            return;
        }
        if (change > 0)
        {
            AddEnergy(change);
        }
        else
        {
            RemoveEnergy(-change);
        }
    }

    public void AddEnergy(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            EnergyPlus();
        }
    }

    public void RemoveEnergy(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            EnergyMinus(i);
        }
    }

    private void EnergyPlus()
    {
        
        Instantiate(energyPointPrefab, energyBar.transform);
    }
    

    private void EnergyMinus(int i)
    {
        
        var parentTrans = energyBar.transform;
        if (parentTrans.childCount > 0)
        {
            Destroy(parentTrans.GetChild(i).gameObject);
        }
    }

    public void UpdateHealthBar(Pokemon p, float curHp)
    {
        if (p != _PokemonInfo.pokemon)
        {
            return;
        }
        healthBar.transform.localScale = new Vector3(curHp / _PokemonInfo.maxHealth, 1, 1);
    }
}
