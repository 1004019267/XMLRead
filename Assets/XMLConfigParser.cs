using System.Collections.Generic;
using System.Xml;
using System.Reflection;
using UnityEngine;
using System;
using System.IO;
using System.Xml.Linq;

/// <summary>
/// XML配置解析器 
/// </summary>
public class XMLConfigParser : IXMLConfigParser
{
    /// <summary>
    /// 载入xml配置
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="tablename"></param>
    /// <returns></returns>
    public override Dictionary<I, T> LoadConfig<I, T>(string tablename, string nodePath, string identify)
    {
        // 定义配置字典
        Dictionary<I, T> dic = new Dictionary<I, T>();

        // 定义xml文档
        XmlDocument doc = new XmlDocument();
        //加载路径
        string path = Application.dataPath + "/Config/" + tablename + ".xml";
        doc.Load(path);

        // 通过节点路径获取配置的节点列表
        XmlNodeList nodeList = doc.SelectNodes(nodePath);
        // 遍历节点列表，并获取列表中的所有数据
        for (int i = 0; i < nodeList.Count; i++)
        {
            // 获取节点列表的一个子节点，并强制转化为Xml元素;
            XmlNode node = nodeList[i];
            XmlElement elem = (XmlElement)node;

            // 生成一个配置对象，并将对象的成员赋值
            T obj = GreateAndSetValue<T>(elem);

            // 获取对象类型，并通过identify获取域的值当key
            FieldInfo fieldInfo = obj.GetType().GetField(identify.ToString());
            if (fieldInfo == null)
            {
                throw new Exception("请把类内部的参数用public出来");
            }
            I key;
            try
            {
                key = (I)fieldInfo.GetValue(obj);
            }
            catch (Exception)
            {
                throw new Exception("Key只能是C#基础数据结构不能是自定义类型");
            }
            // 将读出的对象添加到配置字典中
            if (!dic.ContainsKey(key))
            {
                dic.Add(key, obj);
            }
        }
        Debug.Log("XML表" + tablename + "加载完毕");
        return dic;
    }

    /// <summary>
    /// 写入表 没有就创建 有就插入式写入
    /// </summary>
    /// <typeparam name="I"></typeparam>
    /// <typeparam name="T"></typeparam>
    /// <param name="dic"></param>
    /// <param name="tablename"></param>
    /// <param name="nodePath"></param>
    public override void WriteConfig<I, T>(Dictionary<I, T> dic, string tablename, string nodePath)
    {
        //把路径划分
        string[] sArray = nodePath.Split('/');
        List<XMLClass> xmlClasses = GetNodes(sArray);

        string path = Application.dataPath + "/Config/" + tablename + ".xml";
        XmlDocument xmlDoc = new XmlDocument();

        LoadOrCreateXML(xmlDoc,path);

        XmlElement xmlE = null;
        XmlElement xmlELast = null;//上一个节点
        //到倒数第二个停止循环 因为创建复数个Data时会因为这个循环创建
        //一个空的节点    
        for (int i = 0; i < xmlClasses.Count - 1; i++)
        {
            if (i == 0)
            {
                //如果根节点不存在 或者根节点名字不一样
                if (xmlDoc.DocumentElement == null)
                {
                    xmlE = xmlDoc.CreateElement(xmlClasses[i].className);
                    xmlDoc.AppendChild(xmlE);
                }
                else if (xmlDoc.DocumentElement.Name != xmlClasses[i].className)
                {
                    throw new Exception("一个XML不允许有两个根节点");
                }
                else
                {
                    //节点存在已有数据找该往哪个父节点插入
                    xmlE= GetParentNodeWithPath(xmlDoc, sArray,ref i);
                }
            }
            else
            {

                //创建节点
                xmlE = xmlDoc.CreateElement(xmlClasses[i].className);
                if (xmlClasses[i].keyWord != null)
                {
                    //设置名字 和属性
                    xmlE.SetAttribute(xmlClasses[i].keyWord, xmlClasses[i].value);
                }
                if (xmlELast != null)
                {
                    //往表里添加子节点
                    xmlELast.AppendChild(xmlE);
                }
            }
            xmlELast = xmlE;
            if (i == xmlClasses.Count - 2)
            {
                CreateInsideData<I, T>(dic, xmlDoc, xmlE, xmlClasses[xmlClasses.Count - 1]);
            }


        }
        //存表
        xmlDoc.Save(path);
        Debug.Log("XML表" + tablename + "生成完毕，在" + path + "目录下");
    }
}