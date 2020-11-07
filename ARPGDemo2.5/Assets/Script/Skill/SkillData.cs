using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// 技能数据类
/// </summary>
namespace ARPGDemo.Skill
{
    [Serializable]
    public class SkillData
    {
        //技能数据
        
        //技能ID
        public int skillID;

        //技能名称
        public string name;

        //技能描述
        public string description;

        //冷却时间
        public float coolTime;

        //冷却剩余
        public float coolRemain;

        //魔法消耗
        public int costSP;

        //攻击距离
        public float attackDistance;

        //攻击角度
        public int attackAngle;

        //攻击目标
        public string[] attackTargetTags = { "Enemy","Boss"};

        //攻击目标对象数组
        [HideInInspector]
        public GameObject[] attackTargets;

        //连击的下一个技能编号
        public int nextBatterId;

        //伤害比率
        public float damage;

        //持续时间
        public float durationTime;

        //伤害间隔
        public float damageInterval;

        //技能拥有者
        [HideInInspector]
        public GameObject Owner;

        //技能预制件名称
        public string perfabName;

        //技能预制件对象
        [HideInInspector]
        public GameObject skillPrefab;

        //动画名称
        public string animationName;

        //受击特效名称
        public string hitFxName;

        //受击特效预制件
        [HideInInspector]
        public GameObject hitFxPrefab;

        //技能等级
        public int level;

        //是否激活
        public bool activated;

        //攻击类型 单攻 群攻
        public SkillAttackType attackType;

        //伤害模式 圆形，扇形，矩形
        public DamageMode damageMode;





    }
}

