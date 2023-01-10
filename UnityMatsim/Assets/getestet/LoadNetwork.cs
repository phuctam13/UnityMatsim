using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using System.Xml;
using UnityEngine.UI;
using System.IO;

public class LoadNetwork : MonoBehaviour
{
    string path;
    public TextAsset xmlRawFile;
    public Text uiText;

    public void OpenFileExplorer()
    {
        path = EditorUtility.OpenFilePanel("Show all XML-Files (.xml)", "", "xml");
        GetXml();
    }

    void GetXml()
    {
        XmlDocument xml = new XmlDocument();
        xml.Load(path);
        string data = xml.InnerText;
        parseXmlFile(data);
        
    }

    
    void parseXmlFile(string xmlData)
    {
        string totVal = "";
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(new StringReader(xmlData));

        string xmlPathPattern = "//network name//nodes";
        XmlNodeList myNodeList = xmlDoc.SelectNodes(xmlPathPattern);

        foreach(XmlNode node in myNodeList)
        {
            XmlNode name = node.FirstChild;
            XmlNode addr = node.NextSibling;

            totVal += "Name:" + name.InnerXml + "\n Address:" + addr.InnerXml +"\n\n";
            uiText.text = totVal;
        }

    }

    public void loadXmlNetwork()
    {

    }


    /*
public void LoadButton()
{
    LoadXmlNetwork();
}

private void LoadXmlNetwork()
{

}
*/

    /*
    string path;
    public RawImage rawImage;

    public void OpenFileExplorer()
    {
        path = EditorUtility.OpenFilePanel("Show all XML-Files (.png)", "", "png");
        StartCoroutine(GetXml());
    }

    IEnumerator GetXml()
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture("file:///" + path);

        yield return www.SendWebRequest();

        if(www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Texture myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            rawImage.texture = myTexture;
        }
    }

    */
}
