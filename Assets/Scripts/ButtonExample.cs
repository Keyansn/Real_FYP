using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.Collections;

public class ButtonExample : MonoBehaviour
{
    public Button yourButton;
	public Text txtnodes;
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
//		nodeName = "node name";
//		clone.GetComponentInChildren<TextMesh>().text = nodeName;
		clone.name = NameGen();
		CountNodes ();
		//CountLines ();

    }

	public void CountNodes()
	{
		nodelist = GameObject.FindGameObjectsWithTag("Node");
		node_length = nodelist.Length;

		txtnodes.text = "Number of nodes: " + node_length.ToString();


	}

	//Returns random name from long array of names 
	private string NameGen()
	{
		string[] names = new string[] 
		{
			"Keyan",
			"Allison",
			"Arthur",
			"Ana",
			"Alex",
			"Arlene",
			"Alberto",
			"Barry",
			"Bertha",
			"Bill",
			"Bonnie",
			"Bret",
			"Beryl",
			"Chantal",
			"Cristobal",
			"Claudette",
			"Charley",
			"Cindy",
			"Chris",
			"Dean",
			"Dolly",
			"Danny",
			"Danielle",
			"Dennis",
			"Debby",
			"Erin",
			"Edouard",
			"Erika",
			"Earl",
			"Emily",
			"Ernesto",
			"Felix",
			"Fay",
			"Fabian",
			"Frances",
			"Franklin",
			"Florence",
			"Gabielle",
			"Gustav",
			"Grace",
			"Gaston",
			"Gert",
			"Gordon",
			"Humberto",
			"Hanna",
			"Henri",
			"Hermine",
			"Harvey",
			"Helene",
			"Iris",
			"Isidore",
			"Isabel",
			"Ivan",
			"Irene",
			"Isaac",
			"Jerry",
			"Josephine",
			"Juan",
			"Jeanne",
			"Jose",
			"Joyce",
			"Karen",
			"Kyle",
			"Kate",
			"Karl",
			"Katrina",
			"Kirk",
			"Lorenzo",
			"Lili",
			"Larry",
			"Lisa",
			"Lee",
			"Leslie",
			"Michelle",
			"Marco",
			"Mindy",
			"Maria",
			"Michael",
			"Noel",
			"Nana",
			"Nicholas",
			"Nicole",
			"Nate",
			"Nadine",
			"Olga",
			"Omar",
			"Odette",
			"Otto",
			"Ophelia",
			"Oscar",
			"Pablo",
			"Paloma",
			"Peter",
			"Paula",
			"Philippe",
			"Patty",
			"Rebekah",
			"Rene",
			"Rose",
			"Richard",
			"Rita",
			"Rafael",
			"Sebastien",
			"Sally",
			"Sam",
			"Shary",
			"Stan",
			"Sandy",
			"Tanya",
			"Teddy",
			"Teresa",
			"Tomas",
			"Tammy",
			"Tony",
			"Van",
			"Vicky",
			"Victor",
			"Virginie",
			"Vince",
			"Valerie",
			"Wendy",
			"Wilfred",
			"Wanda",
			"Walter",
			"Wilma",
			"William",
			"Kumiko",
			"Aki",
			"Miharu",
			"Chiaki",
			"Michiyo",
			"Itoe",
			"Nanaho",
			"Reina",
			"Emi",
			"Yumi",
			"Ayumi",
			"Kaori",
			"Sayuri",
			"Rie",
			"Miyuki",
			"Hitomi",
			"Naoko",
			"Miwa",
			"Etsuko",
			"Akane",
			"Kazuko",
			"Miyako",
			"Youko",
			"Sachiko",
			"Mieko",
			"Toshie",
			"Junko"
		};

		return names[Random.Range(0, 152)];
	}

}