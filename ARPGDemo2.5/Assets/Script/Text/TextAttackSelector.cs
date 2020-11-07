using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>

namespace ARPGDemo.Skill
{
    public class TextAttackSelector : MonoBehaviour
    {
        IAttackSelector select01 = new CircleAttackSelector();
        IAttackSelector select02 = new SectorAttackSelector();
        SkillData skillData = new SkillData();

        private void Start()
        {
            


        }

        private void Update()
        {
            skillData.attackDistance = 10;
            skillData.attackAngle = 60;
            skillData.attackType = SkillAttackType.Group;


            //var enemy = select01.SelectTarget(skillData, transform);

            var enemy = select02.SelectTarget(skillData, transform);
            if (enemy != null && enemy.Length != 0)
            {
                for (int i = 0; i < enemy.Length; i++)
                {
                    enemy[i].GetComponent<Renderer>().material.color = Color.red;
                }
            }
        }
    }
}

