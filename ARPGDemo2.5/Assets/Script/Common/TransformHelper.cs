using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 变换组件工具类
/// </summary>

public class TransformHelper : MonoBehaviour 
{
    /// <summary>
    /// 查找子物体
    /// </summary>
    /// <param name="tran">父物体变化组件引用</param>
    /// <param name="goName">想查找的子物体的名称</param>
    /// <returns></returns>
    public static Transform FindChild(Transform tran, string goName)
    {
        Transform child = tran.Find(goName);
        if (child != null)
        {
            return child;
        }

        Transform go;
        for (int i = 0; i < tran.childCount; i++)
        {
            child = tran.GetChild(i);
            go = FindChild(child, goName);
            if (go != null)
            {
                return go;
            }


        }
        return null;
    }


    /// <summary>
    /// 转向
    /// </summary>
    public static void LookAtTarger(Vector3 target,Transform transform,float rotationSpeed)
    {
        if (target != Vector3.zero)
        {
            //Quaternion dir = Quaternion.LookRotation(target);
            //transform.rotation = Quaternion.Lerp(transform.rotation, dir, rotationSpeed);

            Vector3 euler = Quaternion.LookRotation(target).eulerAngles;
            transform.eulerAngles = new Vector3(0, euler.y, 0);
        }

    }
}
