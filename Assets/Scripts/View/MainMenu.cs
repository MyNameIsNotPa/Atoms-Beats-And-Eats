using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
	private AudioSource song;
	private Animator sign;
	private Animator stars;
	public int bpm;
	private float delay;
	private int beat;

	void Start ()
	{
		song = transform.Find("UIMain").GetComponent<AudioSource> ();
		delay = bpm / 60f;
		sign = transform.Find ("UIMain/Sign").GetComponent<Animator> ();
		stars = transform.Find ("Environment").GetComponent<Animator> ();
		beat = 0;
		sign.SetInteger ("Beat", 1);
		stars.SetInteger ("Beat", 1);
	}

	void Update ()
	{
		float songpos = song.time * delay;
		if (songpos > beat + 1)
		{
			beat++;
			sign.SetInteger ("Beat", (beat % 4) + 1);
			stars.SetInteger ("Beat", (beat % 4) + 1);
		}
		else if (songpos < beat)
		{
			beat = 0;
			sign.SetInteger ("Beat", 1);
			stars.SetInteger ("Beat", 1);
		}
	}
}
