using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 角色状态类
/// </summary>

namespace ARPGDemo.Character
{
    abstract public class CharacterStatus : MonoBehaviour, IOnDamager
    {
        /// <summary>
        /// 生命值和最大生命值
        /// </summary>
        public int HP;
        public int MaxHP;

        /// <summary>
        /// 魔法值和最大魔法值
        /// </summary>
        public int SP;
        public int MaxSP;

        /// <summary>
        /// 伤害
        /// </summary>
        public int Damage;

        /// <summary>
        /// 防御
        /// </summary>
        public int Defence;

        /// <summary>
        /// 攻击速度
        /// </summary>
        public float attackSpeed;

        /// <summary>
        /// 攻击距离
        /// </summary>
        public float attackDistance;
        private int field;



        /// <summary>
        /// 死亡
        /// </summary>
        abstract public void Dead();

        virtual public void OnDamage(int damageVal)
        {
            //所有受到伤害是共性 HP减少
            //减去受击者防御力
            damageVal = damageVal - Defence;
            

            if (damageVal > 0)
            {
                HP -= damageVal;
            }

            if (HP <= 0)
            {
                Dead();
            }
            
        }

        //受击，同时播放受击特效：需要找到受击特效挂载点
        public Transform HitFxPos;
        private void Start()
        {
            HitFxPos = TransformHelper.FindChild(transform, "HitFxPos");
        }
    }
}

