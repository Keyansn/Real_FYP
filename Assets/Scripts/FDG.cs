using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Linq;

public class FDG : MonoBehaviour {
	public bool enabled;
	
	private bool debug = true; // Template ->   if (debug) {print("n: " + n);}
	GameObject[] nodelist;
	GameObject centre;
	public float stiffness = 1;
	public float naturalLength = 1;
	int max;
	int count;
	float x,force;
	public float gravityPull = 20;
	public float CoulombsConstant = 1;

	// Use this for initialization
	void Start () {
		nodelist = GameObject.FindGameObjectsWithTag("Node");
		max = nodelist.Length;
		centre = GameObject.FindGameObjectWithTag("GameController");
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
						Attract(nodelist[i],nodelist[j]);
						Repel(nodelist[i],nodelist[j]);
					
						}
				
					}
			
				}
					
			}
		//Attract();
		//Repel();
	
		}
	}


	void Gravity(GameObject A){
		//A.GetComponent<Rigidbody>().AddForce(Vector3.MoveTowards(A.transform.position,centre.transform.position, Time.deltaTime));
		//A.transform.position = Vector3.MoveTowards(A.transform.position, Vector3.zero, Time.deltaTime * gravityPull);

		Vector3 dirToCentre = -A.transform.position;
		Vector3 impulse = dirToCentre.normalized * gravityPull;
		//A.transform.
		A.GetComponent<Rigidbody>().AddForce(impulse);

		//print("centre.transform.position "+ centre.transform.position);
		//print("A.transform.position "+ A.transform.position);
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
		//B.GetComponent<Rigidbody>().AddForce(Vector3.MoveTowards(B.transform.position,A.transform.positi on,force*Time.deltaTime));
		print("Attractive Force:" + force);
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
		//force = (CoulombsConstant * A.GetComponent<EigenvectorCentrality>().degree * B.GetComponent<EigenvectorCentrality>().degree)/x;
		force = (-CoulombsConstant)/x;
		Vector3 direction = A.transform.position - B.transform.position;
		A.GetComponent<Rigidbody>().AddForce(direction.normalized*-force);
		B.GetComponent<Rigidbody>().AddForce(direction.normalized*force);
		print("Repulsive Force:" + force);

	}
}
