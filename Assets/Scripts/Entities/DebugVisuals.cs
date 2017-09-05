using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugVisuals : MonoBehaviour
{

	private Slider beatSlider;
	private Slider measureSlider;
	private Text beatNum;

	[Header("Debug Settings")]
	public bool showDebugVisuals;

	void Awake ()
	{
		beatSlider = transform.Find ("Beat").GetComponent<Slider> ();
		measureSlider = transform.Find ("Measure").GetComponent<Slider> ();
		beatNum = transform.Find ("BeatNum").GetComponent<Text> ();
	}

	public void updateBeat(float songTime)
	{
		if (showDebugVisuals)
		{
			gameObject.SetActive (true);
			beatSlider.value = songTime - Mathf.Floor (songTime);
			measureSlider.value = Mathf.Floor (songTime) % 4;
			beatNum.text = "" + Mathf.Floor (songTime);
		}
	}
}