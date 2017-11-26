using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
using System.Random;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;

public class WCR : MonoBehaviour {

	// Use this for initialization
	void Start () {
		print("WCR: ");
		WCR_function();
	}

	// Update is called once per frame
	void Update () {
		
	}

	void WCR_function(){
		float c = 1000;
		for (int i = 0; i < 20; i++) {
			print(c);

			Random r = new Random();
			foreach (int index in Enumerable.Range(0, 9).OrderBy(x => r.Next()))
			{
				print(index);
			}

		}
	}
}



/*
1 WCR (G):
inputs: graph G = (V, E)
	output: k-dimensional layout X with n vertices
	2 d{i,j} ← ShortestPaths(G)
	3 X ← RandomM atrix(n, k)
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

*/

/*
Quick python code for foreach {i, j : j < i}, NOT random though

data = "abcdefghijklmnopqrstuvwxyz"
combos = []

	n = 26

	for i in range (0,n):
		for j in range (0,n):
			if i<j:
				combos.append(data[i]+data[j])

				print combos

*/

