using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ARPGDemo.Skill
{
    /// <summary>
    /// 近身技能释放类
    /// </summary>
    public class MeleeSkillDeployer:SkillDeployer
    {


        override public void DeploySkill()
        {
            if (skillData == null)
            {
                return;
            }
            //1.确定目标
            skillData.attackTargets = ResetTargets();

            //2.执行自身影响
            listSelfImpact.ForEach(p => p.SelfImpact(this, skillData, skillData.Owner));

            //3.执行目标影响
            listTargerImpact.ForEach(p => p.TargerImpact(this, skillData, null));

            //4.回收技能
            CollectSkill();
        }


    }
}
