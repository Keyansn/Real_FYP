using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalScalers : MonoBehaviour {


	private GameObject[] linkList;
	private int listLength;
	public float largestLink;

	// Use this for initialization
	void Start () {
		

		//find largest link
		linkList = GameObject.FindGameObjectsWithTag("link");
		listLength = linkList.Length;

		for (int i = 0; i < listLength; i++){

			if (linkList[i].GetComponent<Link>().weight > largestLink){
				largestLink = linkList[i].GetComponent<Link>().weight;
			}

		} 

		print("largestLink is: " + largestLink);

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
