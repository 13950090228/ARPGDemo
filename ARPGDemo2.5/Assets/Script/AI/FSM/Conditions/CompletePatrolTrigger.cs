using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AI.FSM
{
    class CompletePatrolTrigger: FSMTrigger
    {
        public override bool HandleTrigger(BaseFSM basefsm)
        {
            return basefsm.isPatrolComplete;
        }

        public override void Init()
        {
            triggerid = FSMTriggerID.CompletePatrol;
        }
    }
}
