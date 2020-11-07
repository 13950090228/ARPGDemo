using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AI.FSM
{
    /// <summary>
    /// 发现目标条件类
    /// </summary>
    class SawPlayerTrigger : FSMTrigger
    {

        public override bool HandleTrigger(BaseFSM basefsm)
        {
            bool b = false;
            if (basefsm.targetObj != null)
            {
                b = true;
                return b;
            }
            return b;
        }

        public override void Init()
        {
            triggerid = FSMTriggerID.SawPlayer;
        }
    }
}
