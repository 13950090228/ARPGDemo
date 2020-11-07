using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>

public class practice01 : MonoBehaviour 
{
    public void Awake()
    {
        print("Awake");
    }
    public void OnEnable()
    {
        print("OnEnable");
    }

    public void OnDisable()
    {
        print("OnDisable");
    }
}
