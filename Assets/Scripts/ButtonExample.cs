using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.Collections;

public class ButtonExample : MonoBehaviour
{
    public Button yourButton;
	public Text txt;
	GameObject[] nodelist;
	int node_length;

    void Start()
    {
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
		CountNodes ();

    }

    void TaskOnClick()
    {
        Debug.Log("You have clicked the button!");
        Object prefab = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Node1.prefab", typeof(GameObject));
        GameObject clone = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;

        //Move the new cloned prefab to random location 
        clone.transform.position = new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f));
		CountNodes ();
    }

	void CountNodes()
	{
		nodelist = GameObject.FindGameObjectsWithTag("Node");
		node_length = nodelist.Length;

		txt.text = "Number of nodes: " + node_length.ToString();
	}

}