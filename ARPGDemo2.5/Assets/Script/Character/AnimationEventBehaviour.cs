using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 动画时间行为  播放某个动画F1{攻击动画}  某个行为发生F2{攻击方法}
/// </summary>

namespace ARPGDemo.Character
{
    public class AnimationEventBehaviour : MonoBehaviour
    {
        /// <summary>
        /// 动画组件
        /// </summary>
        private Animator anim;

        private void Start()
        {
            anim = GetComponent<Animator>();
        }



        /// <summary>
        /// 撤销动画播放
        /// </summary>
        public void OnCancelAnim(string animName)
        {
            anim.SetBool(animName, false);
        }

        //定义委托:数据类型嵌套
        public delegate void AttackHandler();
        //使用事件设计模式  定义事件名称 = 声明委托对象
        public event AttackHandler attackHandler;

        /// <summary>
        /// 攻击时使用
        /// </summary>
        public void OnAttack()
        {
            if (attackHandler != null)
            {
                attackHandler();
            }
        }
    }
}

    

