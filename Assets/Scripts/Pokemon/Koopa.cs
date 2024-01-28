using UnityEngine;


public class Koopa : BasePokemon
{
    public override void Awake()
    {
        base.Awake();
    }
    public enum EKoopaSkill
    {
        NormalAttack,
        ShellDefense,
        ShellRush
    }
    
    public void NormalAttack(BasePokemon target)
    {
        UpdateStatus();
        target.TakeDamage(3);
        RestoreEnergy(1);
        EventSystem.Instance.Invoke<int>(EEvent.OnKoopaUseSkill, (int)EKoopaSkill.NormalAttack);
    }

    public bool ShellDefense()
    {
        if (energy >= 1)
        {
            UpdateStatus();
            Heal(2);
            ApplyDefenseBonus(2);
            UseEnergy(1);
            EventSystem.Instance.Invoke<int>(EEvent.OnKoopaUseSkill, (int)EKoopaSkill.ShellDefense);
            return true;
        }

        return false;
    }

    public bool ShellRush(BasePokemon target)
    {
        if (energy >= 2)
        {
            UpdateStatus();
            target.TakeDamage(6);
            // 敌方眩晕逻辑需要在战斗控制器中实现
            target.IsStunned = true;
            UseEnergy(2);
            EventSystem.Instance.Invoke<int>(EEvent.OnKoopaUseSkill, (int)EKoopaSkill.ShellRush);
            return true;
        }

        return false;
    }
    
    public void UpdateStatus()
    {
        if (NextDefenseBonus > 0)
        {
            defense -= NextDefenseBonus;
            EventSystem.Instance.Invoke(EEvent.OnKoopaDefenseEnd);
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
