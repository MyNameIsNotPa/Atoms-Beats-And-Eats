﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
	private EventSystem events;

	private Animator animator;

	private GameObject button1;
	private GameObject button2;
	private GameObject button3;

	private Animator button1Animator;
	private Animator button2Animator;
	private Animator button3Animator;

	public LevelChanger levelChanger;

	private Engine engine;

	private int currSelected;

	private AudioSource source;

	private bool wasPaused;

	void Start ()
	{
		wasPaused = false;
		events = GameObject.FindObjectOfType<EventSystem> ();
		currSelected = 1;
		animator = GetComponent<Animator> ();
		button1 = transform.Find ("Controls/Resume").gameObject;
		button2 = transform.Find ("Controls/Restart").gameObject;
		button3 = transform.Find ("Controls/Quit").gameObject;
		source = button1.GetComponent<AudioSource> ();

		engine = GameObject.FindGameObjectWithTag ("Engine").GetComponent<Engine> ();

		button1.GetComponent<Button> ().onClick.AddListener(button1Click);
		button2.GetComponent<Button> ().onClick.AddListener(button2Click);
		button3.GetComponent<Button> ().onClick.AddListener(button3Click);

		button1Animator = button1.GetComponent<Animator> ();
		button2Animator = button2.GetComponent<Animator> ();
		button3Animator = button3.GetComponent<Animator> ();
	}

	void Update ()
	{
		if (engine.gamePaused && !wasPaused)
		{
			events.SetSelectedGameObject (button1);
		}
		else if (!engine.gamePaused && wasPaused)
		{
			events.SetSelectedGameObject (null);
		}
		wasPaused = engine.gamePaused;
		animator.SetBool ("IsPaused", engine.gamePaused);
		button1Animator.SetInteger ("Selected", currSelected);
		button2Animator.SetInteger ("Selected", currSelected);
		button3Animator.SetInteger ("Selected", currSelected);
	}

	// Resume button was clicked
	public void button1Click()
	{
		engine.gamePaused = false;
	}

	// Restart button was clicked
	public void button2Click()
	{
		levelChanger.loadLevel (SceneManager.GetActiveScene().name);
	}

	// Quit button was clicked
	public void button3Click()
	{
		levelChanger.loadLevel ("LevelSelect");
	}

	public void button1Enter()
	{
		currSelected = 1;
	}

	public void button2Enter()
	{
		currSelected = 2;
	}

	public void button3Enter()
	{
		currSelected = 3;
	}
}
