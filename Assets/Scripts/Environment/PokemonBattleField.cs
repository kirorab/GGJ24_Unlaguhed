using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PokemonBattleField : MonoBehaviour
{
    public GameObject koopa;
    private bool IsBlank = false;
    public GameObject apple;
    private void Awake()
    {
        EventSystem.Instance.AddListener<bool>(EEvent.OnEndTurtleChoose, arg0 => IsBlank = !arg0);
        EventSystem.Instance.AddListener(EEvent.OnStartPokemonBattle, HandleBattleStart);
        apple.SetActive(false);
        koopa.SetActive(false);
    }

    void HandleBattleStart()
    {
        if (IsBlank)
        {
            apple.SetActive(true);
            return;
        }
        koopa.SetActive(true);
    }
}
