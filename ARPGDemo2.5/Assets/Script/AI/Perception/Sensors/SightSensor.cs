using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace AI.Perception
{
    class SightSensor:AbstractSensor
    {

        //视距
        public float sightDistance;
        //视角
        public float sightAngle;
        //启动角度检查
        public bool enableAngle;
        //启动遮挡检查
        public bool enableRay;
        //发射点
        public Transform sendPos;

        public override void Init()
        {
            
        }

        protected override bool TestTrigger(AbstractTrigger absTtrigger)
        {
            //如果当前类型不是视觉直接返回false看不到
            if (absTtrigger.triggerType != TriggerType.Sight)
            {
                return false;
            }

            var tempTrigger = absTtrigger as SightTrigger;

            var dir = tempTrigger.recivePos.position - sendPos.position;

            bool result = dir.magnitude < sightDistance;

            //角度检测
            if (enableAngle)
            {
                bool b1 = Vector3.Angle(transform.forward, dir) < sightAngle/2;

                result = result && b1;
            }

            //遮挡检测
            RaycastHit hit;
            if (enableRay)
            {
                //判断射中的物体是不是触发器物体
                bool b2 = Physics.Raycast(sendPos.position, dir, out hit, sightDistance) && hit.collider.gameObject == tempTrigger.gameObject;

                result = result && b2;
            }


            return result;
        }
    }
}
