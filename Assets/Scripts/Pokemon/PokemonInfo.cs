using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PokemonInfo", menuName = "PokemonInfo", order = 1)]
public class PokemonInfo : ScriptableObject
{
    public Pokemon pokemon;
    
    public int maxDefense;
    public float maxHealth;
    public int maxEnergy;
    public Sprite pokemonImage;
}
