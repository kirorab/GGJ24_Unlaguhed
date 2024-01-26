public enum EEvent
{
    /// <summary>
    /// 玩家血量改变时
    /// </summary>
    OnPlayerHealthChange,
    
    /// <summary>
    /// 切换场景前
    /// </summary>
    BeforeLoadScene,
    /// <summary>
    /// 开始与乌龟的战斗时
    /// </summary>
    OnStartTurtleBattle,
    /// <summary>
    /// 结束与乌龟的战斗时
    /// </summary>
    OnEndTurtleBattle,
    /// <summary>
    /// 开始与宝可梦的战斗时
    /// </summary>
    OnStartPokemonBattle,
    /// <summary>
    /// 结束与宝可梦的战斗时
    /// </summary>
    OnEndPokemonBattle,
}
