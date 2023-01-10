using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using UnityEngine.UI;
using System.IO;
using UnityEditor;
using System.Text;

public class NodeLoader2 : MonoBehaviour
{
    public TextAsset xmlRawFile;
    // Start is called before the first frame update
    void Start()
    {
        string data = xmlRawFile.text;
        parseXmlFile(data);
    }

    void parseXmlFile(string xmlData)
    {
        string totval = "";
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(new StringReader(xmlData));

        string xmlPathPattern = "//Network/Nodes";
        XmlNodeList myNodeList = xmlDoc.SelectNodes(xmlPathPattern);
        foreach(XmlNode node in myNodeList)
        {
            XmlNode x = node.FirstChild;
            XmlNode y = x.NextSibling;

            totval = "x:" + x.InnerXml + " y:" + y.InnerXml;
            Debug.Log(totval);
        }
        
    }

}
