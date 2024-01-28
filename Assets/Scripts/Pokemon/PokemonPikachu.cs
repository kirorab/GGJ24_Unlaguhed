using UnityEngine;


public class PokemonPikachu : BasePokemon
{
    public override void Awake()
    {
        base.Awake();
    }
    

    public bool Charge()
    {
        if (energy < 1)
        {
            return false;
        }
        UseEnergy(1);
        NextAttackBonus = 2; // 下一次攻击伤害翻倍
        return true;
    }

    public void NormalAttack(BasePokemon target)
    {
        target.TakeDamage(4 * NextAttackBonus);
        NextAttackBonus = 1; // 重置攻击加成
        RestoreEnergy(1);
    }
    
    public bool Thunderbolt(BasePokemon target)
    {
        if (energy >= 1)
        {
            target.TakeDamage(6);
            UseEnergy(1);
            return true;
        }

        return false;
    }
}