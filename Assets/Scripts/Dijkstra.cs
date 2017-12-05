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
using NUnit.Framework;

public class Dijkstra : MonoBehaviour {

	GameObject _Start;
	GameObject _End;
	GameObject[] Nodes;
	//GameObject[] AvailableNodes;
	GameObject[] linklist;
	int link_length;
	int i;
	private GameObject smallest;
	private GameObject test;
	bool debug = false; 


	      
	// Use this for initialization
	void Start () {
		i = 0;

	}
	
	// Update is called once per frame
	void Update () {
		/*
		 * We need to check if Start and End Selected
		 * Also don't want to be recalculating over and over
		 * 	-> Need a public variable which can be reset
		 */

		if((GameObject.FindGameObjectWithTag("Start"))&(GameObject.FindGameObjectWithTag("End"))){
			//print("Start and End Found");
			if (i == 0){
				_Start = getParent("Start");
				_End = getParent("End");
				Calculate(_Start,_End,true);
			}
			i = 1;
		} 
	}

	public int Calculate(GameObject StartNode, GameObject EndNode, bool highlight){
		
		linklist = GameObject.FindGameObjectsWithTag("link");
		List<GameObject> AvailableLinks = new List<GameObject>();
		List<GameObject> nodes = new List<GameObject>();
		List<GameObject> NodesVisited = new List<GameObject>();
		List<GameObject> path = new List<GameObject>();


		nodes.AddRange(GameObject.FindGameObjectsWithTag("Node"));
		IDictionary<GameObject, GameObject> previous =  new Dictionary<GameObject, GameObject>();
		IDictionary<GameObject, float> distance = new Dictionary<GameObject, float>();


		foreach (GameObject item in nodes) {
			distance.Add(item, float.MaxValue);
		}

		distance[StartNode] = 0; 

		while (nodes.Count != 0){

			/*
			 *	I need to sort here otherwise it will detatch and spew garbage 
			 */

			GameObject smallest = nodes[0];

			float tempweight = float.MaxValue;

			foreach (GameObject item in nodes) {
				if (distance[item] < tempweight){
					tempweight = distance[item];
					smallest = item;
				}
			}

			nodes.Remove(smallest);

			if (smallest == EndNode){
				break;
			}

			AvailableLinks.Clear();

			foreach (GameObject link in linklist) 
			{
				if ((link.GetComponent<Link>().source == smallest)||(link.GetComponent<Link>().target == smallest))
				{
					AvailableLinks.Add(link);
				}
			}


			foreach (GameObject link in AvailableLinks)
			{
				if ((link.GetComponent<Link>().source == smallest))
				{
					if (link.GetComponent<Link>().weight + distance[link.GetComponent<Link>().source] < distance[link.GetComponent<Link>().target])
					{
						distance[link.GetComponent<Link>().target] = link.GetComponent<Link>().weight+ distance[link.GetComponent<Link>().source];
						previous[link.GetComponent<Link>().target] = link.GetComponent<Link>().source;
						//print("Ding!");

					}
				}

				if ((link.GetComponent<Link>().target == smallest))
				{
					if (link.GetComponent<Link>().weight+ distance[link.GetComponent<Link>().target]< distance[link.GetComponent<Link>().source])
					{
						distance[link.GetComponent<Link>().source] = link.GetComponent<Link>().weight+ distance[link.GetComponent<Link>().target];
						previous[link.GetComponent<Link>().source] = link.GetComponent<Link>().target;
						//print("Ding!");

					}
				}
			}
				
			NodesVisited.Add(smallest);

		}

		//StartNode,EndNode
		GameObject dist = EndNode;
		path.Add(EndNode);

		while (dist != StartNode){
			dist = previous[dist];
			path.Add(dist);
		}
		//path.Reverse();

		//get all links in paths and change colour

		if (highlight) {

			for (int n = 0; n < path.Count - 1; n++) {
				foreach (GameObject item in linklist) {
					if (((item.GetComponent<Link>().source == path[n]) & (item.GetComponent<Link>().target == path[n + 1])) || ((item.GetComponent<Link>().target == path[n]) & (item.GetComponent<Link>().source == path[n + 1]))) {
						item.GetComponent<Link>().colorStart = Color.yellow;
						item.GetComponent<Link>().colorEnd = Color.yellow;
						//print("Changed colour");
						//print(path[n] + " to " + path[n + 1]);
						item.GetComponent<Link>().ChangeColour();
					}
				}
			}
		}



		nodes.Clear();
		AvailableLinks.Clear();
		NodesVisited.Clear();

		int pathlength = path.Count - 1;
		path.Clear();

		return pathlength;


	}

	GameObject getParent(string Tag){
		return GameObject.FindGameObjectWithTag(Tag).transform.parent.gameObject;
	}

	GameObject getSmallest(List<GameObject> list){
		
		float weight = float.MaxValue;

		smallest = list[0];

		foreach (GameObject item in list) {
			if (item.GetComponent<Link>().weight < weight){
				weight = item.GetComponent<Link>().weight;
				smallest = item;
			}
		}

		return smallest;
	}


}
