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
    public GameObject Pikachu;
    public GameObject TurtleShellPrefab;
    public GameObject Thunder;
    private GameObject TurtleShell;
    private bool isTurtleAttacking = false;
    private void Awake()
    {
        EventSystem.Instance.AddListener<bool>(EEvent.OnEndTurtleChoose, arg0 => IsBlank = !arg0);
        EventSystem.Instance.AddListener(EEvent.OnStartPokemonBattle, HandleBattleStart);
        EventSystem.Instance.AddListener(EEvent.OnEndPokemonBattle, (() => koopa.SetActive(false)));
        EventSystem.Instance.AddListener(EEvent.OnKoopaAttack, TurtleShellAttack);
        EventSystem.Instance.AddListener(EEvent.OnPikachuAttack, (() => Thunder.SetActive(true)));
        EventSystem.Instance.AddListener<int>(EEvent.OnKoopaTurnStart, ((i) => Thunder.SetActive(false)));
        apple.SetActive(false);
        koopa.SetActive(false);
        Thunder.SetActive(false);
    }

    void TurtleShellAttack()
    {
        TurtleShell = Instantiate(TurtleShellPrefab, koopa.transform.position, Quaternion.identity);
        var dir = Pikachu.transform.position - koopa.transform.position;
        isTurtleAttacking = true;
    }

    private void Update()
    {
        if (isTurtleAttacking)
        {
            TurtleShell.transform.position += TurtleShell.transform.right * 0.1f;
            if (Vector3.Distance(TurtleShell.transform.position, Pikachu.transform.position) < 0.5f)
            {
                Destroy(TurtleShell);
                isTurtleAttacking = false;
            }
        }
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
