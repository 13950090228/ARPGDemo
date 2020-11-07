using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 游戏对象池
/// </summary>

public class GameObjectPool : MonoSingLeton<GameObjectPool>
{
    //1.创建池
    private GameObjectPool()
    {

    }
    //static private GameObjectPool obj = new GameObjectPool();

    ////提供得到对象的唯一通道
    //static public GameObjectPool GetObject()
    //{
    //    return obj;
    //}
    private Dictionary<string, List<GameObject>> cache = new Dictionary<string, List<GameObject>>();

    //2.创建一个对象并使用对象
    public GameObject CreateObject(string key,GameObject go,Vector3 pos,Quaternion rat)
    {
        //1.查找池中有无可用的游戏对象
        GameObject tempGo = FindUsable(key);


        //2.池中有从池中返回
        if (tempGo != null)
        {
            tempGo.transform.position = pos;
            tempGo.transform.rotation = rat;
            tempGo.SetActive(true);  //表示当前正在使用
        }
        else//3.池中没有加载，放入池中再返回
        {
            tempGo = Instantiate(go, pos, rat) as GameObject;
            //放入池中
            Add(key, tempGo);
        }


        tempGo.transform.parent = transform;

        return tempGo;
    }

    private GameObject FindUsable(string key)
    {
        if (cache.ContainsKey(key))
        {
            //从列表中找出未激活的游戏物体
            return cache[key].Find((p) => !p.activeSelf);
        }
        return null;
    }

    private void Add(string key,GameObject go)
    {
        //检查池中有没有需要的key，没有的话则创建对应列表
        if (!cache.ContainsKey(key))
        {
            cache.Add(key,new List<GameObject>());
        }

        cache[key].Add(go);
    }

    //3.  释放资源：从池中删除对象
    //3.1 释放部分：按key释放

    public void Clear(string key)
    {

        if (cache.ContainsKey(key))
        {
            //销毁场景中游戏物体
            for (int i = 0; i < cache[key].Count; i++)
            {
                Destroy(cache[key][i]);
            }
            //紧紧移除了字典中对象地址
            cache.Remove(key);
        }
    }
    //3.2 释放全部
    public void ClearAll()
    {
        List<string> list = new List<string>(cache.Keys);
        for (int i = 0; i < list.Count; i++)
        {
            Clear(list[i]);
        }
    }
    //4.  回收对象【从画面中消失】
    //4.1 即使回收对象
    public void CollectObject(GameObject go)
    {
        go.SetActive(false);
    }
    //4.2 延时回收对象  协程
    public void CollectObject(GameObject go,float delay)
    {
        StartCoroutine(CollectDelay(go,delay));
    }

    private IEnumerator CollectDelay(GameObject go,float delay)
    {
        yield return new WaitForSeconds(delay);
        CollectObject(go);
    }

}
