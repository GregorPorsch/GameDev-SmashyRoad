using UnityEngine;
using System.Collections;




public class CopLight : MonoBehaviour {

	public float Offset;
	public bool rotation = false;
	public bool flickering = false;
	public bool flicker = false;
	public float delayFlicker = 0;
	public float speedFlicker = 0.4f;

	Light testLight;


	void Start()
	{
		transform.Rotate(0, Offset, 0);
		testLight = GetComponent<Light> ();
		StartCoroutine(Flashing());
	}

	void Update ()
	{

	}
	IEnumerator Flashing ()
	{
		if (flickering == true) {
			yield return new WaitForSeconds (delayFlicker);
			while (true) 
			{
				yield return new WaitForSeconds (speedFlicker);
				testLight.intensity = 0;
				yield return new WaitForSeconds (speedFlicker);
				testLight.intensity = 3;
				Debug.Log ("Flicker");
			}
		}
	}
}





