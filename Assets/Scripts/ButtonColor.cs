using UnityEngine; 
using UnityEngine.UI;
using UnityEditor;
using System.Collections.Generic;

public class ButtonColor : MonoBehaviour {
	/*
public Button yourButton; 
public IDictionary<GameObject,List<GameObject>> dict = new Dictionary<GameObject,List<GameObject>>(); 
public GameObject[] nodelist,linklist; 
public bool debug = true;
public Material[] materials;//Allows input of material colors in a set size of array;
public Renderer Rend; //What are we rendering? Input object(Sphere,Cylinder,...) to render.
public int index = 1;

void Start () {
	Button btn = yourButton.GetComponent<Button>(); 
	btn.onClick.AddListener(Refresh);
	nodelist = GameObject.FindGameObjectsWithTag("Node");
	linklist = GameObject.FindGameObjectsWithTag("link");
	Rend = GetComponent<Renderer>();//Gives functionality for the renderer
	Rend.enabled = true;//Makes the rendered 3d object visable if enabled;

}

// Update is called once per frame
void Update () {

}

void ColorAll () {
	index += 1;//When mouse is pressed down we increment up to the next index location

	if (index == materials.Length + 1)//When it reaches the end of the materials it starts over.
	index = 1;

	print(index);//used for debugging  

	Rend.sharedMaterial = materials[index - 1]; //This sets the material color values inside the index 
}

void Refresh()
	{ 
		List<GameObject> adjacentlist = new List<GameObject>();
		if ((this.GetComponent<Link>().source == Node) || this.GetComponent<Link>().target == Node) { 
			if ((this.GetComponent<Link>().source != Node)) { 
				adjacentlist.Add(new GameObject() { this.GetComponent<Link>().source });
			}
			if ((this.GetComponent<Link>().target != Node)) {
				adjacentlist.Add(this.GetComponent<Link>().target);
			}
		}


	}*/
}