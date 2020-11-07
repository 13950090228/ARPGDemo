using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ARPGDemo.Skill
{
    /// <summary>
    /// 自身影响接口【算法】
    /// </summary>
    public interface ISelfImpact
    {
        /// <summary>
        /// 影响自身方法
        /// </summary>
        /// <param name="deployer">技能释放器</param>
        /// <param name="skillData">技能数据对象</param>
        /// <param name="goSelf">自身或队友对象</param>
        void SelfImpact(SkillDeployer deployer,SkillData skillData,GameObject goSelf);
    }
}
