using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class NodeLoader : MonoBehaviour
{
    public const string path = "items";

    // Start is called before the first frame update
    void Start()
    {
       
        NodeContainer nodeContainer = NodeContainer.Load(path);

        foreach(Node node in nodeContainer.nodes)
        {
            print(node.id);
        }
    }

}
