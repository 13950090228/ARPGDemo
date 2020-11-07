using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
namespace AI.FSM
{
    /// <summary>
    /// 巡逻类
    /// </summary>
    class PatrollingState:FSMState
    {
        private int currentWayPoint = 0;

        public override void Init()
        {
            //设置状态的编号 为待机状态的编号
            stateid = FSMStateID.Patrolling;
        }

        public override void Action(BaseFSM basefsm)
        {
            basefsm.PlayAnimation(basefsm.animParams.Walk);
            basefsm.MoveToTarget(basefsm.wayPoints[currentWayPoint].position, basefsm.walkSpeed, basefsm.patrolArrivalDistance);
            //1.是否到达当前路点
            if (Vector3.Distance(basefsm.transform.position, basefsm.wayPoints[currentWayPoint].position) < basefsm.patrolArrivalDistance)
            {
                
                //2.是否是最后一个路点
                if (currentWayPoint == basefsm.wayPoints.Length - 1)
                {
                    //根据巡逻的方式决定结束或开始
                    switch (basefsm.patrolMode)
                    {
                        case PatrolMode.Once:
                            basefsm.isPatrolComplete = true;
                            return;

                        case PatrolMode.PingPong:
                            break;

                        case PatrolMode.Loop:
                            currentWayPoint = 0;
                            break;
                    }

                }
                currentWayPoint += 1;
            }

        }







    }
}
