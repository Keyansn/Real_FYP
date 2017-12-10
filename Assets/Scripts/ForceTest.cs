using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.Collections;

public class ForceTest : MonoBehaviour {
	GameObject[] nodes;
	public Button button;
	int nodeCount;

	public float grav = 0.48f;
	// Use this for initialization
	void Start () {
		Button btn = button.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
		nodes = GameObject.FindGameObjectsWithTag("Test");
		nodeCount = nodes.Length;
	}
	
	// Update is called once per frame
	void Update () {

		for (int i = 0; i < nodeCount; i++) {
			for (int j = 0; j < nodeCount; j++) {

				if(i<j){
				
					//nodes[i].transform.position = Vector3.MoveTowards(nodes[i].transform.position, Vector3.one, Time.deltaTime * grav);
					//nodes[j].transform.position = Vector3.MoveTowards(nodes[j].transform.position, Vector3.one, Time.deltaTime * grav);

					Attract(nodes[i],nodes[j]);
					//Repel(nodes[i],nodes[j]);
				}

			}

		}

		/*
		nodes[0].transform.position = Vector3.MoveTowards(nodes[0].transform.position, Vector3.one, Time.deltaTime * grav);
		nodes[1].transform.position = Vector3.MoveTowards(nodes[1].transform.position, Vector3.one, Time.deltaTime * grav);

		Attract(nodes[0],nodes[1]);
		Repel(nodes[0],nodes[1]);
*/

	}

	void TaskOnClick()
	{
		Debug.Log("You have clicked the button!");
		Object prefab = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Sphere.prefab", typeof(GameObject));
		GameObject clone = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;

		//Move the new cloned prefab to random location 
		clone.transform.position = new Vector3(Random.Range(-10.0f, 10.0f), 1, Random.Range(-10.0f, 10.0f));
		nodes = GameObject.FindGameObjectsWithTag("Test");
		nodeCount = nodes.Length;
	}

	void Attract(GameObject A, GameObject B){
		// f = k * x
		// f = force, k = stiffness, x = extension	x = distance - set length

		float x;
		float naturalLength = 1f;
		float force;
		float stiffness=2f;

		x = Vector3.Distance(A.transform.position, B.transform.position) - naturalLength;
		force = stiffness * x ;
		if (force>1){
			force = 1;
		}


		A.GetComponent<Rigidbody>().AddForce(Vector3.MoveTowards(A.transform.position,B.transform.position,force));
		B.GetComponent<Rigidbody>().AddForce(Vector3.MoveTowards(B.transform.position,A.transform.position,force));
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

		float x;
		float force;

		float CoulombsConstant =-8f;

		x = Vector3.Distance(A.transform.position, B.transform.position);
		x = x * x;
		force = (CoulombsConstant)/x;

		if (force>1){
			force = 1;
		}

		print("Repulsive Force:" + force);
		A.GetComponent<Rigidbody>().AddForce(Vector3.MoveTowards(A.transform.position,B.transform.position,force));
		B.GetComponent<Rigidbody>().AddForce(Vector3.MoveTowards(B.transform.position,A.transform.position,force));

	}

}
