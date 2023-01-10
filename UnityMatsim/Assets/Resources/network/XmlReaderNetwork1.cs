using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;
using UnityEditor;
using UnityEngine;

public class XmlReaderNetwork1 : MonoBehaviour
{
    string path;    //xml doc path (open in explorer and load)
    public Dictionary<string, MyNode1> myNodeDic = new Dictionary<string, MyNode1>();
    public GameObject nodeGameObject;
    public Dictionary<string, MyLink1> myLinkDic = new Dictionary<string, MyLink1>();
    public GameObject linkGameObject;

    public GameObject nodeParent;
    public GameObject linkParent;
    public Vector3 networkPosition;
    bool netWorkPositionSet = true;
    public void OpenFileExplorer()
    {
        path = EditorUtility.OpenFilePanel("Show all XML-Files (.xml)", "", "xml");
        GetXml();
    }
    public void ButtonInstantiateNetwork()
    {
        InstantiateNodes(myNodeDic);
        InstantiateLinks(myLinkDic);
    }
    void GetXml()
    {
        XmlDocument xml = new XmlDocument();
        xml.Load(path);
        GetNodes(xml);
        GetLinks(xml);
    }
    private void GetNodes(XmlDocument xml)
    {
        XmlNodeList elemList = xml.GetElementsByTagName("nodes");
        foreach (XmlNode i in elemList)
        {
            XmlNodeList elemList2 = xml.GetElementsByTagName("node");
            foreach (XmlNode j in elemList2)
            {
                MyNode1 node = new MyNode1();
                string id = "";
                var attribute = j.Attributes["id"];
                if (attribute != null)
                {
                    id = (attribute.Value);
                    node.id = (attribute.Value);
                }

                var attribute2 = j.Attributes["x"];
                if (attribute2 != null)
                {
                    node.x = float.Parse(attribute2.Value, new CultureInfo("en-US"));
                }

                var attribute3 = j.Attributes["y"];
                if (attribute3 != null)
                {
                    node.y = float.Parse(attribute3.Value, new CultureInfo("en-US"));
                }
                myNodeDic.Add(id, node);
            }
        }
    }

    private void InstantiateNodes(Dictionary<string, MyNode1> myNodeDic)
    {
        float x;
        float y;
        foreach (KeyValuePair<string, MyNode1> entry in myNodeDic)
        {
            x = entry.Value.x;
            y = entry.Value.y;
            GameObject brick = Instantiate(nodeGameObject, new Vector3(x, 0, y), Quaternion.identity,nodeParent.transform);
            //brick.transform.localScale = Vector3.one * (1/3);
            if (netWorkPositionSet == true)
            {
                networkPosition = new Vector3(x, 0, y);
                netWorkPositionSet = false;
            }
        }
        
    }

    private void GetLinks(XmlDocument xml)
    {
        XmlNodeList elemList = xml.GetElementsByTagName("links");

        foreach (XmlNode i in elemList)
        {
            XmlNodeList elemList2 = xml.GetElementsByTagName("link");
            foreach (XmlNode j in elemList2)
            {
                string id = "";
                MyLink1 link = new MyLink1();
                var attribute = j.Attributes["id"];
                if (attribute != null)
                {
                    link.id = (attribute.Value);
                    id = (attribute.Value);
                }

                var attribute2 = j.Attributes["from"];
                if (attribute2 != null)
                {
                    link.from = (attribute2.Value);
                }

                var attribute3 = j.Attributes["to"];
                if (attribute3 != null)
                {
                    link.to = (attribute3.Value);
                }

                var attribute4 = j.Attributes["length"];
                if (attribute4 != null)
                {
                    link.length = float.Parse(attribute4.Value);
                }

                var attribute5 = j.Attributes["freespeed"];
                if (attribute5 != null)
                {
                    link.freespeed = float.Parse(attribute5.Value);
                }

                var attribute6 = j.Attributes["permlanes"];
                if (attribute6 != null)
                {
                    link.permlanes = float.Parse(attribute6.Value);
                }

                MyNode1 fromNode = myNodeDic[link.from];
                MyNode1 toNode = myNodeDic[link.to];

                //mittelpunkt berechnen
                link.mittelpunktX = (fromNode.x + toNode.x) / 2;
                link.mittelpunktY = (fromNode.y + toNode.y) / 2;

                //durchmesser
                double XX = fromNode.x - toNode.x;
                double YY = fromNode.y - toNode.y;

                link.durchmesser = Math.Sqrt(Math.Pow(XX, 2) + Math.Pow(YY, 2));

                //Winkel
                if ((toNode.x - fromNode.x) == 0)
                {
                    link.rotation = 90;
                }
                else
                {
                    double anstiegZ = (toNode.y - fromNode.y);
                    double anstiegX = (toNode.x - fromNode.x);
                    double anstieg = (float)(Math.Atan(anstiegZ / anstiegX));
                    link.rotation = (float)((anstieg * (360 / (2 * Math.PI))) * (-1));

                    //Links rechts verschieben
                    //Vector3 normalVektor = new Vector3((float)anstiegZ, 0, -(float)anstiegX);
                    //link.normiertNormalVektor = 50*link.permlanes*normalVektor/ Mathf.Sqrt(normalVektor.x*normalVektor.x+normalVektor.z*normalVektor.z);
                    
                }

                myLinkDic.Add(id, link);
            }
        }
    }
    private void InstantiateLinks(Dictionary<string, MyLink1> myLinkDic)
    {
        float mittelpunktX;
        float mittelpunktY;
        float rotation;
        double durchmesser;
        foreach (KeyValuePair<string, MyLink1> entry in myLinkDic)
        {
            mittelpunktX = entry.Value.mittelpunktX + entry.Value.normiertNormalVektor.x;
            mittelpunktY = entry.Value.mittelpunktY + entry.Value.normiertNormalVektor.z;
            rotation = entry.Value.rotation;
            durchmesser = entry.Value.durchmesser;
            GameObject brick = Instantiate(linkGameObject, new Vector3(mittelpunktX, 0, mittelpunktY), Quaternion.Euler(new Vector3(0, rotation, 0)),linkParent.transform);
            brick.transform.localScale = new Vector3((float)durchmesser, 0.2f, 50 /*entry.Value.permlanes*/);
        }
    }
}


//Debug.Log(myNodeDic.Count);
//        foreach (KeyValuePair<int, MyNode1> entry in myNodeDic)
//        {
//            Debug.Log(entry.Key);
//            Debug.Log(entry.Value.x);
//            Debug.Log(entry.Value.y);
//        }


