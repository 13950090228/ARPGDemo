using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>

public abstract class MonoSingLeton<T> : MonoBehaviour where T : MonoSingLeton<T>
{
    private static T m_Instance = null;

    //3.
    //设计阶段，写脚本没有挂在物体上，希望脚本单例模式
    //运行时，需要这个脚本唯一实例，第1次，调用instance
    //不管
    public static T instance
    {
        get
        {
            if(m_Instance == null)
            {
                m_Instance = FindObjectOfType(typeof(T)) as T;
                if (m_Instance == null)
                {
                    m_Instance = new GameObject("Singleton of" + typeof(T).ToString(), typeof(T)).GetComponent<T>();
                    m_Instance.Init();
                }
            }
          

            return m_Instance;
        }
    }

    private void Awake()
    {
        if (m_Instance == null)
        {
            m_Instance = this as T;
        }
    }
    //提供初始化一种选择
    public virtual void Init() { }

    //当程序退出时做清理工作，单例模式对象=null
    private void OnApplicationQuit()
    {
        m_Instance = null;
    }
}

