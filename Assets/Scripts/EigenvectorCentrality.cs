using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.Collections;

public class EigenvectorCentrality : MonoBehaviour {
	
	public int degree;
	public int ec;
	//GameObject[] connectednodelist;
	/*
	public int ec
	{
		get { return EC; }
		set { EC = value; }
	}

	
		THIS WILL NEED TO BE CALLED BY THE BUTTON PRESSES RATHER THAN BEING ATTACHED TO THE PREFAB AND RUNNING ON UPDATE

		Also needs to be normalised.
*/



	// Use this for initialization
	void Start () {
		degree = 0;
		ec = 0;
		CountCentrality();
		StartCoroutine(Begin());
	}
	
	// Update is called once per frame
	void Update () {
		//CountCentrality();
	}

	void CountCentrality(){
		GameObject[] linklist;
		int link_length;

		linklist = GameObject.FindGameObjectsWithTag("link");

		//Find how many nodes & links there are
		link_length = linklist.Length;

		ec = 0;
		degree = 0;
		for (int i = 0; i < link_length; i++)
			{
			if ((linklist[i].GetComponent<Link>().source == this.gameObject)||linklist[i].GetComponent<Link>().target == this.gameObject)
				{
				degree = degree + 1;

				}

			if ((linklist[i].GetComponent<Link>().source == this.gameObject)||linklist[i].GetComponent<Link>().target == this.gameObject)
			{
				if ((linklist[i].GetComponent<Link>().source != this.gameObject)){

					EigenvectorCentrality eScript = linklist[i].GetComponent<Link>().source.GetComponent<EigenvectorCentrality>();
					print(eScript.degree);
					ec = ec + eScript.degree;


				}

				if ((linklist[i].GetComponent<Link>().target != this.gameObject)){

					EigenvectorCentrality eScript = linklist[i].GetComponent<Link>().target.GetComponent<EigenvectorCentrality>();
					print(eScript.degree);
					ec = ec + eScript.degree;


				}
			}


			}

	}


	IEnumerator Begin()
	{
		yield return new WaitForSeconds(1);
		CountCentrality();
	}
}


