using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ARPGDemo.Skill
{
    /// <summary>
    /// 影响目标算法 抽象 【接口】
    /// </summary>
    public interface ITargerImpact
    {
        /// <summary>
        /// 影响目标算法
        /// </summary>
        /// <param name="deployer">技能释放器</param>
        /// <param name="skillData">技能数据对象</param>
        /// <param name="goSelf">目标对象</param>
        void TargerImpact(SkillDeployer deployer, SkillData skillData, GameObject goSelf);
    }
}
