using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System;
using System.Text;

public class XmlSave : MonoBehaviour
{
    public Button yourButton;
    GameObject[] nodelist;
    int node_length;
    public static XmlSave ins;
    //public List <node> temp= new List<node>();
    public ItemDB itemDB;
    //public node temp = new node();
    
    
    //List<node> temp = new List<node>;
    

    void Awake()
    {
        ins = this;
    }



    private void Start()
    {
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(SaveItems);

    }

    public void SaveItems()
    {
        nodelist = GameObject.FindGameObjectsWithTag("Node");
        node_length = nodelist.Length;


        for (int i = 0; i < node_length; i++)
        {
            Debug.Log(GameObject.FindGameObjectsWithTag("Node")[i].transform.position);

            node temp = new node();
            temp.name = nodelist[i].name;
            temp.ID = 1;
            temp.Data = "Data";
            temp.loc = (nodelist[i].transform.position);
            Debug.Log(temp.name + "name");
            Debug.Log(itemDB.nodes.Count);

            itemDB.nodes.Add(temp);
            //itemDB.nodes.Add();

            /*
            temp.name = "";
            temp.ID = 0;
            temp.Data = "";
            temp.loc = (Vector3.zero);
            */
        }

        
        



        //Create new xml file
        XmlSerializer serializer = new XmlSerializer(typeof(ItemDB));
        FileStream stream = new FileStream(Application.dataPath + "/StreamingAssets/XML/item_data2.xml", FileMode.Create);
        serializer.Serialize(stream, itemDB);
        stream.Close();
    }

}

[System.Serializable]
public class node
{
    public string name;
    public int ID;
    public string Data;
    public Vector3 loc;
}

[System.Serializable]
public class ItemDB
{
    public List<node> nodes = new List<node>();

}

