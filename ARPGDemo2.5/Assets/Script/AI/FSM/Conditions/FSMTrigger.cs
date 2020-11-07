using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 条件抽象类
/// </summary>
namespace AI.FSM
{
    public abstract class FSMTrigger
    {
        public FSMTriggerID triggerid ;

        public FSMTrigger()
        {
            Init();
        }

        public abstract bool HandleTrigger(BaseFSM basefsm);

        public abstract void Init();


    }
}

