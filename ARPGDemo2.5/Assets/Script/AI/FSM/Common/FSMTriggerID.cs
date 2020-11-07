using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ID状态转换条件枚举
/// </summary>
namespace AI.FSM
{
    public enum FSMTriggerID
    {
        //生命为0
        NoHealth,

        //发现目标
        SawPlayer,

        //目标进入攻击范围
        ReachPlayer,

        //丢失玩家
        LosePlayer,

        //完成巡逻
        CompletePatrol,

        //打死目标
        KilledPlayer,

        //目标不在攻击范围
        WithOutAttackRange
    }
}

