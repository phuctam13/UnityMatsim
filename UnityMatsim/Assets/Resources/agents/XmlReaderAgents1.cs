using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEditor;
using UnityEngine;

public class XmlReaderAgents1 : MonoBehaviour
{
    string path;
    Dictionary<string, Agent1> myAgentDic = new Dictionary<string, Agent1>();

    public GameObject carModel;
    XmlReaderNetwork1  network;

    private void Start()
    {
        network = GameObject.FindObjectOfType<XmlReaderNetwork1>();
    }
    public void OpenFileExplorer()
    {
        path = EditorUtility.OpenFilePanel("Show all XML-Files (.xml)", "", "xml");
        GetXml();
    }

    private void GetXml()
    {
        XmlDocument xml = new XmlDocument();
        xml.Load(path);
        GetAgents(xml);
    }

    public void ButtonInstantiateAgents()
    {
        InstantiateAgents(myAgentDic);
    }

    private void InstantiateAgents(Dictionary<string, Agent1> AgentDic)
    {
        foreach (KeyValuePair<string, Agent1> entry in AgentDic)
        {
            GameObject agent = Instantiate(carModel, new Vector3(0, 0, 0), Quaternion.identity);
            CarModelScript carModelScript = agent.GetComponent<CarModelScript>();
            carModelScript.speed = 1000;

            for (int j = 0; j < entry.Value.routeList.Count; j++)
            {
                List<string> currentRoute = entry.Value.routeList[j];
                List<Vector3> currentRouteVector3 = new List<Vector3>();
                for (int i = 0; i < currentRoute.Count; i++)
                {
                    if (i == 0)
                    {
                        string linkID = currentRoute[i];
                        string nodeFrom = network.myLinkDic[linkID].from;
                        string nodeTo = network.myLinkDic[linkID].to;
                        Vector3 vectorNode = new Vector3(network.myNodeDic[nodeFrom].x , 0, network.myNodeDic[nodeFrom].y);
                        //carModelScript.wayPoints.Add(vectorNode);
                        currentRouteVector3.Add(vectorNode);
                        Vector3 vectorNode2 = new Vector3(network.myNodeDic[nodeTo].x, 0, network.myNodeDic[nodeTo].y);
                        //carModelScript.wayPoints.Add(vectorNode2);
                        currentRouteVector3.Add(vectorNode2);
                    }
                    else
                    {
                        string linkID = currentRoute[i];
                        string nodeTo = network.myLinkDic[linkID].to;
                        Vector3 vectorNode2 = new Vector3(network.myNodeDic[nodeTo].x, 0, network.myNodeDic[nodeTo].y);
                        //carModelScript.wayPoints.Add(vectorNode2);
                        currentRouteVector3.Add(vectorNode2);
                    }
                }
                for (int h = 0; h < currentRouteVector3.Count; h++)
                {
                    //Debug.Log(currentRouteVector3[h].ToString());
                }
                carModelScript.wayPoints.Add(currentRouteVector3);
                //Debug.Log(carModelScript.wayPoints.Count);
            }
        }
    }

    private void GetAgents(XmlDocument xml)
    {
        XmlNodeList elemList = xml.GetElementsByTagName("person");
        foreach (XmlNode i in elemList)
        {
            Agent1 agent = new Agent1();
            string id = "";
            var attribute = i.Attributes["id"];
            if (attribute != null)
            {
                id = (attribute.Value);
                agent.id = (attribute.Value);
            }


            XmlElement elem = (XmlElement)i;
            XmlNodeList elemList2 = elem.GetElementsByTagName("route");
            int routeCounter = 0;
            foreach(XmlNode j in elemList2)
            {
                XmlNode routeTest = elemList2.Item(routeCounter);
                routeCounter++;
                string test = routeTest.InnerText;
                List<string> oneRouteList = GetRouteListFromXMLwithoutWhiteSpace(test);
                agent.routeList.Add(oneRouteList);
            }
            myAgentDic.Add(id ,agent);
        }
    }

    List<string> GetRouteListFromXMLwithoutWhiteSpace(string routeList)
    {
        // Creating array of string length 
        char[] ch = new char[routeList.Length];

        // Copy character by character into array 
        for (int i = 0; i < routeList.Length; i++)
        {
            ch[i] = routeList[i];
        }

        List<string> stringList = new List<string>();
        string node = "";
        // Printing content of array 
        for (int i = 0; i < ch.Length; i++)
        {
            if (Char.IsWhiteSpace(ch[i]) ==false)
            {
                node = node + ch[i];
            }
            else
            {
                stringList.Add(node);
                node = "";
            }
            if(i== (ch.Length - 1))
            {
                stringList.Add(node);
            }
        }
        return stringList;
    }
}
