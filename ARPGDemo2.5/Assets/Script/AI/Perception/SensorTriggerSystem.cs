using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace AI.Perception
{
    public class SensorTriggerSystem: MonoSingLeton<SensorTriggerSystem>
    {
        //检查时间间隔
        public float checkInterval = 0.2f;
        //触发器列表
        private List<AbstractTrigger> listTrigger = new List<AbstractTrigger>();
        //感应器列表
        private List<AbstractSensor> listSensor = new List<AbstractSensor>();

        private SensorTriggerSystem() { }
        /// <summary>
        /// 添加感应器
        /// </summary>
        /// <param name="absSensor"></param>
        public void AddSensor(AbstractSensor absSensor)
        {
            listSensor.Add(absSensor);
        }

        /// <summary>
        /// 添加触发器
        /// </summary>
        /// <param name="absTrigger"></param>
        public void AddTrigger(AbstractTrigger absTrigger)
        {
            listTrigger.Add(absTrigger);
        }

        /// <summary>
        /// 检查触发条件:每个感应器 检查 对应触发器
        /// </summary>
        private void CheckTrigger()
        {
            for (int i = 0; i < listSensor.Count; i++)
            {
                if (listSensor[i].enabled)
                {
                    listSensor[i].OnTestTriggers(listTrigger);
                }
            }
        }

        private void OnDisable()
        {
            CancelInvoke("UpdateSystem");
        }

        private void OnEnable()
        {
            InvokeRepeating("UpdateSystem", 0, checkInterval);
        }


        /// <summary>
        /// 更新系统
        /// </summary>
        private void UpdateSystem()
        {
            listSensor.RemoveAll(s => s.isRemove);
            listTrigger.RemoveAll(t => t.isRemove);
        }
    }
}
