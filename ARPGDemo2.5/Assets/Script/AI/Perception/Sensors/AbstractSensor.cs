using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
namespace AI.Perception
{
    /// <summary>
    /// 抽象感应器
    /// </summary>

    public abstract class AbstractSensor:MonoBehaviour
    {
        //是否移除、是否禁用
        public bool isRemove;

        //没有感知事件  （没看到 做什么）
        public event Action OnNonPerception;
        //感知事件   （看到了 做什么）
        public event Action<List<AbstractTrigger>> OnPerception;


        /// <summary>
        /// 初始化
        /// </summary>
        private void Start()
        {
            Init();
            //把当前感应器放到 感应系统中
            SensorTriggerSystem sys = SensorTriggerSystem.instance;
            sys.AddSensor(this);
        }

        abstract public void Init();

        /// <summary>
        /// 销毁方法:把当前感应器从感应系统中移除
        /// </summary>
        private void OnDestroy()
        {
            isRemove = true;
        }

        /// <summary>
        /// 检测触发器：检查所有的触发器（触发条件）
        /// </summary>
        /// <param name="list"></param>
        public void OnTestTriggers(List<AbstractTrigger> listTriggers)
        {
            //找到启用的所有触发器
            listTriggers = listTriggers.FindAll(t => t.enabled && t.gameObject!=this.gameObject && TestTrigger(t));

            //触发感知事件
            if (listTriggers.Count > 0)
            {
                if (OnPerception != null)
                {
                    OnPerception(listTriggers);
                }
            }
            else
            {
                if (OnNonPerception != null)
                {
                    OnNonPerception();
                }
            }
            
        
        }

        /// <summary>
        /// 检测触发器是否被感知
        /// </summary>
        /// <param name="absTtrigger"></param>
        abstract protected bool TestTrigger(AbstractTrigger absTtrigger);
    }

}