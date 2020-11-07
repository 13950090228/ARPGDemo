using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AI.FSM
{
    /// <summary>
    /// 生命为0条件类
    /// </summary>
    class NoHealthTrigger:FSMTrigger
    {

        public override bool HandleTrigger(BaseFSM basefsm)
        {
            return basefsm.chState.HP <= 0;
        }

        public override void Init()
        {
            triggerid = FSMTriggerID.NoHealth;
        }
    }
}
