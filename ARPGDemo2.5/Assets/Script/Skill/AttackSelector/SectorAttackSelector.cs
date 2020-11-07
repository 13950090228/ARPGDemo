using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ARPGDemo.Skill
{
    public class SectorAttackSelector:IAttackSelector
    {
        /// <summary>
        /// 影响目标算法
        /// 扇形范围查找
        /// </summary>
        /// <param name="deployer">技能释放器</param>
        /// <param name="skillData">技能数据对象</param>
        /// <param name="goSelf">目标对象</param>
        public GameObject[] SelectTarget(SkillData skillDate, Transform skillTransform)
        {

            ////1.物体没tag标记用射线找，有tag标记用标记找，性能高
            ////找出标记为tag的所有物体
            List<GameObject> listTargets = new List<GameObject>();

            for (int i = 0; i < skillDate.attackTargetTags.Length; i++)
            {
                var targets = GameObject.FindGameObjectsWithTag(skillDate.attackTargetTags[i]);
                if (targets != null || targets.Length > 0)
                {
                    listTargets.AddRange(targets);
                }
                    
            }

            if (listTargets.Count == 0)
            {
                return null;
            }


            ////2.过滤指定半径，HP>0,角度范围
            var enemys = listTargets.FindAll((go) => 
            (Vector3.Distance(skillTransform.position,go.transform.position)<skillDate.attackDistance) 
            && (go.gameObject.GetComponent<Character.CharacterStatus>().HP > 0) 
            && (Vector3.Angle(skillTransform.forward,go.transform.position-skillTransform.position)<=skillDate.attackAngle*0.5f)
            );

            if (enemys == null && enemys.Count == 0)
            {
                return null;
            }

            ////3.根据攻击类型返回干个或多个

            switch (skillDate.attackType)
            {
                case SkillAttackType.Group:
                    return enemys.ToArray();
                //break;

                case SkillAttackType.Single:

                    var collider = ArrayHelper.Min(enemys.ToArray(), (e) => Vector3.Distance(skillTransform.position, e.gameObject.transform.position));
                    if (collider!=null)
                    {
                        return new GameObject[] { collider.gameObject };
                    }
                    else
                    {
                        return null;
                    }
                    

            }
            return null;
        }


    }
}
