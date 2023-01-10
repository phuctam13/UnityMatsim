using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEditor;
using UnityEngine;

public class XmlReaderNetwork : MonoBehaviour
{
    
    string path;    //xml doc path (open in explorer and load)
    public List<MyNode> nodeList = new List<MyNode>();
    public GameObject nodeGameObject;
    public List<MyLink> linkList = new List<MyLink>();
    public GameObject linkGameObject;
    public void OpenFileExplorer()
    {
        path = EditorUtility.OpenFilePanel("Show all XML-Files (.xml)", "", "xml");
        GetXml();
    }

    public void ButtonInstantiateNetwork()
    {
        InstantiateNodes(nodeList);
        InstantiateLinks(linkList);
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
        for (int i = 0; i < elemList.Count; i++)
        {

            XmlNodeList elemList2 = xml.GetElementsByTagName("node");
            for (int j = 0; j < elemList2.Count; j++)
            {
                MyNode node = new MyNode();
                var attribute = elemList2[j].Attributes["id"];
                if (attribute != null)
                {
                    node.id = Int32.Parse(attribute.Value);
                }

                var attribute2 = elemList2[j].Attributes["x"];
                if (attribute2 != null)
                {
                    node.x = float.Parse(attribute2.Value);
                }

                var attribute3 = elemList2[j].Attributes["y"];
                if (attribute3 != null)
                {
                    node.y = float.Parse(attribute3.Value);
                }

                nodeList.Add(node);
            }
        }
    }

    private void InstantiateNodes(List<MyNode> nodeList)
    {
        float x;
        float y;
        for(int i = 0; i< nodeList.Count; i++)
        {
            x = nodeList[i].x;
            y = nodeList[i].y;
            GameObject brick = Instantiate(nodeGameObject, new Vector3(x,0,y), Quaternion.identity);
        }
    }

    private void GetLinks(XmlDocument xml)
    {
        XmlNodeList elemList = xml.GetElementsByTagName("links");
        for (int i = 0; i < elemList.Count; i++)
        {

            XmlNodeList elemList2 = xml.GetElementsByTagName("link");
            for (int j = 0; j < elemList2.Count; j++)
            {
                MyLink link = new MyLink();
                var attribute = elemList2[j].Attributes["id"];
                if (attribute != null)
                {
                    link.id = Int32.Parse(attribute.Value);
                }

                var attribute2 = elemList2[j].Attributes["from"];
                if (attribute2 != null)
                {
                    link.from = Int32.Parse(attribute2.Value);
                }

                var attribute3 = elemList2[j].Attributes["to"];
                if (attribute3 != null)
                {
                    link.to = Int32.Parse(attribute3.Value);
                }

                var attribute4 = elemList2[j].Attributes["length"];
                if (attribute4 != null)
                {
                    link.length = float.Parse(attribute4.Value);
                }

                var attribute5 = elemList2[j].Attributes["freespeed"];
                if (attribute5 != null)
                {
                    link.freespeed = float.Parse(attribute5.Value);
                }

                MyNode fromNode = nodeList[link.from-1];
                MyNode toNode = nodeList[link.to - 1];

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
                    link.rotation = (float)((anstieg * (360 / (2 * Math.PI)))*(-1));
                }


                linkList.Add(link);
            }
        }
    }
    private void InstantiateLinks(List<MyLink> linkList)
    {
        float mittelpunktX;
        float mittelpunktY;
        float rotation;
        double durchmesser;
        for (int i = 0; i < linkList.Count; i++)
        {
            mittelpunktX = linkList[i].mittelpunktX;
            mittelpunktY = linkList[i].mittelpunktY;
            rotation = linkList[i].rotation;
            durchmesser = linkList[i].durchmesser;
            GameObject brick = Instantiate(linkGameObject, new Vector3(mittelpunktX, 0, mittelpunktY), Quaternion.Euler(new Vector3(0, rotation, 0)));
            brick.transform.localScale = new Vector3((float)durchmesser, 0.2f,50);
        }
    }
}




/*
    private void parseXmlFile(XmlDocument xml)
    {
        XmlNode elem = xml.DocumentElement.FirstChild;
        Debug.Log("Display the InnerText of the element...");
        Debug.Log(elem.InnerText);

        // InnerXml includes the markup of the element.
        Debug.Log("Display the InnerXml of the element...");
        Debug.Log(elem.InnerXml);

        // Set InnerText to a string that includes markup.
        // The markup is escaped.
        elem.InnerText = "Text containing <markup/> will have char(<) and char(>) escaped.";
        Debug.Log(elem.OuterXml);

        // Set InnerXml to a string that includes markup.
        // The markup is not escaped.
        elem.InnerXml = "Text containing <markup/>.";
        Debug.Log(elem.OuterXml);
    }
    */

//private void getElements(XmlDocument xml)
//{
//    //Display all the book titles.
//    XmlNodeList elemList = xml.GetElementsByTagName("nodes");
//    for (int i = 0; i < elemList.Count; i++)
//    {
//        //Debug.Log(elemList[i].InnerXml);

//        XmlNodeList elemList2 = xml.GetElementsByTagName("node");
//        for (int j = 0; j < elemList2.Count; j++)
//        {
//            var attribute = elemList2[j].Attributes["id"];
//            if (attribute != null)
//            {
//                string id = attribute.Value;
//                Debug.Log(id);
//            }

//            var attribute2 = elemList2[j].Attributes["x"];
//            if (attribute2 != null)
//            {
//                string x = attribute2.Value;
//                Debug.Log(x);
//            }

//            var attribute3 = elemList2[j].Attributes["y"];
//            if (attribute3 != null)
//            {
//                string y = attribute3.Value;
//                Debug.Log(y);
//            }
//        }
//    }
//}
