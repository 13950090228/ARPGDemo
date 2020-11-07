using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>

public class TextGameObjectPool : MonoBehaviour 
{
    public GameObject[] prefabs;
    public string[] names = { "Sphere","Cube"};

    GameObjectPool poolobj = null;

    public void Start()
    {

        poolobj = GameObjectPool.instance;
    }

    public void OnGUI()
    {
        if (GUILayout.Button("创建"))
        {
            for (int i = 0; i < 3; i++)
            {
                Vector3 pos = new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));


                poolobj.CreateObject(names[0], prefabs[0], pos, Quaternion.identity);
            }
        }

        if (GUILayout.Button("回收"))
        {
            var go = GameObject.FindGameObjectWithTag(names[0]);
            print(go);
            if (go!=null)
            {
                
                poolobj.CollectObject(go);
            }
            
        }

        if (GUILayout.Button("全部回收"))
        {
            poolobj.ClearAll();

        }

    }

    public void Update()
    {
        
    }
}
