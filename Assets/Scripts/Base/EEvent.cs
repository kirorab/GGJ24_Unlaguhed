public enum EEvent
{
    /// <summary>
    /// 玩家血条改变时
    /// </summary>
    OnPlayerHealthChange,
    /// <summary>
    /// 开始对话时
    /// </summary>
    OnStartDialogue,
    OnstartDialogueWithDialogues,
    /// <summary>
    /// 结束对话时
    /// </summary>
    OnEndDialogue,
    /// <summary>
    /// 存档失败
    /// </summary>
    OnSaveFailed,
    /// <summary>
    /// 切换场景前
    /// </summary>
    BeforeLoadScene,
    /// <summary>
    /// 开始与乌龟的战斗时
    /// </summary>
    OnStartTurtleBattle,
    /// <summary>
    /// 弹出乌龟选择框时
    /// </summary>
    OnTurtleChoose,
    /// <summary>
    /// 结束乌龟选择，true为放过，false为杀害
    /// </summary>
    OnEndTurtleChoose, 
    /// <summary>
    /// 结束与乌龟的战斗时
    /// </summary>
    OnEndTurtleBattle,
    /// <summary>
    /// 触发与宝可梦的战斗时
    /// </summary>
    OnTriggerPokemonBattle,
    /// <summary>
    /// 开始与宝可梦的战斗时
    /// </summary>
    OnStartPokemonBattle,
    /// <summary>
    /// 宝可梦的血量和能量变化时，传递两个参数，第一个为数值，第二个为宝可梦
    /// </summary>
    OnPokemonHealthChange,
    OnPokemonEnergyChange,
    /// <summary>
    /// 结束与宝可梦的战斗时
    /// </summary>
    OnEndPokemonBattle,
    /// <summary>
    /// bug选择等待时
    /// </summary>
    OnBugWait,
    /// <summary>
    /// 当进入笑的选择时
    /// </summary>
    OnLaughChoose,
    /// <summary>
    /// 结束笑的选择，true为放笑，false为不笑
    /// </summary>
    OnEndLaughChoose,
}
