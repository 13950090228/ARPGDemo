using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
namespace AI.FSM
{
    class ReachPlayerTrigger:FSMTrigger
    {
        public override bool HandleTrigger(BaseFSM basefsm)
        {
            bool b = false;
            if (basefsm.targetObj != null)
            {
                b = Vector3.Distance(basefsm.transform.position, basefsm.targetObj.position) < basefsm.chState.attackDistance;
            }
            
            return b;
        }

        public override void Init()
        {
            triggerid = FSMTriggerID.ReachPlayer;
        }
    }
}
