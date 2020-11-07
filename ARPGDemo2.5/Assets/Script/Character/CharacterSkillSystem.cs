using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using ARPGDemo.Skill;

namespace ARPGDemo.Character
{
    /// <summary>
    /// 角色系统和技能系统外观类
    /// </summary>
    class CharacterSkillSystem:MonoBehaviour
    {

        private CharacterAnimation chAnim;

        //当前攻击目标
        private GameObject currentAttackTarget;

        //当前使用技能
        private SkillData currentUseSkill;

        //技能管理
        private CharacterSkillManager skillMgr;


        private void Start()
        {
            chAnim = GetComponent<CharacterAnimation>();

            skillMgr = GetComponent<CharacterSkillManager>();

            //使用攻击事件 调用动画组件 动画播放 调用方法OnAttack
            GetComponentInChildren<AnimationEventBehaviour>().attackHandler += DeploySkill;
        }

        /// <summary>
        /// 使用指定编号的技能进行攻击
        /// </summary>
        /// <param name="skill">技能编号</param>
        /// <param name="Bool">是否连续攻击</param>
        public void AttackUseSkill(int skillID,bool isBatter)
        {
            //如果是连击，获取下一个技能编号
            if(isBatter && currentUseSkill!=null)
            {
                skillID = currentUseSkill.nextBatterId;
            }

            //1 通过编号 获取技能
            currentUseSkill = skillMgr.PrePareSkill(skillID);



            //2 播放技能对应的攻击动画，技能释放 由动画事件调用
            if (currentUseSkill == null)
            {
                print("播放攻击动画");
                return;
            }

            chAnim.PlayAnimation(currentUseSkill.animationName);

            //3 找出受攻击的目标
            var objTarget = SelectTarget();
            if (objTarget == null)
            {
                return;
            }
            //4 显示选中的目标效果
            ShowSelectedFx(false);
            //将刚找出的目标作为攻击目标
            currentAttackTarget = objTarget;
            ShowSelectedFx(true);
            //5 面向目标
            //transform.LookAt(objTarget.transform.position);
            Vector3 euler = Quaternion.LookRotation(objTarget.transform.position - transform.position).eulerAngles;
            transform.eulerAngles = new Vector3(0, euler.y, 0);
        }

        /// <summary>
        /// 调用技能释放器
        /// </summary>
        public void DeploySkill()
        {
            if (currentUseSkill != null)
            {
                skillMgr.DeploySkill(currentUseSkill);
            }
            
        }

        public void UseRandomSkill()
        {
            //从技能列表随机抽取一个可用技能
            //已经冷却好加上魔法值够用

            var usableSkills = skillMgr.skills.FindAll(skill => skill.coolRemain == 0
                && skill.costSP <= skill.Owner.GetComponent<CharacterStatus>().SP);
            //2>随机抽取集合中的一个技能对象 
            if (usableSkills != null && usableSkills.Count > 0)
            {
                var index = UnityEngine.Random.Range(0, usableSkills.Count);
                var skillId = usableSkills[index].skillID;  //根据对象找编号
                                                            //调用AttackUseSkill(int skillId,bool isBatter)
                AttackUseSkill(skillId, false);
            }

        }

        /// <summary>
        /// 选择攻击目标
        /// </summary>
        /// <returns></returns>
        private GameObject SelectTarget()
        {
            ////1.物体没tag标记用射线找，有tag标记用标记找，性能高
            ////找出标记为tag的所有物体
            List<GameObject> listTargets = new List<GameObject>();
            if (currentUseSkill.attackTargetTags == null && currentUseSkill.attackTargetTags.Length == 0)
            {
                return null;
            }

            for (int i = 0; i < currentUseSkill.attackTargetTags.Length; i++)
            {
                var targets = GameObject.FindGameObjectsWithTag(currentUseSkill.attackTargetTags[i]);
                if (targets != null && targets.Length > 0)
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
            (Vector3.Distance(transform.position, go.transform.position) < currentUseSkill.attackDistance)
            && (go.gameObject.GetComponent<CharacterStatus>().HP > 0));

            if (enemys == null && enemys.Count == 0)
            {
                return null;
            }

            ////3.根据攻击类型返回干个或多个


            var re = ArrayHelper.Min(enemys.ToArray(), (e) => Vector3.Distance(transform.position, e.gameObject.transform.position));
            return re;

        }
           
        
        /// <summary>
        /// 显示选中的目标效果（红圈）
        /// </summary>
        /// <param name="isShow">显示或隐藏</param>
        private void ShowSelectedFx(bool isShow)
        {
            Transform SelectedFx = null;
            if (currentAttackTarget != null)
            {
                SelectedFx = TransformHelper.FindChild(currentAttackTarget.transform,"selected");
            }

            if (SelectedFx != null)
            {
                SelectedFx.GetComponent<Renderer>().enabled = isShow;
            }
        }



    }
}
