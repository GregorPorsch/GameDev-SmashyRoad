using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LightManager : MonoBehaviour {

	public CopLight[] LightList;
	public float rotationsPerMinute = 50.0f;
	public float rotationsPerMinute2 = 70.0f;
	private float downTime, upTime, pressTime = 0;
	private float countDown = 0.2f;
	private bool horn = false;
	private float mode = 0;
	private int sirenMode = 0;
	private Light[] lights;
	public GameObject lightParent;
	private AudioSource Sound;
	public AudioClip Siren1;
	public AudioClip Siren2;
	public AudioClip Horn;


	void Awake()
	{
		Sound = GetComponent<AudioSource> ();


	}

	// Use this for initialization
	void Start () {
		LightList = GetComponentsInChildren<CopLight>();
		Debug.Log(LightList.Length);
		lights = lightParent.GetComponentsInChildren<Light>();
		foreach (Light light in lights)
		{
			light.enabled = false; //turn off the light at start of game
		}
	}

	// Update is called once per frame
	void Update () {

		for(int i = 0; i < LightList.Length; i++)
		{
			if(LightList[i].rotation == true && sirenMode == 1)
			{
				LightList[i].transform.Rotate(0, 6.0f * rotationsPerMinute * Time.deltaTime, 0);
			}
			else if(LightList[i].rotation == true && sirenMode == 2)
			{
				LightList[i].transform.Rotate(0, 6.0f * rotationsPerMinute2 * Time.deltaTime, 0);
			}
			else if(LightList[i].flickering == true && sirenMode == 1)
			{
				LightList[i].flicker = false;
			}
			else if(LightList[i].flickering == true && sirenMode == 2)
			{
				LightList[i].flicker = true;
			}

		}
		if (Input.GetKeyDown (KeyCode.F) && horn == false) //Start the countdown
		{
			downTime = Time.time;
			pressTime = downTime + countDown;
			mode = 1;
		}

		if (Input.GetKeyUp (KeyCode.F) && horn == false && mode == 1)
		{
			sirenMode += 1;

			if (sirenMode > 2)
			{
				sirenMode = 0;
				horn = false;
				mode = 3;
				Sound.Stop ();
			}

			if(sirenMode == 0)
			{
				Debug.Log ("Resetting lightmode");
				horn = false;
				mode = 3;
				//No lights and sirens
				foreach (Light light in lights)
				{
					light.enabled = false; //turn off the light at start of game
				}
			}

			else if(sirenMode == 1)
			{
				Debug.Log ("You clicked once!");
				horn = false;
				mode = 3;
				//Lights only
				foreach (Light light in lights)
				{
					light.enabled = true; //turn on the lights
				}
			}

			else if (sirenMode == 2)
			{
				Debug.Log("You clicked twice!");
				horn = false;
				mode = 3;
				Sound.loop = true;
				Sound.clip = Siren2;
				Sound.Play ();
				//Lights and sirens
				foreach (Light light in lights)
				{
					light.enabled = true; //turn on the lights
				}
			}
		}
		if (Time.time >= pressTime && mode == 1) //Play horn or siren
		{
			horn = true; //Horn is activated
			mode = 2;
			if (sirenMode == 0) {
				Sound.loop = true;
				Sound.clip = Horn;
				Sound.Play ();
				Debug.Log ("Horn");
			}
			else if (sirenMode == 1) {
				Sound.loop = true;
				Sound.clip = Siren1;
				Sound.Play ();
				Debug.Log ("2nd siren");
			} else if (sirenMode == 2) {
				Sound.loop = true;
				Sound.clip = Siren1;
				Sound.Play ();
				Debug.Log ("2nd siren");
			}

		}    
		if (Input.GetKeyUp (KeyCode.F) && horn == true && mode == 2) //Return to current sirenMode
		{
			horn = false; //Horn is turned off
			Sound.Stop ();
			Debug.Log("Horn off");
			mode = 0;
			if (sirenMode == 2) {
				Sound.loop = true;
				Sound.clip = Siren2;
				Sound.Play ();
				Debug.Log ("2nd siren");
			}
		}
	}
}
