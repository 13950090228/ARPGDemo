using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
namespace AI.FSM
{
    class LosePlayerTrigger:FSMTrigger
    {
        public override bool HandleTrigger(BaseFSM basefsm)
        {
            bool b = true;

            if (basefsm.targetObj != null)
            {
                b = Vector3.Distance(basefsm.transform.position, basefsm.targetObj.position) > basefsm.sightDistance;

                if (b)
                {
                    basefsm.targetObj = null;
                }
                return b;


            }
            return b;
        }

        public override void Init()
        {
            triggerid = FSMTriggerID.LosePlayer;
        }
    }
}
