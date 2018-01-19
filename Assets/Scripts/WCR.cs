using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;


public class WCR : MonoBehaviour {

	public bool enabled;
	private bool debug = true; // Template ->   if (debug) {print("n: " + n);}
	float weight_ij;
	float dist_ij;
	float weight;
	float moveX,moveY,moveZ;
	float c = 1000;
	float xi, yi, zi, xj, yj, zj;
	Vector3 currentPositioni = Vector3.zero;
	Vector3 currentPositionj = Vector3.zero;

	// Use this for initialization
	void Start () {
		
		if (enabled) {WCR_function();}

	}

	// Update is called once per frame
	void Update () {

	}

	void WCR_function(){
		GameObject[] nodelist;
		int node_length;

		nodelist = GameObject.FindGameObjectsWithTag("Node");
		node_length = nodelist.Length;

		foreach (GameObject item in nodelist) {
			if (debug) {
				print(item.transform.position);
				//item.transform.position = new Vector3(Random.value, Random.value, Random.value);
			}
		}



		for (int n = 0; n < 20; n++) {
			if (!debug) {print("n: " + n);}

			for (int i = 0; i < node_length; i++) {
				for (int j = 0; j < node_length; j++) {
					if (j < i) {


						Dijkstra eScript = GameObject.FindGameObjectWithTag("GameController").GetComponent<Dijkstra>();
						dist_ij = 5*eScript.Calculate(nodelist[i], nodelist[j], false);

						if (debug) {print("dist_ij: " + dist_ij);}

						weight_ij = (float)Mathf.Pow(dist_ij, -2);
						weight = weight_ij * c;

						if (weight > 2) {weight = 2;}

						if (debug) {print("weight: " + weight);}

						if (debug) {print("weight_ij: " + weight_ij);}

						//        dist_ij - ||Xi-Xj||    Xi-Xj
						// move = -------------------   -------
						// 		 		   2           ||Xi-Xj||

						// Separate out x,y and z
						xi = nodelist[i].transform.position.x;
						xj = nodelist[j].transform.position.x;
						yi = nodelist[i].transform.position.y;
						yj = nodelist[j].transform.position.y;
						zi = nodelist[i].transform.position.z;
						zj = nodelist[j].transform.position.z;

						if (!debug) {
							print("xi: " + xi);
						}

						//moveX = (float)((0.5)*(dist_ij - Mathf.Abs(xi-xj)) * ((xi-xj)/Mathf.Abs(xi-xj)));
						//could use vector3.Distance

						moveX = move(dist_ij, xi, xj);
						moveY = move(dist_ij, yi, yj);
						moveZ = move(dist_ij, zi, zj);

						if (!debug) {
							print("moveX: " + moveX);
							print("moveY: " + moveY);
							print("moveZ: " + moveZ);
						}

						currentPositioni.Set(nodelist[i].transform.position.x, nodelist[i].transform.position.y, nodelist[i].transform.position.z);
						currentPositionj.Set(nodelist[j].transform.position.x, nodelist[j].transform.position.y, nodelist[j].transform.position.z);

						nodelist[i].transform.position = new Vector3(currentPositioni.x + (weight * moveX), currentPositioni.y + (weight * moveY), currentPositioni.z + (weight * moveZ));
						//print("Test:");
						//print(currentPositioni.x + (weight * moveX));

						nodelist[j].transform.position = new Vector3(currentPositionj.x - (weight * moveX), currentPositionj.y - (weight * moveY), currentPositionj.z - (weight * moveZ));

						c = c / 2;
					}

				}
			}
		}
	}

	float move(float dist, float i, float j){
		float abs = (float)Mathf.Abs(i - j);
		return (0.5f)*(dist - abs) * ((i-j)/abs);
	}
}



/*
1 WCR (G):
inputs: graph G = (V, E)
	output: k-dimensional layout X with n vertices
	2 d{i,j} ← ShortestPaths(G)
	3 X ← Random Matrix(n, k)
	4 c ← 1000
	5 for 20 iterations :
		6 foreach {i, j : j < i} in random order :
			7 ω ← wij c
			8 if ω > 2 :
				9 ω ← 2
				10 m ←
				dij−||Xi−Xj ||
				/2
				Xi−Xj
				/||Xi−Xj ||
				11 Xi ← Xi + ω m
				12 Xj ← Xj − ω m
				13 c ← c/2



Wij = (Dij)^-2
*/