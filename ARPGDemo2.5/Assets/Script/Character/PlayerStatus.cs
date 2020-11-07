using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 主角状态
/// </summary>

namespace ARPGDemo.Character
{
    public class PlayerStatus : CharacterStatus
    {
        /// <summary>
        /// 经验值与最大经验值
        /// </summary>
        public int Exp;
        public int MaxExp;


        /// <summary>
        /// 收集经验
        /// </summary>
        public void CollectExp()
        {

        }

        /// <summary>
        /// 升级
        /// </summary>
        public void LevelUp()
        {

        }

        public override void Dead()
        {
            print("玩家死亡");
        }

        public override void OnDamage(int damageVal)
        {

            base.OnDamage(damageVal);
        }
    }
}

