using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePokemon : MonoBehaviour
{
    public PokemonInfo pokemonInfo;
    public int defense;
    public float health;
    public int energy;

    public int NextAttackBonus = 1;
    public int NextDefenseBonus = 0;
    public bool IsStunned = false;

    public virtual void Awake()
    {
        defense = pokemonInfo.maxDefense;
        health = pokemonInfo.maxHealth;
        energy = 0;
    }

    public void TakeDamage(int d)
    {
        health -= Math.Max(0, d - defense);
        if (health <= 0)
        {
            health = 0;
            isDead();
        }
        EventSystem.Instance.Invoke<Pokemon, float>(EEvent.OnPokemonHealthChange, pokemonInfo.pokemon, health);
    }

    public void Heal(int h)
    {
        health = Math.Min(pokemonInfo.maxHealth, health + h);
        EventSystem.Instance.Invoke<Pokemon, float>(EEvent.OnPokemonHealthChange, pokemonInfo.pokemon, health);
    }

    public void UseEnergy(int i)
    {
        energy -= i;
        EventSystem.Instance.Invoke<Pokemon, int>(EEvent.OnPokemonEnergyChange, pokemonInfo.pokemon, -i);
    }

    public void RestoreEnergy(int i)
    {
        int restore = Math.Min(i, pokemonInfo.maxEnergy - energy);
        energy += restore;
        EventSystem.Instance.Invoke<Pokemon, int>(EEvent.OnPokemonEnergyChange, pokemonInfo.pokemon, restore);

    }

    public bool isDead()
    {
        return health <= 0;
    }
}
