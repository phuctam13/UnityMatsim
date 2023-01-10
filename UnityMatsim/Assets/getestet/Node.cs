using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;

public class Node : MonoBehaviour
{
    [XmlAttribute("id")]
    public int id;
    [XmlElement("x")]
    public float x;
    [XmlElement("y")]
    public float y;
}
