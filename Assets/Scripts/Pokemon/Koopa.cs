using UnityEngine;


public class Koopa : BasePokemon
{
    public override void Awake()
    {
        base.Awake();
    }
    
    public void NormalAttack(BasePokemon target)
    {
        target.TakeDamage(3);
        RestoreEnergy(1);
    }

    public bool ShellDefense()
    {
        if (energy >= 1)
        {
            Heal(2);
            ApplyDefenseBonus(2);
            UseEnergy(1);
            return true;
        }

        return false;
    }

    public bool ShellRush(BasePokemon target)
    {
        if (energy >= 2)
        {
            target.TakeDamage(6);
            // 敌方眩晕逻辑需要在战斗控制器中实现
            target.IsStunned = true;
            UseEnergy(2);
            return true;
        }

        return false;
    }
    
    public void UpdateStatus()
    {
        if (NextDefenseBonus > 0)
        {
            defense -= NextDefenseBonus;
            NextDefenseBonus = 0;
        }
    }

    // 应用单回合防御加成的方法
    public void ApplyDefenseBonus(int bonus)
    {
        defense += bonus;
        NextDefenseBonus = bonus;
    }
}
