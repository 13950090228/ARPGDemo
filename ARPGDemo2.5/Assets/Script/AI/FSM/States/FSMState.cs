using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
namespace AI.FSM
{
    //状态抽象类
    abstract class FSMState
    {
        //状态编号
        public FSMStateID stateid;

        //条件列表
        private List<FSMTrigger> triggers = new List<FSMTrigger>();
        //转换映射表
        private Dictionary<FSMTriggerID, FSMStateID> map = new Dictionary<FSMTriggerID, FSMStateID>();

        public FSMState()
        {
            Init();
        }

        //状态行为
        public abstract void Action(BaseFSM basefsm);

        //添加条件
        public void AddTrigger(FSMTriggerID triggerid,FSMStateID stateid)
        {
            if (map.ContainsKey(triggerid))
            {
                map[triggerid] = stateid;
            }
            else
            {
                map.Add(triggerid, stateid);
                AddTriggerObject(triggerid);
            }
        }

        //添加条件对象
        private void AddTriggerObject(FSMTriggerID triggerid)
        {
            Type typeobj = Type.GetType("AI.FSM." + triggerid + "Trigger");
            
            if (typeobj != null)
            {
                var triggerobj = Activator.CreateInstance(typeobj) as FSMTrigger;
                triggers.Add(triggerobj);
            }
        }

        //进入状态
        public virtual void EnterState(BaseFSM basefsm)
        {

        }

        //退出状态
        public virtual void ExitState(BaseFSM basefsm)
        {

        }

        //查找映射
        public FSMStateID GetOutputState(FSMTriggerID triggerid)
        {
            if (map.ContainsKey(triggerid))
            {
                return map[triggerid];
            }

            return FSMStateID.None;
        }

        //初始化
        public abstract void Init();

        //条件检测,如果条件符合则进入下一个状态
        public virtual void Reason(BaseFSM basefsm)
        {

            for (int i = 0; i < triggers.Count; i++)
            {
                if (triggers[i].HandleTrigger(basefsm))
                {
                    basefsm.ChangeActiveState(triggers[i].triggerid);
                    return;
                }
            }
        }

        //删除条件
        public void RemoveTrigger(FSMTriggerID triggerid)
        {
            if (map.ContainsKey(triggerid))
            {
                map.Remove(triggerid);
                RemoveTriggerObject(triggerid);
            }
        }

        //移除条件对象
        private void RemoveTriggerObject(FSMTriggerID triggerid)
        {
            triggers.RemoveAll(t=>t.triggerid==triggerid);
        }


    }
}
