using System.Collections;
using UnityEngine;

public class BattleController : MonoBehaviour
{
    private PokemonPikachu pikachu;
    private Koopa marioTurtle;
    private bool pikachuTurn = false; // 初始玩家为先手
    private int turn = 0;
    private bool canEndTurn = false;
    void Start()
    {
        pikachu = GetComponent<PokemonPikachu>();
        marioTurtle = GetComponent<Koopa>();
        EventSystem.Instance.AddListener(EEvent.OnAppleHitPikachu, () => pikachu.TakeDamage(999));
        // 开始战斗
        //EventSystem.Instance.Invoke(EEvent.OnStartPokemonBattle);
        StartCoroutine(BattleRoutine());
    }

    IEnumerator BattleRoutine()
    {
        while (pikachu.health > 0 && marioTurtle.health > 0)
        {
            if (pikachuTurn)
            {
                ExecutePikachuTurn();
            }
            else
            {
                ExecuteMarioTurtleTurn();
                canEndTurn = false; // 重置回合结束标志
                yield return new WaitUntil(() => canEndTurn); 
            }

            pikachuTurn = !pikachuTurn; // 切换回合
            yield return new WaitForSeconds(1f); // 暂停一秒，模拟战斗延时
        }

        // 战斗结束逻辑
        if (pikachu.isDead())
        {
            EventSystem.Instance.Invoke(EEvent.OnEndPokemonBattle);
        }
        else
        {
            Debug.Log("Pikachu Wins!");
        }
    }

    public void UseTurtleSkill(int index)
    {
        switch (index)
        {
            case 0:
                marioTurtle.NormalAttack(pikachu);
                break;
            case 1:
                marioTurtle.ShellDefense();
                break;
            case 2:
                marioTurtle.ShellRush(pikachu);
                break;
        }
        canEndTurn = true;
    }
    
    void ExecutePikachuTurn()
    {
        EventSystem.Instance.Invoke(EEvent.OnPikachuTurnStart);
        // 这里根据皮卡丘的攻击顺序实现具体的行动逻辑
        // 例如：普攻，普攻，蓄力，十万伏特，普攻，蓄力，普攻
        // 可以用一个额外的变量来追踪当前的行动是哪一个
        //皮卡丘攻击顺序：普攻，普攻，蓄力，十万伏特，普攻，蓄力，普攻。按上列顺序循环
        switch (turn)
        {
            case 0:
                pikachu.NormalAttack(marioTurtle);
                turn++;
                break;
            case 1:
                pikachu.NormalAttack(marioTurtle);
                turn++;
                break;
            case 2:
                pikachu.Charge();
                turn++;
                break;
            case 3:
                pikachu.Thunderbolt(marioTurtle);
                turn++;
                break;
            case 4:
                pikachu.NormalAttack(marioTurtle);
                turn++;
                break;
            case 5:
                pikachu.Charge();
                turn++;
                break;
            case 6:
                pikachu.NormalAttack(marioTurtle);
                turn = 0;
                break;
        }
    }

    
    
    void ExecuteMarioTurtleTurn()
    {
        EventSystem.Instance.Invoke<int>(EEvent.OnKoopaTurnStart, marioTurtle.energy);
    }
}