using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	public float speed;
    public Text winText;
    public Text countText;

	private Rigidbody rb;
    private int count;

	void Start()
	{
		//runs on first frame of scrip running
		rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winText.text = "";
        Renderer rend = GetComponent<Renderer>();
        rend.material.shader = Shader.Find("Specular");
        rend.material.SetColor("_SpecColor", Color.red);

    }


//	void Update()
//	{
//	before frame rendered
//	}

	void FixedUpdate()
	{
		//physics
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

		rb.AddForce (movement*speed);
	}

    void OnTriggerEnter(Collider other)
    {
        //Destroy(other.gameObject);
        if (other.gameObject.CompareTag("Pick Up"))
        {
            count = count + 1;
            other.gameObject.SetActive(false);
            SetCountText();

        }
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 5)
        {
            winText.text = "You Win!";
}
    }


}
