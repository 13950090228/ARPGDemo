using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace AI.FSM
{
    class AttackingState:FSMState
    {
        private float attackTime = 0;

        public override void Init()
        {
            //设置状态的编号 为待机状态的编号
            stateid = FSMStateID.Attacking;
        }


        public override void Action(BaseFSM basefsm)
        {
            if (attackTime > basefsm.chState.attackSpeed)
            {
                //播放攻击动画


                basefsm.AutoUseSkill();
                attackTime = 0;
            }
            attackTime = attackTime + Time.deltaTime;
            basefsm.transform.LookAt(basefsm.targetObj);

            basefsm.transform.localEulerAngles -= new Vector3(basefsm.transform.rotation.x, 0, 0);


            basefsm.transform.localEulerAngles -= new Vector3(0, 0, basefsm.transform.rotation.z);




        }

        public override void EnterState(BaseFSM basefsm)
        {
            basefsm.StopMove();
            basefsm.PlayAnimation(basefsm.animParams.Idle);
        }

    }
}
