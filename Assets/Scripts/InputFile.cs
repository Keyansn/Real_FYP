﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Xml;

public class InputFile : MonoBehaviour
{
    //the xml file attached
    public TextAsset xmlRawFile;

    void Start ()
    {
        string data = xmlRawFile.text;
        LoadInputFile(data);
    }

    void LoadInputFile(string xmlFile)
    {

        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(new StringReader(xmlFile));

        //Go through the node section of the Links.xml file
        string xmlPathNodes = "//nodes/node";
        XmlNodeList nodeList = xmlDoc.SelectNodes (xmlPathNodes);
        foreach(XmlNode node in nodeList)
        {
            /*  _name is the name of the node
             *  tbd
             *  tbd
             */
            XmlNode _name = node.FirstChild;
            XmlNode id= _name.NextSibling;
            XmlNode data = id.NextSibling;

            //Create nodes from the prefab
            Object prefab = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Node1.prefab", typeof(GameObject));
            GameObject clone = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;

            //Move the new cloned prefab to random location 
            clone.transform.position = new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f));

            //Name the cloned prefab (for hierarchy view)
            clone.name = _name.InnerXml;

            //This has an error but I don't understand why (it works fine in Unity)
            //UPDATE^: error gone after infinite loop crash
            //Sets the TextMesh to read the _name from the xml file
            clone.GetComponentInChildren<SetText>().name = _name.InnerXml;
            

        }


        //Go through the link section of the Links.xml file
        string xmlPathLinks = "//nodes/link";
        XmlNodeList nodeListLinks = xmlDoc.SelectNodes(xmlPathLinks);
        foreach (XmlNode node in nodeListLinks)
        {
            /*  _source is where the link is coming from
             *  _target is where the link is going 
             *  _direction is true if direction matters, otherwise false
             */
            XmlNode _source = node.FirstChild;          
            XmlNode _target = _source.NextSibling;
            XmlNode _direction = _target.NextSibling;

            print(_source.InnerXml + " " + _target.InnerXml );

            //Create link from the prefab
            Object prefab = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Link.prefab", typeof(GameObject));
            GameObject clone = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;

            //Sets the source and target nodes for the connection
            clone.GetComponent<Link>().source = GameObject.Find(_source.InnerXml);
            clone.GetComponent<Link>().target = GameObject.Find(_target.InnerXml);

            //Checks if link has a direction asssigned to it
            if (_direction.InnerXml == " True ")
            {
                clone.GetComponent<Link>().direction = true;
            }
            else
            {
                clone.GetComponent<Link>().direction = false;
            }
            




        }






    }

}