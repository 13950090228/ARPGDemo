using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 角色马达
/// </summary>

namespace ARPGDemo.Character
{
    public class CharacterMontor : MonoBehaviour
    {
        /// <summary>
        /// 移动速度
        /// </summary>
        public float moveSpeed = 1;

        /// <summary>
        /// 转向速度
        /// </summary>
        public float rotationSpeed = 1;

        /// <summary>
        /// 动画系统
        /// </summary>
        private CharacterAnimation chAnim;

        /// <summary>
        /// 角色控制器
        /// </summary>
        private CharacterController chController;

        private void Start()
        {
            chAnim = GetComponent<CharacterAnimation>();
            chController = GetComponent<CharacterController>();
        }


        /// <summary>
        /// 移动
        /// </summary>
        public void Move(float x,float z)
        {
            if(x!=0 || z!= 0)
            {
                //1.转向
                //LookAtTarger(new Vector3(x, 0, z));
                TransformHelper.LookAtTarger(new Vector3(x, 0, z), transform, rotationSpeed);

                //2.向目标运动：调用角色控制器的运动方法
                //-1表示模拟重力，贴地走路


                Vector3 motion = new Vector3(transform.forward.x, -1, transform.forward.z);
                chController.Move(motion * Time.deltaTime * moveSpeed);


                //3.播放动画
                chAnim.PlayAnimation("run");
            }
            else
            {
                chAnim.PlayAnimation("idle");
            }




        }

        /// <summary>
        /// 转向
        /// </summary>
        private void LookAtTarger(Vector3 target)
        {
            if (target != Vector3.zero)
            {
                Quaternion dir = Quaternion.LookRotation(target);
                transform.rotation = Quaternion.Lerp(transform.rotation, dir, rotationSpeed);
            }

        }
    }
}

