using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ARPGDemo.Skill
{
    public class CircleAttackSelector:IAttackSelector
    {
        /// <summary>
        /// 影响目标算法
        /// 圆形范围查找
        /// </summary>
        /// <param name="deployer">技能释放器</param>
        /// <param name="skillData">技能数据对象</param>
        /// <param name="goSelf">目标对象</param>
        public GameObject[] SelectTarget(SkillData skillDate, Transform skillTransform)
        {
            //1.物体没tag标记用射线找，有tag标记用标记找，性能高
            //找出标记为tag的所有物体
            var colloders = Physics.OverlapSphere(skillTransform.position, skillDate.attackDistance);
            if (colloders == null || colloders.Length == 0)
            {
                return null;
            }
            //2.活着的
            var enemys = ArrayHelper.FindAll(colloders, c => (Array.IndexOf(skillDate.attackTargetTags, c.tag) >= 0)
             && (c.gameObject.GetComponent<Character.CharacterStatus>().HP > 0)
            );
            if (enemys == null || enemys.Length == 0)
            {
                return null;
            }


            //3.根据攻击类型返回干个或多个

            switch (skillDate.attackType)
            {
                case SkillAttackType.Group:
                    return ArrayHelper.Select(enemys, (e) => e.gameObject);
                //break;

                case SkillAttackType.Single:
                    var collider = ArrayHelper.Min(enemys, (e) => Vector3.Distance(skillTransform.position,e.gameObject.transform.position));
                    return new GameObject[] { collider.gameObject };

            }
            return null;
        }


    }
}
