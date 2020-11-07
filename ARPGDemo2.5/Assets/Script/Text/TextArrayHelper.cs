using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>

public class TextArrayHelper : MonoBehaviour 
{
    

    public void Update()
    {
        //FindHPMax();
        //FindHPMaxs();
        FindDistanceMin();
    }
    public void FindHPMax()
    {
        GameObject[] obj = GameObject.FindGameObjectsWithTag("Enemy");
        var a = ArrayHelper.Max(obj, (go) => (go.GetComponent<EnemyHealth>().HP));
        a.GetComponent<Renderer>().material.color = Color.red;
    }

    public void FindHPMaxs()
    {
        GameObject[] obj = GameObject.FindGameObjectsWithTag("Enemy");
        var array = ArrayHelper.FindAll(obj, (go) => (go.GetComponent<EnemyHealth>().HP>20));
        foreach (var a in array)
        {
            a.GetComponent<Renderer>().material.color = Color.red;
        }
    }

    public void FindDistanceMin()
    {
        GameObject[] obj = GameObject.FindGameObjectsWithTag("Enemy");
        var a = ArrayHelper.Min(obj, (go) => (Vector3.Distance(transform.position,go.transform.position)));
        a.GetComponent<Renderer>().material.color = Color.red;
    }


}
