using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.Collections;

public class ButtonLines : MonoBehaviour
{
    public Button yourButton;
    GameObject[] nodelist;
    GameObject[] linklist;
    int node_length, link_length;
    int num1, num2;
    int no_possible;

    void Start()
    {
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        Debug.Log("You have clicked the lines button!");

        //look for all with tag "Node" & "link" (capitalisation is an error but oh well...)
        nodelist = GameObject.FindGameObjectsWithTag("Node");
        linklist = GameObject.FindGameObjectsWithTag("link");

        //Find how many nodes & links there are
        node_length = nodelist.Length;
        link_length = linklist.Length;

        //Generate two random within range that are different
        no_possible = 0;
        do
        {
            num1 = Random.Range(0, node_length);
            num2 = Random.Range(0, node_length);
            print("Random numbers are: " + num1 + " , " + num2);
            no_possible++;

            for (int i = 0; i < link_length; i++)
            {
                if ((linklist[i].GetComponent<Link>().source == nodelist[num1] && linklist[i].GetComponent<Link>().target == nodelist[num2])|| (linklist[i].GetComponent<Link>().target == nodelist[num1] && linklist[i].GetComponent<Link>().source == nodelist[num2]))
                {
                    print("Redo randomiser");
                    num1 = num2;
                }

            }

             if (no_possible >= (node_length*node_length))
             {
                 num1 = 0;
                 num2 = 1;
                no_possible = -1;
                print("TOO MANY NODES!");
             }

        } while (num1==num2);

        //Sets the source and target nodes for the connection
         if (no_possible != -1)
         {
            //Create link from the prefab
            Object prefab = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Link.prefab", typeof(GameObject));
            GameObject clone = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
            clone.GetComponent<Link>().source = nodelist[num1];
            clone.GetComponent<Link>().target = nodelist[num2];
         }

    }
}