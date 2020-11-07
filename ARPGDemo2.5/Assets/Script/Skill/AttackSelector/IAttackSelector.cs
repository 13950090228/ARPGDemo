using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
namespace ARPGDemo.Skill
{
    /// <summary>
    /// 攻击选择接口【算法】   选择：选择什么范围内的敌人受到攻击
    ///                       例如： 扇形或圆形
    /// </summary>
    public interface IAttackSelector
    {
        /// <summary>
        /// 选择目标方法：选择哪些敌人作为要攻击的目标
        /// </summary>
        /// <param name="skillDate">技能对象</param>
        /// <param name="transform">变换点：选择时的一个参考点，技能拥有者</param>
        /// <returns></returns>
        GameObject[] SelectTarget(SkillData skillDate, Transform skillTransform);

    }

}

