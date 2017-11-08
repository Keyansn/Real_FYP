﻿using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.Collections;

public class ButtonExample : MonoBehaviour
{
    public Button yourButton;

    void Start()
    {
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        Debug.Log("You have clicked the button!");
        Object prefab = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Node1.prefab", typeof(GameObject));
        GameObject clone = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;

        //Move the new cloned prefab to random location 
        clone.transform.position = new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f));
    }
}