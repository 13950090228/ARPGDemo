using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using ARPGDemo.Character;
using System.Collections;


namespace ARPGDemo.Skill
{
    /// <summary>
    /// 目标伤害影响类
    /// </summary>
    public class DamagTargetImpact:ITargerImpact
    {
        private int baseDamage = 0;

        public void TargerImpact(SkillDeployer deployer, SkillData skillData, GameObject goSelf)
        {
            if (skillData.Owner!=null && skillData.Owner.gameObject != null)
            {
                baseDamage = skillData.Owner.GetComponent<CharacterStatus>().Damage;
            }
            deployer.StartCoroutine(RepeatDamage(deployer, skillData));
        }

        //多次伤害
        private IEnumerator RepeatDamage(SkillDeployer deployer,SkillData skillData)
        {
        
            float attackTime = 0;

            do
            {
                if (skillData.attackTargets != null && skillData.attackTargets.Length != 0)
                {
                    //对目标执行伤害
                    for (int i = 0; i < skillData.attackTargets.Length; i++)
                    {
                        OnceDamage(skillData, skillData.attackTargets[i]);
                    }
                }

                yield return new WaitForSeconds(skillData.damageInterval);
                attackTime += skillData.damageInterval;

                //攻击一次要重置目标
                skillData.attackTargets = deployer.ResetTargets();


            } while (attackTime < skillData.durationTime);  //防止死循环

        }

        //单次伤害
        private void OnceDamage(SkillData skillData,GameObject goTarget)
        {

            //1.调用角色的Damage方法

            var chStatus = goTarget.GetComponent<CharacterStatus>();
            var damageVal = skillData.damage * baseDamage;
            chStatus.OnDamage((int)damageVal);

            //2.将受击特效挂载到目标身上
            //创建一个受击特效预制件
            
            if (skillData.hitFxPrefab!=null && chStatus.HitFxPos != null)
            {

                var hitGo = GameObjectPool.instance.CreateObject(skillData.hitFxName, skillData.hitFxPrefab, chStatus.HitFxPos.position, chStatus.HitFxPos.rotation);
                //将对象挂在挂载点上
                hitGo.transform.SetParent(chStatus.HitFxPos);
                GameObjectPool.instance.CollectObject(hitGo);
            }
            //特效播放完回收
            

            

        }
    }
}
