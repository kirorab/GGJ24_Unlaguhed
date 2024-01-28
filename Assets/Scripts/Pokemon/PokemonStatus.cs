using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokemonStatus : MonoBehaviour
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

    public void TakeDamage(int d)
    {
        health -= Math.Max(0, d - defense);
        _pokemonUI.UpdateHealthBar(health, pokemonInfo.maxHealth);
        if (health <= 0)
        {
            Dead();
        }
    }

    public void Heal(int h)
    {
        health = Math.Min(pokemonInfo.maxHealth, health + h);
        _pokemonUI.UpdateHealthBar(health, pokemonInfo.maxHealth);
    }

    public void UseEnergy(int i)
    {
        energy -= i;
        _pokemonUI.RemoveEnergy(i);
    }

    public void RestoreEnergy(int i)
    {
        int restore = Math.Min(i, pokemonInfo.maxEnergy - energy);
        energy += restore;
        _pokemonUI.AddEnergy(restore);
    }
    
    private void Dead()
    {
        throw new NotImplementedException();
    }
}
