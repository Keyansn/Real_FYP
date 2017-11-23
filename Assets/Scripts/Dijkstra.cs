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

	GameObject StartNode;
	GameObject EndNode;
	GameObject[] Nodes;
	//GameObject[] AvailableNodes;
	GameObject[] linklist;
	int link_length;
	public List<GameObject> AvailableLinks = new List<GameObject>();
	public List<GameObject> nodes = new List<GameObject>();
	public List<GameObject> NodesVisited = new List<GameObject>();
	public List<GameObject> path = new List<GameObject>();
	int i;
	private GameObject smallest;
	private GameObject test;


	      
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

				Calculate();
			}
			i = 1;
		} 
	}

	public void Calculate(){
		
		linklist = GameObject.FindGameObjectsWithTag("link");
		StartNode = getParent("Start");
		EndNode = getParent("End");

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

			print("smallest " + smallest);

			//smallest = getSmallest(nodes);

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
						print("Ding!");
						foreach (KeyValuePair<GameObject, GameObject> item in previous) {
							print("prev: " + item.Key + item.Value);
						}
					}
				}

				if ((link.GetComponent<Link>().target == smallest))
				{
					if (link.GetComponent<Link>().weight+ distance[link.GetComponent<Link>().target]< distance[link.GetComponent<Link>().source])
					{
						distance[link.GetComponent<Link>().source] = link.GetComponent<Link>().weight+ distance[link.GetComponent<Link>().target];
						previous[link.GetComponent<Link>().source] = link.GetComponent<Link>().target;
						print("Ding!");
						foreach (KeyValuePair<GameObject, GameObject> item in previous) {
							print("prev: " + item.Key + item.Value);
						}
					}
				}
			}
				
			NodesVisited.Add(smallest);



			print("Looped");

			foreach (KeyValuePair<GameObject, GameObject> item in previous) {
				print("prev: " + item.Key + item.Value);
			}

		}

		print("Bingo");

		foreach (KeyValuePair<GameObject, float> item in distance) {
			print("Dict: " + item.Key + item.Value);
		}

		foreach (KeyValuePair<GameObject, GameObject> item in previous) {
			print("prev: " + item.Key + item.Value);
		}


		//StartNode,EndNode
		GameObject dist = EndNode;
		path.Add(EndNode);

		while (dist != StartNode){
			dist = previous[dist];
			path.Add(dist);
		}
		path.Reverse();

		foreach (GameObject item in path) {
			print(item);
		}


//		nodes.Sort((x, y) => costs[x] - costs[y]);
//
//		foreach (GameObject thing in nodes){
//			print(thing);
//		}

















		linklist = GameObject.FindGameObjectsWithTag("link");
		//link_length = linklist.Length;

		foreach (GameObject link in linklist) {
			if ((link.GetComponent<Link>().source == StartNode)){
				AvailableLinks.Add(link);
			}
			if (link.GetComponent<Link>().target == StartNode) {
				AvailableLinks.Add(link);
			}
		}

		test = getSmallest(AvailableLinks);
		print(test.GetComponent<Link>().weight);

		//print(getSmallest(AvailableLinks).GetComponent<Link>().weight);

		print(StartNode + " " + EndNode);

		foreach (GameObject thing in AvailableLinks){
			print(thing);
		}

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
