using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AI.FSM
{
    /// <summary>
    /// 待机状态类
    /// </summary>
    class IdleState:FSMState
    {
        public override void Init()
        {
            //设置状态的编号 为待机状态的编号
            stateid = FSMStateID.Idle;
        }

        public override void Action(BaseFSM basefsm)
        {

            //播放待机动画
            basefsm.PlayAnimation(basefsm.animParams.Idle);
        }

    }
}
