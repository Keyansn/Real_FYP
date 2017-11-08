using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SetText : MonoBehaviour {

    public string name;

	// Use this for initialization
	void Start () {
        GetComponent<TextMesh>().text = name;
    }
	

}
