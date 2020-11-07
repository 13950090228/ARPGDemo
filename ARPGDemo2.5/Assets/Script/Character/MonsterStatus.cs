using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 小怪状态
/// </summary>

namespace ARPGDemo.Character
{
    public class MonsterStatus : CharacterStatus
    {
        /// <summary>
        /// 贡献经验值
        /// </summary>
        public int GiveExp;

        public override void Dead()
        {
            StartCoroutine(IsHit());
            
        }

        private IEnumerator IsHit()
        {

            yield return new WaitForSeconds(5);
            GameObjectPool.instance.CollectObject(gameObject);

        }

        public override void OnDamage(int damageVal)
        {
            base.OnDamage(damageVal);

        }

    }
}
