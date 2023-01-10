using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

[XmlRoot("NodeCollection")]
public class NodeContainer : MonoBehaviour
{
    [XmlArray("Nodes")]
    [XmlArrayItem("Node")]
    public List<Node> nodes = new List<Node>();
   
    public static NodeContainer Load(string path)
    {
        TextAsset _xml = Resources.Load<TextAsset>(path);

        XmlSerializer serializer = new XmlSerializer(typeof(NodeContainer));

        StringReader reader = new StringReader(_xml.text);

        NodeContainer nodes = serializer.Deserialize(reader) as NodeContainer;

        reader.Close();

        return nodes;

    }
}
