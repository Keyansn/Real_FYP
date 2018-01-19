using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Linq;
using NUnit.Framework;
using UnityEngine.UI;
using UnityEditor;

public class FDG : MonoBehaviour {
	public bool enabled;
	public Button yourButton;
	private bool debug = true; // Template ->   if (debug) {print("n: " + n);}
	GameObject[] nodelist,linklist;
	GameObject centre;
	public float stiffness = 1;
	public float naturalLength = 1;
	int max;
	int count;
	float x,force;
	public float gravityPull = 20;
	public float CoulombsConstant = 100;
	private float sphRadius;
	private float sphRadiusSqr;

	// Use this for initialization
	void Start () {
		UpdateLists();
		Button btn = yourButton.GetComponent<Button>();
		btn.onClick.AddListener(UpdateLists);
	}

	void UpdateLists(){
		nodelist = GameObject.FindGameObjectsWithTag("Node");
		linklist = GameObject.FindGameObjectsWithTag("link");
		max = nodelist.Length;

	}
	
	// Update is called once per frame
	void Update () {
		if (enabled==true) {
			count = count + 1;
			if (count>80) {
				for (int i = 0; i < max; i++) {
					Gravity(nodelist[i]);
					for (int j = 0; j < max; j++) {
						if (i > j) {
							//Attract(nodelist[i],nodelist[j]);
							////////////////////////Repel(nodelist[i],nodelist[j]);					
						}				
					}			
				}
				foreach (GameObject list in linklist) {
					Attract(list.GetComponent<Link>().source, list.GetComponent<Link>().target);
				}					
			}
		}
	}


	void Gravity(GameObject A){
		Vector3 dirToCentre = -A.transform.position;
		Vector3 impulse = dirToCentre.normalized * gravityPull;
		A.GetComponent<Rigidbody>().AddForce(impulse);
	}


	void Attract(GameObject A, GameObject B){
		// f = k * x
		// f = force, k = stiffness, x = extension	x = distance - set length
		x = Vector3.Distance(A.transform.position, B.transform.position) - naturalLength;
		force = stiffness * x ;

		Vector3 direction = A.transform.position - B.transform.position;
		float distsqr = direction.sqrMagnitude;

		A.GetComponent<Rigidbody>().AddForce(direction.normalized*-force);
		B.GetComponent<Rigidbody>().AddForce(direction.normalized*force);
		//print("Attractive Force:" + force);
	}

	void Repel(GameObject A, GameObject B){
		/*
		 * 	    k * q1 * q2
		 * F = ------------
		 * 		    d^2
		 * 
		 *  f = force, k = Coulomb's constant, q1 & q2 respective charges, d = distance between nodes
		*/
		x = Vector3.Distance(A.transform.position, B.transform.position);
		x = x * x;
		force = (-CoulombsConstant)/x;
		Vector3 direction = A.transform.position - B.transform.position;
		A.GetComponent<Rigidbody>().AddForce(direction.normalized*-force);
		B.GetComponent<Rigidbody>().AddForce(direction.normalized*force);
		print("Repulsive Force:" + force);

	}

}
