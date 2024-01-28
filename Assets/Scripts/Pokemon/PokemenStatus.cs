using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokemenStatus : MonoBehaviour
{
    public PokemonInfo pokemonInfo;
    public int defense;
    public float health;
    public int energy;
    private PokemonUI _pokemonUI;

    private void Awake()
    {
        _pokemonUI = GetComponent<PokemonUI>();
        _pokemonUI.Init(pokemonInfo.pokemon.ToString(), pokemonInfo.pokemonImage, pokemonInfo.maxEnergy);
    }
    
    public void TakeDamage(int d){}
}
