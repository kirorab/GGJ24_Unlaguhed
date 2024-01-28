using UnityEngine;


public class PokemonPikachu : BasePokemon
{
    public override void Awake()
    {
        base.Awake();
    }
    
    public enum EPikachuSkill
    {
        NormalAttack,
        Charge,
        Thunderbolt
    }
    
    public void NormalAttack(BasePokemon target)
    {
        target.TakeDamage(4 * NextAttackBonus);
        NextAttackBonus = 1; // 重置攻击加成
        EventSystem.Instance.Invoke(EEvent.OnPikaChargeEnd);
        RestoreEnergy(1);
        EventSystem.Instance.Invoke<int>(EEvent.OnPikaUseSkill, (int)EPikachuSkill.NormalAttack);
    }

    public bool Charge()
    {
        if (energy < 1)
        {
            return false;
        }
        UseEnergy(1);
        NextAttackBonus = 2; // 下一次攻击伤害翻倍
        EventSystem.Instance.Invoke<int>(EEvent.OnPikaUseSkill, (int)EPikachuSkill.Charge);
        return true;
    }

    
    public bool Thunderbolt(BasePokemon target)
    {
        if (energy >= 1)
        {
            target.TakeDamage(6 * NextAttackBonus);
            NextAttackBonus = 1; // 重置攻击加成
            EventSystem.Instance.Invoke(EEvent.OnPikaChargeEnd);
            UseEnergy(1);
            EventSystem.Instance.Invoke<int>(EEvent.OnPikaUseSkill, (int)EPikachuSkill.Thunderbolt);
            return true;
        }

        return false;
    }
}