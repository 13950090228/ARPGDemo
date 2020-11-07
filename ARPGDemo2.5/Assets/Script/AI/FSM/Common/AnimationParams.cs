using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// 动画参数类
/// </summary>
namespace AI.FSM
{
    [Serializable]
    public class AnimationParams 
    {
        public string Idle = "Idle";
        public string Dead = "dead";
        public string Run = "run";
        public string Walk = "walk";
        public string Attack = "attack";
    }
}

