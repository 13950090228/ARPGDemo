using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



public delegate TKey SelectHandler<T, TKey>(T t);
//public delegate TKey Handler2<T, TKey>(T t, TKey tkey);
public delegate bool FindHandler<T>(T t);

static class ArrayHelper
{


    /// <summary>
    /// 升序排序
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="array"></param>
    static public void OrderBy<T>(T[] array)where T: System.IComparable<T>
    {
        for (int i = 0; i < array.Length; i++)
        {
            for (int j = 0; j < array.Length; j++)
            {
                if (array[i].CompareTo(array[j])>0)
                {
                    var a = array[j];
                    array[j] = array[i];
                    array[i] = a;
                }
            }
            
        }
    }


    /// <summary>
    /// 降序排序
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="array"></param>
    /// <param name="compare"></param>
    static public void OrderByDescending<T>(T[] array,IComparer<T> compare) where T : IComparable<T>
    {
        for (int i = 0; i < array.Length; i++)
        {
            for (int j = 0; j < array.Length; j++)
            {
                if (compare.Compare(array[i],array[j]) > 0)
                {
                    
                    var a = array[j];
                    array[j] = array[i];
                    array[i] = a;
                }
            }

        }
    }

    /// <summary>
    /// 返回最大的
    /// </summary>
    /// <param name="array">数据类型对象的数组</param>
    /// <param name="handler">
    static public T Max<T, TKey>(T[] array, SelectHandler<T, TKey> handler) where TKey : IComparable<TKey>
    {
        T temp = default(T);

        if (array != null && array.Length != 0)
        {
            temp = array[0];
        }
        else
        {
            return temp;
        }

        for (int i = 1; i < array.Length; i++)
        {
            if (handler(temp).CompareTo(handler(array[i])) < 0)
            {
                temp = array[i];
            }
        }
        return temp;
    }

    /// <summary>
    /// 返回最小的
    /// </summary>
    /// <typeparam name="T">数据类型</typeparam>
    /// <typeparam name="TKey">数据类型字段的类型</typeparam>
    /// <param name="array">数据类型对象的数组</param>
    /// <param name="handler">
    /// 委托对象：负责 从某个类型中选取某个字段 返回这个字段的值
    /// </param>
    static public T Min<T, TKey>(T[] array, SelectHandler<T, TKey> handler)where TKey : IComparable<TKey>
    {
        T temp = default(T);
        
        if(array!=null && array.Length != 0)
        {
            temp = array[0];
        }
        else
        {
            return temp;
        }

        

        for (int i = 1; i < array.Length; i++)
        {
            if (handler(temp).CompareTo(handler(array[i])) > 0)
            {
                temp = array[i];
            }
        }
        return temp;
    }


    //实现数组工具类的 通用的查找的方法 Find
    //给定一个查找的条件？ 返回满足条件的一个
    static public T Find<T>(T[] array, FindHandler<T> handler)
    {
        T temp = default(T);
        for (int i = 0; i < array.Length; i++)
        {
            if (handler(array[i]))
            {
                return array[i];
            }
        }

        return temp;
    }

    //查找所有的方法 FindAll
    //给定一个查找的条件？ 返回满足条件的所有的
    static public T[] FindAll<T>(T[] array, FindHandler<T> handler)
    {
        List<T> list = new List<T>();
        for (int i = 0; i < array.Length; i++)
        {
            if (handler(array[i]))
            {
                list.Add(array[i]);
            }
        }
        return list.ToArray();
    }

    //选择：选取数组中对象的某些成员形成一个独立的数组
    //多个学生【id age tall score】   【60,50,70,80】
    //       【“zs”，“ls”】
    static public TKey[] Select<T, TKey>(T[] array, SelectHandler<T, TKey> handler)
    {
        TKey[] keys = new TKey[array.Length];
        for (int i = 0; i < array.Length; i++)
        {
            keys[i] = handler(array[i]);
        }
        return keys;
    }

}


