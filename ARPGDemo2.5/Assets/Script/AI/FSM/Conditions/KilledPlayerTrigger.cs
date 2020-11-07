using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace AI.FSM
{
    class KilledPlayerTrigger:FSMTrigger
    {
        public override bool HandleTrigger(BaseFSM basefsm)
        {
            bool b = true;

            if (basefsm.targetObj != null)
            {
                b = basefsm.targetObj.GetComponent<ARPGDemo.Character.CharacterStatus>().HP <= 0;
                if (b)
                {
                    basefsm.targetObj= null;//!!!
                } 
                return b;

            }


            return b;
        }

        public override void Init()
        {
            triggerid = FSMTriggerID.KilledPlayer;
        }
    }
}
