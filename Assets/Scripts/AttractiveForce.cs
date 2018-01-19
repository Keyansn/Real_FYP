using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class AttractiveForce : MonoBehaviour { 
	public IDictionary<GameObject,List<GameObject>> dict = new Dictionary<GameObject,List<GameObject>>(); 
	public GameObject[] nodelist,linklist; 
	public bool debug = false;
	public float stiffness = 1;
	public float naturalLength = 1;
	float x,force;

	// Use this for initialization
	void Start () {
		nodelist = GameObject.FindGameObjectsWithTag("Node");
		linklist = GameObject.FindGameObjectsWithTag("link");
		Refresh();
	}
	
	// Update is called once per frame
	void Update () {
		/*
		foreach (var item in nodelist) {
			foreach (var subitem in dict[item]) {
				Attract(item, subitem);
			}
		}
		*/
	}

	void Refresh (){
		dict.Clear(); 
		foreach (GameObject item in nodelist) {
			dict.Add(item, ConnectedList(item));
		}

		if (debug) {
			foreach (var item in nodelist) {
				foreach (var subitem in dict[item]) {
					print("sub: " + subitem);

				}
			}
		}
	}

	void Attract(GameObject A, GameObject B){
		// f = k * x
		// f = force, k = stiffness, x = extension	x = distance - set length
		x = Vector3.Distance(A.transform.position, B.transform.position) - naturalLength;
		force = stiffness * x ;

		Vector3 direction = A.transform.position - B.transform.position;
		float distsqr = direction.sqrMagnitude;

		A.GetComponent<Rigidbody>().AddForce(direction.normalized*-force);
		//B.GetComponent<Rigidbody>().AddForce(direction.normalized*force);
		//print("Attractive Force:" + force);
	}

		
	List<GameObject> ConnectedList (GameObject Node){ 

		List<GameObject> tempList = new List<GameObject>();

		foreach (GameObject item in linklist) {
			if ((item.GetComponent<Link>().source == Node)||item.GetComponent<Link>().target == Node){
				if ((item.GetComponent<Link>().source != Node)){
					tempList.Add(item.GetComponent<Link>().source);
				}
				if ((item.GetComponent<Link>().target != Node)){ 
					tempList.Add(item.GetComponent<Link>().target); 
				}
			}
		} 

		//print("Con_Node: " + Node.name);

		foreach (var item in tempList) {
			//print(item.name);
		}
		return tempList;
	}
}

/*
public class SerializableDictionary<TK,TV> :ISerializationCallbackReceiver{
	private Dictionary<TK, TV> _Dictionary;
	[SerializeField] List<TK> _Keys;
	[SerializeField] List<TV> _Values;
}
*/