using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AI.FSM
{
    /// <summary>
    /// 待机状态类
    /// </summary>
    class PursuitState: FSMState
    {
        public override void Init()
        {
            //设置状态的编号 为追逐状态的编号
            stateid = FSMStateID.Pursuit;
        }

        public override void Action(BaseFSM basefsm)
        {
            //1.条件需要有追逐的目标
            if (basefsm.targetObj != null)
            {
                //2.播放追逐动画
                basefsm.PlayAnimation(basefsm.animParams.Run);
                basefsm.MoveToTarget(basefsm.targetObj.position, basefsm.moveSpeed, basefsm.chState.attackDistance);

                //3.主要控制追的速度，靠近的距离==攻击距离
            }
            else
            {
                return;
            }

        }

        public override void ExitState(BaseFSM basefsm)
        {
            //4.停下来转换状态追逐=>攻击
            basefsm.StopMove();
        }

    }
}
