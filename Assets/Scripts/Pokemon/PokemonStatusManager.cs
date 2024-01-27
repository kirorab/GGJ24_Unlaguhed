using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PokemonStatusManager : MonoBehaviour
{
    public PokemonInfo pokemonInfo;

    private TMP_Text name;
    public Image pokemonImage;
    public Image healthBar;
    public GameObject energyBar;
    public int defense;
    public float health;
    public int energy;
    // Start is called before the first frame update
    private void Awake()
    {
        name = GetComponentInChildren<TMP_Text>();
        name.text = pokemonInfo.pokemon.ToString();
        pokemonImage.sprite = pokemonInfo.pokemonImage;
        defense = pokemonInfo.maxDefense;
        health = pokemonInfo.maxHealth;
        energy = pokemonInfo.maxEnergy;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
