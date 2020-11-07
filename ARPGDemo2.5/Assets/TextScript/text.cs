using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HedgehogTeam.EasyTouch;
/// <summary>
/// 
/// </summary>

public class text : MonoBehaviour 
{
    private void Update()
    {

        //EasyTouch5.0新版本可以在EasyTouch.current获取玩家在屏幕的手势
        Gesture currentGesture = EasyTouch.current;
        //将玩家在屏幕输入的手势类型与系统设置的手势进行匹配
        //当玩家没有触摸屏幕的时候currentGesture会返回null
        if (currentGesture != null && EasyTouch.EvtType.On_TouchStart == currentGesture.type)
        {
            //写法一：直接在Update里书写游戏逻辑
            OnTouchStart(currentGesture);
        }
        if (currentGesture != null && EasyTouch.EvtType.On_TouchUp == currentGesture.type)
        {
            //写法2：游戏逻辑放在一个单独的方法调用
            OnTouchEnd(currentGesture);
        }
        if (currentGesture != null && EasyTouch.EvtType.On_Swipe == currentGesture.type)
        {
            OnSwipe(currentGesture);
        }
    }

    private void OnTouchStart(Gesture gesture)
    {
        print("OnTouchStart");
        print("StartPosition:" + gesture.startPosition);
    }

    private void OnTouchEnd(Gesture gesture)
    {
        print("OnTouchEnd");
        print("ActionTime:" + gesture.actionTime);
    }

    private void OnSwipe(Gesture gesture)
    {
        print("OnSwipe");
        print("StartPosition:" + gesture.swipe);
    }
}
