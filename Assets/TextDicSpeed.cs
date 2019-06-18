using UnityEngine;
using System.Collections;
using System.Diagnostics;
using System;
using System.Linq;
using System.Collections.Generic;
public class TextDicSpeed : MonoBehaviour
{
    Dictionary<int, int> dic;
    GameMap<int, int> dic1;
    // Use this for initialization
    void Start()
    {
        //dic = CreateArr(10000);
        dic1 = CreateArr1(10000);
        int k;
        int v;
        Stopwatch time = new Stopwatch();
        time.Start();
        //foreach (var item in dic) //时间为1
        //{
        //    k = item.Key;
        //    v = item.Value;
        //    if (k == 500)
        //    {
        //        //会报错就先不移除了
        //        // dic.Remove(k);
        //    }
        //}

        //for (int i = 0; i < dic.Count; i++)//时间为4370
        //{
        //    var item = dic.ElementAt(i);
        //    k = item.Key;
        //    v = item.Value;
        //    if (k == 500)
        //    {
        //        dic.Remove(k);
        //    }
        //}

        for (int i = 0; i < dic1.Count; i++)//时间为3
        {

            k = dic1.getKeyByIndex(i);
            v = dic1.getDataByIndex(i);
            if (k == 500)
            {
                dic1.Remove(k);
            }
        }
        time.Stop();
        UnityEngine.Debug.Log(time.ElapsedMilliseconds);
    }
    /// <summary>
    /// 创建一个很大的Dic
    /// </summary>
    /// <param name="v"></param>
    /// <returns></returns>
    public static Dictionary<int, int> CreateArr(int v)
    {
        Dictionary<int, int> arr = new Dictionary<int, int>();
        System.Random r = new System.Random();
        for (int i = 0; i < v; i++)
        {
            arr.Add(i, r.Next(0, v));
        }
        return arr;
    }

    /// <summary>
    /// 创建一个很大的Dic1
    /// </summary>
    /// <param name="v"></param>
    /// <returns></returns>
    public static GameMap<int, int> CreateArr1(int v)
    {
        GameMap<int, int> arr = new GameMap<int, int>();
        System.Random r = new System.Random();
        for (int i = 0; i < v; i++)
        {
            arr.Add(i, r.Next(0, v));
        }
        return arr;
    }
}
