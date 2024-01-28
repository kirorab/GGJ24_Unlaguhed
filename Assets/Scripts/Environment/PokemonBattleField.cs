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
    public GameObject TurtleShellPrefabSmall;
    public GameObject TurtleShellPrefabBig;
    public GameObject TurtleDefense;
    public GameObject PikachuCharge;
    public GameObject Thunder;
    public GameObject ThunderBallPrefab;
    private GameObject ThunderBall;
    private GameObject TurtleShell;
    private bool isTurtleAttacking = false;
    private bool isThunderAttacking = false;

    public float AttackSpeed = 0.2f;
    
    private void Awake()
    {
        EventSystem.Instance.AddListener<bool>(EEvent.OnEndTurtleChoose, arg0 => IsBlank = !arg0);
        EventSystem.Instance.AddListener(EEvent.OnStartPokemonBattle, HandleBattleStart);
        EventSystem.Instance.AddListener(EEvent.OnEndPokemonBattle, (() => koopa.SetActive(false)));
        EventSystem.Instance.AddListener<int>(EEvent.OnKoopaUseSkill, HandleTurtleSkill);
        EventSystem.Instance.AddListener<int>(EEvent.OnPikaUseSkill, HandlePikachuUseSkill);
        EventSystem.Instance.AddListener<int>(EEvent.OnKoopaTurnStart, ((i) => Thunder.SetActive(false)));
        EventSystem.Instance.AddListener(EEvent.OnKoopaDefenseEnd, (() =>
        {
            Debug.Log("turtle defense end");
            TurtleDefense.SetActive(false);
        }));
        EventSystem.Instance.AddListener(EEvent.OnPikaChargeEnd, (() => PikachuCharge.SetActive(false)));
        EventSystem.Instance.AddListener(EEvent.OnKoopaDead, () => apple.SetActive(true));
        apple.SetActive(false);
        koopa.SetActive(false);
        Thunder.SetActive(false);
        TurtleDefense.SetActive(false);
        PikachuCharge.SetActive(false);
    }

    void HandleTurtleSkill(int i)
    {
        switch (i)
        {
            // normal attack
            case (int)Koopa.EKoopaSkill.NormalAttack:
                TurtleShell = Instantiate(TurtleShellPrefabSmall, koopa.transform.position, Quaternion.identity);
                isTurtleAttacking = true;
                break;
            case (int)Koopa.EKoopaSkill.ShellDefense:
                TurtleDefense.SetActive(true);
                break;
            case (int)Koopa.EKoopaSkill.ShellRush:
                TurtleShell = Instantiate(TurtleShellPrefabBig, koopa.transform.position, Quaternion.identity);
                isTurtleAttacking = true;
                break;
        }
        
    }
    
    void HandlePikachuUseSkill(int i)
    {
        switch (i)
        {
            case (int)PokemonPikachu.EPikachuSkill.NormalAttack:
                Thunder.SetActive(true);
                break;
            case (int)PokemonPikachu.EPikachuSkill.Charge:
                PikachuCharge.SetActive(true);
                break;
            case (int)PokemonPikachu.EPikachuSkill.Thunderbolt:
                ThunderBall = Instantiate(ThunderBallPrefab, Pikachu.transform.position, Quaternion.identity);
                isThunderAttacking = true;
                break;
        }
    }

    private void Update()
    {
        if (isTurtleAttacking)
        {
            TurtleShell.transform.position += TurtleShell.transform.right * AttackSpeed;
            if (Vector3.Distance(TurtleShell.transform.position, Pikachu.transform.position) < 0.5f)
            {
                Destroy(TurtleShell);
                isTurtleAttacking = false;
            }
        }

        if (isThunderAttacking)
        {
            ThunderBall.transform.position -= ThunderBall.transform.right * AttackSpeed;
            if (Vector3.Distance(ThunderBall.transform.position, koopa.transform.position) < 0.5f)
            {
                Destroy(ThunderBall);
                isThunderAttacking = false;
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
