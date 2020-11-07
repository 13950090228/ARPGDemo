using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 角色动画系统
/// </summary>

namespace ARPGDemo.Character
{
    public class CharacterAnimation : MonoBehaviour
    {
        /// <summary>
        /// 动画组件
        /// </summary>
        private Animator anim;

        private void Start()
        {
            anim = GetComponentInChildren<Animator>();
        }

        string preAnimName = "idle";
        //播放动画
        public void PlayAnimation(string AnimName)
        {
            anim.SetBool(preAnimName, false);
            anim.SetBool(AnimName, true);
            
            preAnimName = AnimName;

        }
    }
}

