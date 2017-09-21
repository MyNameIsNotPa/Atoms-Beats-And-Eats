using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bookends : MonoBehaviour {

	private Animator animator;
	private AudioClip startSound;
	private AudioClip endSound;

	private AudioSource source;

	void Awake () {
		animator = GetComponent<Animator> ();
		source = GetComponent<AudioSource> ();
		startSound = Resources.Load<AudioClip> ("Sounds/start");
		endSound = Resources.Load<AudioClip> ("Sounds/finish");
	}

	public void onGameStart()
	{
		source.clip = startSound;
		source.Play ();
		animator.SetTrigger ("GameStart");
	}

	public void onGameEnd()
	{
		source.clip = endSound;
		source.Play ();
		animator.SetTrigger ("GameEnd");
	}
}
