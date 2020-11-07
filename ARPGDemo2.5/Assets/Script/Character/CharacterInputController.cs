using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.Linq;
using HedgehogTeam.EasyTouch;
//using ARPGDemo.Skill;

namespace ARPGDemo.Character
{
    /// <summary>
    /// 角色控制类
    /// </summary>

    public class CharacterInputController : MonoBehaviour
    {
        /// <summary>
        /// 马达
        /// </summary>
        private CharacterMontor chMotor;

        private ETCJoystick joyStick;

        private ETCButton skill01;

        private ETCButton skill02;

        private ETCButton baseAttack;

        //private CharacterSkillManager chSkillMsg;

        private PlayerStatus playerStatus;

        /// <summary>
        /// 初始化第一次赋值
        /// </summary>
        
        private void Start()
        {
            playerStatus = GetComponent<PlayerStatus>();
            //chSkillMsg = GetComponent<CharacterSkillManager>();
            chMotor = GetComponent<CharacterMontor>();
            joyStick = ETCInput.GetControlJoystick("MyJoystick");
            skill01 = ETCInput.GetControlButton("Skill1");
            skill02 = ETCInput.GetControlButton("Skill2");
            baseAttack = ETCInput.GetControlButton("BaseAttack");


            //joyStick.onMove.AddListener(JoystickMove);
            //绑定事件
            joyStick.onMove.AddListener(JoystickMove);
            joyStick.onMoveEnd.AddListener(JoystickMoveEnd);

            baseAttack.onDown.AddListener(() => ButtonDown(10));
            skill01.onDown.AddListener(()=>ButtonDown(11));
            skill02.onDown.AddListener(() => ButtonDown(12));




        }



        public void OnEnable()
        {
            

        }

        public void OnDisable()
        {
            
        }

        private void ButtonDown(int id)
        {
            //SkillData skill = chSkillMsg.PrePareSkil(id);
            //if (skill != null)
            //{
            //    playerStatus.SP -= skill.costSP;
            //    chSkillMsg.DeploySkill(skill);
            //}

            var chSkillSys = GetComponent<CharacterSkillSystem>();
            chSkillSys.AttackUseSkill(id, false);



        }

        /// <summary>
        /// 遥感移动执行的方法
        /// </summary>
        public void JoystickMove(Vector2 value)
        {

            chMotor.Move(value.x, value.y);

        }


        /// <summary>
        /// 遥感停止时执行的方法
        /// </summary>
        public void JoystickMoveEnd()
        {

            chMotor.Move(0, 0);
        }


    }
}
