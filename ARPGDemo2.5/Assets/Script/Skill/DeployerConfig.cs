using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ARPGDemo.Skill
{
    /// <summary>
    /// 释放器初始化类=配置
    /// </summary>
    class DeployerConfig:MonoBehaviour
    {
        /// <summary>
        /// 工厂方法设计模式：创建目标选择对象方法
        /// </summary>
        /// <param name="skillData"></param>
        /// <returns></returns>
        public static IAttackSelector CreateAttackSelector(SkillData skillData)
        {
            IAttackSelector attackSelector = null;

            //switch (skillData.damageMode)
            //{
            //    case DamageMode.Circle:
            //        attackSelector = new CircleAttackSelector();
            //        break;
            //    case DamageMode.Sector:
            //        attackSelector = new SectorAttackSelector();
            //        break;
            //}
            string allName = "ARPGDemo.Skill."+ skillData.damageMode + "AttackSelector";
            Type typeObj = Type.GetType(allName);
            object obj = Activator.CreateInstance(typeObj);
            attackSelector = (IAttackSelector)obj;

            return attackSelector;
        }

        /// <summary>
        /// 初始化自身影响
        /// </summary>
        /// <param name="skillData"></param>
        /// <returns></returns>
        public static List<ISelfImpact> CreateSelfImpact(SkillData skillData)
        {
            List<ISelfImpact> list = new List<ISelfImpact>();
            list.Add(new CostSPSelfImpact());





            return list;
        }


        /// <summary>
        /// 初始化目标影响
        /// </summary>
        /// <param name="skillData"></param>
        /// <returns></returns>
        public static List<ITargerImpact> CreateTargerImpact(SkillData skillData)
        {
            List<ITargerImpact> list = new List<ITargerImpact>();
            list.Add(new DamagTargetImpact());



            return list;
        }
    }
}
