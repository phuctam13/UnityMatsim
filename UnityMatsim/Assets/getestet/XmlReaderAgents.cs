using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEditor;
using UnityEngine;

public class XmlReaderAgents : MonoBehaviour
{
    string path;
    public List<Agent> agentList = new List<Agent>();

    public GameObject carModel;
    XmlReaderNetwork  network;

    private void Start()
    {
        network = GameObject.FindObjectOfType<XmlReaderNetwork>();
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
        InstantiateAgents(agentList);
    }

    private void InstantiateAgents(List<Agent> agentList)
    {
        for(int k = 0; k<5000; k++) { 
        for (int i = 0; i < agentList.Count; i++)
        {
            GameObject agent= Instantiate(carModel, new Vector3(0, 0, 0), Quaternion.identity);
            CarModelScript carModelScript = agent.GetComponent<CarModelScript>();
            carModelScript.speed = UnityEngine.Random.Range(5000,10000);

            for (int j= 0; j< agentList[i].routeList.Count; j++)
            {
                if (j == 0)
                {
                    int linkID = agentList[i].routeList[j];
                    int nodeFrom = network.linkList[linkID - 1].from;
                    int nodeTo = network.linkList[linkID - 1].to;
                    Vector3 vectorNode = new Vector3(network.nodeList[nodeFrom-1].x, 0, network.nodeList[nodeFrom-1].y);
                    //carModelScript.wayPoints.Add(vectorNode);
                    Vector3 vectorNode2 = new Vector3(network.nodeList[nodeTo-1].x, 0, network.nodeList[nodeTo-1].y);
                    //carModelScript.wayPoints.Add(vectorNode2);
                }
                else {
                    int linkID = agentList[i].routeList[j];
                    int nodeTo = network.linkList[linkID - 1].to;
                    Vector3 vectorNode2 = new Vector3(network.nodeList[nodeTo - 1].x, 0, network.nodeList[nodeTo - 1].y);
                    //carModelScript.wayPoints.Add(vectorNode2);
                }
            }
        }
        }
    }

    private void GetAgents(XmlDocument xml)
    {
        XmlNodeList elemList = xml.GetElementsByTagName("person");
        for (int i = 0; i < elemList.Count; i++)
        {
            Agent agent = new Agent();
            var attribute = elemList[i].Attributes["id"];
            if (attribute != null)
            {
                agent.id = Int32.Parse(attribute.Value);
            }

            XmlNode  person = elemList[i];
            XmlNode plan = person.FirstChild;
            XmlNode leg = plan.FirstChild.NextSibling;
            XmlNode route = leg.FirstChild;
            var routeList = route.InnerText;
            agent.routeList = StringToIntArray(routeList).ConvertAll<int>(Convert.ToInt32);
            



            agentList.Add(agent);
        }
    }

    List<string> StringToIntArray(string routeList)
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
