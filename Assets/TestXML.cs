using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
public class TestXML : MonoBehaviour
{
    Dictionary<string, SpinEquipment> partXMl = new Dictionary<string, SpinEquipment>();
    Dictionary<int, ChaiAnimation> stepXml = new Dictionary<int, ChaiAnimation>();
    Dictionary<int, Vec3> step = new Dictionary<int, Vec3>();
    Dictionary<int, Vec3> step1 = new Dictionary<int, Vec3>();
    // Use this for initialization
    void Start()
    {
        XMLConfigParser partXMLConfig = new XMLConfigParser();
        //partXMl = partXMLConfig.LoadConfig<string, SpinEquipment>("PartXml", GetNodePath("Knit", "Circular", "danmianji"), "ShowName");
        //for (int i = 0; i < partXMl.Count; i++)
        //{
        //    var item = partXMl.ElementAt(i);
        //    Debug.Log(item.Value.ShowName);
        //    Debug.Log(item.Value.Name);
        //}

        //stepXml = partXMLConfig.LoadConfig<int, ChaiAnimation>("Step", GetNodePath("duobilongtou", "Zhuang"), "ID");
        //for (int i = 0; i < stepXml.Count; i++)
        //{
        //    var item = stepXml.ElementAt(i);
        //    Debug.Log(item.Value.ID);
        //    Debug.Log(item.Value.Info);
        //    Debug.Log(item.Value.tool);
        //}
        Vec3 vec3 = new Vec3();
        vec3.vec = new Vector3(1, 1, 1);
        vec3.id = 1;
        step.Add(1, vec3);
        partXMLConfig.WriteConfig<int, Vec3>(step, "Step1", GetNodePath("564", "fsdf", "yuy"));
        step1 = partXMLConfig.LoadConfig<int, Vec3>("Step1", GetNodePath("564", "fsdf", "yuy"), "id");
    }

    public string GetNodePath(string workShopName, string productLineName, string euqipMentName)
    {
        //WorkShop[@Name='']就是WorkShop里面的Name=''的路径
        return $"tent/WorkShop[@Name='{workShopName}']/ProductLine[@Name='{productLineName}']/Equipment[@Name='{euqipMentName}']/EquipmentList/Part";
    }
    public string GetNodePath(string equName, string actionName)
    {
        //WorkShop[@Name='']就是WorkShop里面的Name=''的路径
        return $"Content/Equ[@Name='{ equName }']/action[@Name='{ actionName }']/Step";
    }

    // Update is called once per frame
    void Update()
    {
    }
}
public class Vec3
{
    public int id;
    public Vector3 vec;
}

public class SpinEquipment
{
    public string ShowName;
    public string Name;
    public int ID;
    public int Type;
    public string Path;
    public string isHaveChild;
    public float Dis;
    public float Min;
    public float Max;
}

public class ChaiAnimation
{
    public int ID;
    public int type;
    public EType tool;
    public string partNm;
    public string Info;
}

public enum EType
{
    Hand = 0,
    X,
    Y,
    Z,
    W,
    G
}