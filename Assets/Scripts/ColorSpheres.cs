using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ColorSpheres : MonoBehaviour {

    public Material[] materials;//Allows input of material colors in a set size of array;
    public Renderer Rend; //What are we rendering? Input object(Sphere,Cylinder,...) to render.
    GameObject[] nodelist;
    int node_length;

    private int index = 1;//Initialize at 1, otherwise you have to press the ball twice to change colors at first.

    // Use this for initialization
    void Start()
    {
        Rend = GetComponent<Renderer>();//Gives functionality for the renderer
        Rend.enabled = true;//Makes the rendered 3d object visable if enabled;
    }

    void OnMouseDown()
    {

        if (materials.Length == 0)//If there are no materials nothing happens.
            return;

        //Debugging mouse presses
        //works
        if (Input.GetMouseButton(0))
            Debug.Log("Pressed left click.");

        //doesn't work, no idea why
        if (Input.GetMouseButton(1))
            Debug.Log("Pressed right click.");

        if (Input.GetMouseButton(2))
            Debug.Log("Pressed middle click.");


        if (Input.GetMouseButtonDown(0))
        {
            index += 1;//When mouse is pressed down we increment up to the next index location

            if (index == materials.Length + 1)//When it reaches the end of the materials it starts over.
                index = 1;

            //Increase size
            //transform.localScale += new Vector3(0.1F, 0.1F, 0.1F);

            print(index);//used for debugging

            Rend.sharedMaterial = materials[index - 1]; //This sets the material color values inside the index

            nodelist = GameObject.FindGameObjectsWithTag("Node");
            node_length = nodelist.Length;

            for (int i = 0; i < node_length; i++)
            {
                Debug.Log(GameObject.FindGameObjectsWithTag("Node")[i].transform.position);
            }

            


            /*
            Object prefab = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Cube.prefab", typeof(GameObject));
            GameObject clone = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
            clone.transform.position = Vector3.one;
            */
        }
    }




        public string id;
        public TextMesh nodeText;

        void Update()
        {
            //node text always facing camera
            nodeText.transform.LookAt(Camera.main.transform);
            nodeText.transform.Rotate(0,180F,0);
        

    }
    }

