using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
namespace AI.FSM
{
    class WithOutAttackRangeTrigger:FSMTrigger
    {

        public override bool HandleTrigger(BaseFSM basefsm)
        {
            bool b = false;
            if (basefsm.targetObj != null)
            {
                var distance = Vector3.Distance(basefsm.transform.position, basefsm.targetObj.position);
                b = distance < basefsm.sightDistance && distance > basefsm.chState.attackDistance;
                return b;
            }

            return b;
        }

        public override void Init()
        {
            triggerid = FSMTriggerID.WithOutAttackRange;
        }
    }
}
