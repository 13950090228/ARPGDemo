using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>

public class TriggerDemo : MonoBehaviour 
{
    public void PrintMsg(GameObject go)
    {
        if (go == null)
        {
            print("null");
        }
        else
        {
            print(go.name);
        }
    }

    public void PrintOK()
    {
        print("OK");
    }
}
