using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AI.FSM
{
    /// <summary>
    /// 死亡状态类
    /// </summary>
    class DeadState:FSMState
    {
        public override void Init()
        {
            //设置状态的编号 为待机状态的编号
            stateid = FSMStateID.Dead;
        }

        public override void Action(BaseFSM basefsm)
        {

            basefsm.PlayAnimation(basefsm.animParams.Dead);
            basefsm.StopMove();

            basefsm.enabled = false; //死亡动画播放完毕后不再进入别的状态
        }

        public override void EnterState(BaseFSM basefsm)
        {


            
        }

    }
}
