using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace AI.Perception
{


    public abstract class AbstractTrigger:MonoBehaviour
    {
        //是否删除、是否禁用
        public bool isRemove;
        //触发器类型
        public TriggerType triggerType;

        /// <summary>
        /// 初始化
        /// </summary>
        private void Start()
        {
            Init();
            SensorTriggerSystem sys = SensorTriggerSystem.instance;
            sys.AddTrigger(this);
        }

        /// <summary>
        /// 初始化方法
        /// </summary>
        abstract public void Init();

        /// <summary>
        /// 销毁方法:把当前触发器从触发系统中移除
        /// </summary>
        public void OnDestroy()
        {
            isRemove = true;
        }


    }
}