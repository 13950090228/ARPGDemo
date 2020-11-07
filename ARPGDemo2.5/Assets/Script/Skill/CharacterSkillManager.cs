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
    /// 技能管理类
    /// </summary>
    class CharacterSkillManager:MonoBehaviour
    {
        //管理多个技能数据对象
        public List<SkillData> skills = new List<SkillData>();

        private void Start()
        {
            foreach (var skill in skills)
            {
                if (!(string.IsNullOrEmpty(skill.perfabName)) && skill.skillPrefab==null)
                {

                    skill.skillPrefab = LoadPrefab(skill.perfabName);
                }

                if (!(string.IsNullOrEmpty(skill.hitFxName)) && skill.hitFxPrefab)
                {
                    skill.hitFxPrefab = LoadPrefab(skill.hitFxName);
                }

                
                skill.Owner = gameObject;
            }
        }

        /// <summary>
        /// 动态加载 预制件资源
        /// </summary>
        /// <param name="resName">预制件名称</param>
        private GameObject LoadPrefab(string resName)
        {
            //动态加载预制件
            //var prefabGo = Resources.Load<GameObject>(resName);
            var prefabGo = ResourceManager.Load<GameObject>(resName);

            //使用游戏对象池，防止第一次使用技能出现卡帧
            var tempGo = GameObjectPool.instance.CreateObject(resName, prefabGo, transform.position, transform.rotation);
            GameObjectPool.instance.CollectObject(tempGo);

            return prefabGo;
        }

        /// <summary>
        /// 准备技能
        /// </summary>
        /// <param name="id">技能编号</param>
        public SkillData PrePareSkill(int id)
        {
            var skill = skills.Find((a) => a.skillID == id);
            if (skill != null)
            {
                if(skill.coolRemain==0 && skill.costSP <= skill.Owner.GetComponent<CharacterStatus>().SP)
                {
                    return skill;
                }
            }
            return null;
        }

        /// <summary>
        /// 施放技能   调用释放器
        /// </summary>
        /// 
        /// <param name="skillData"></param>
        public void DeploySkill(SkillData skillData)
        {

            //1.创建技能预制件特效  对象池中创建
            var SpVal = GetComponent<CharacterStatus>().SP;

            var tempGo = GameObjectPool.instance.CreateObject(skillData.name, skillData.skillPrefab, transform.position, transform.rotation);

            //2.为技能预制件对象设置当前要使用的这个技能
            var deployer = tempGo.GetComponent<SkillDeployer>();
            //3.调用释放器的方法
            deployer.skillData = skillData;
            deployer.DeploySkill();
            //冷却计时
            StartCoroutine(CoolTimeDown(skillData));
            //技能对象需要回收，留给释放器完成
        }

        /// <summary>
        /// 技能冷却处理
        /// </summary>
        /// <param name="skill"></param>
        private IEnumerator CoolTimeDown(SkillData skillData)
        {
            skillData.coolRemain = skillData.coolTime;

            while (skillData.coolRemain > 0)
            {
                yield return new WaitForSeconds(1);
                skillData.coolRemain -= 1;
            }

            skillData.coolRemain = 0;

        }

        /// <summary>
        /// 获取技能剩余冷却时间
        /// </summary>
        /// <param name="id"></param>
        public float GetSkillCoolRemain(int id)
        {
            return skills.Find((a) => a.skillID == id).coolRemain;
        }
    }
}
