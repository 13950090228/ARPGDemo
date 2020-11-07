using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 状态编号枚举
/// </summary>
namespace AI.FSM
{
    public enum FSMStateID
    {
        //无
        None,

        //待机
        Idle,

        //死亡
        Dead,

        //追逐
        Pursuit,

        //攻击
        Attacking,

        //默认
        Default,

        //巡逻
        Patrolling
    }
}

