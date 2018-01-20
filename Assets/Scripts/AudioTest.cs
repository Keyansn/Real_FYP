using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEditor;


public class AudioTest : MonoBehaviour
{
	public Button yourButton;
	public int position = 0;
	public int samplerate = 44100;
	public float frequency = 440;


	void Start()
	{
		//AudioClip myClip = AudioClip.Create("MySinusoid", samplerate * 2, 1, samplerate, true, OnAudioRead, OnAudioSetPosition);
		//aud.clip = myClip;
		Button btn = yourButton.GetComponent<Button>();
		btn.onClick.AddListener(OnClick);

		//aud.Play();
	}

	void OnAudioRead(float[] data)
	{
		int count = 0;
		while (count < data.Length)
		{
			data[count] = Mathf.Sign(Mathf.Sin(2 * Mathf.PI * frequency * position / samplerate));
			position++;
			count++;
		}
	}

	void OnAudioSetPosition(int newPosition)
	{
		position = newPosition;
	}

	void OnClick(){
		//AudioSource aud = GetComponent<AudioSource>();
		//aud.loop = true;
		//aud.Play();
		//aud.pitch = aud.pitch + 0.01f;
	}
}