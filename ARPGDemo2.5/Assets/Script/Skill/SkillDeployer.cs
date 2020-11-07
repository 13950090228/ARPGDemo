using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ARPGDemo.Skill
{
    /// <summary>
    /// 技能释放类
    /// </summary>
    abstract public class SkillDeployer:MonoBehaviour
    {
        //要释放的技能
        private SkillData m_SkillData;
        public SkillData skillData
        {
            get { return m_SkillData; }
            set {
                if (value == null)
                {
                    return;
                }
                m_SkillData = value;
                attackSelector = DeployerConfig.CreateAttackSelector(m_SkillData);
                listSelfImpact = DeployerConfig.CreateSelfImpact(m_SkillData);
                listTargerImpact = DeployerConfig.CreateTargerImpact(m_SkillData);
            }

        }



        protected IAttackSelector attackSelector;

        protected List<ISelfImpact> listSelfImpact = new List<ISelfImpact>();

        protected List<ITargerImpact> listTargerImpact = new List<ITargerImpact>();

        abstract public void DeploySkill();

        /// <summary>
        /// 每攻击完一次就要重置目标
        /// </summary>
        public GameObject[] ResetTargets()
        {
            var targets = attackSelector.SelectTarget(m_SkillData, transform);
            if (targets != null && targets.Length>0)
            {
                return targets;
            }

            return null;

        }

        /// <summary>
        /// 技能回收
        /// </summary>
        public void CollectSkill()
        {
            if (m_SkillData.durationTime > 0)
            {
                GameObjectPool.instance.CollectObject(gameObject, m_SkillData.durationTime);
            }
            else
            {
                GameObjectPool.instance.CollectObject(gameObject);
            }
        }
    }
}
