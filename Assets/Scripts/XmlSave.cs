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
	GameObject[] linklist;
	int link_length;

    public static XmlSave ins;
    //public List <node> temp= new List<node>();
    public ItemDB itemDB;
    //public node temp = new node();
	public string Filename = "save_data";
    
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
		itemDB.nodes.Clear();
        nodelist = GameObject.FindGameObjectsWithTag("Node");
        node_length = nodelist.Length;
		linklist = GameObject.FindGameObjectsWithTag("link");
		link_length = linklist.Length;

        for (int i = 0; i < node_length; i++)
        {
            Debug.Log(GameObject.FindGameObjectsWithTag("Node")[i].transform.position);

            node temp = new node();
            temp.name = nodelist[i].name;
            temp.ID = 1;
            temp.data = "data";
			temp.x = (nodelist[i].transform.position.x);
			temp.y = (nodelist[i].transform.position.y);
			temp.z = (nodelist[i].transform.position.z);
            Debug.Log(temp.name + "name");
            Debug.Log(itemDB.nodes.Count);

            itemDB.nodes.Add(temp);
           
        }

		for (int i = 0; i < link_length; i++) 
		{
			link templink = new link();
			templink.source = linklist[i].transform.GetComponent<Link>().source.name.ToString();	//Need to use .name otherwise it says (UnityEngine.GameObject) after the name
			templink.target = linklist[i].transform.GetComponent<Link>().target.name.ToString();
			templink.direction = linklist[i].transform.GetComponent<Link>().direction.ToString();
			itemDB.links.Add(templink);
		}

        
        

		print("Starting file write");

        //Create new xml file
        XmlSerializer serializer = new XmlSerializer(typeof(ItemDB));
		FileStream stream = new FileStream(Application.dataPath + "/StreamingAssets/XML/" + Filename + ".xml", FileMode.Create);
        serializer.Serialize(stream, itemDB);
        stream.Close();

		print("Finished file write");
    }

}

[System.Serializable]
public class node
{
    public string name;
    public int ID;
    public string data;
	public float x;
	public float y;
	public float z;
}

[System.Serializable]
public class link
{
	public string source;
	public string target;
	public string direction;
}

[System.Serializable]
public class ItemDB
{
    public List<node> nodes = new List<node>();
	public List<link> links = new List<link>();

}

