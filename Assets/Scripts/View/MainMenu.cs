using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
	private AudioSource song;
	private Animator sign;
	private Animator stars;
	private Animator menu;
	public int bpm;
	private float delay;
	private int beat;
	private int currentButton;

	private EventSystem events;

	public GameObject credits;
	public GameObject help;
	private Canvas main;

	public List<GameObject> buttons;

	private AudioSource slide;

	void Start ()
	{
		events = GameObject.FindObjectOfType<EventSystem> ();
		menu = GetComponent<Animator> ();
		song = transform.Find("UIMain").GetComponent<AudioSource> ();
		delay = bpm / 60f;
		sign = transform.Find ("UIMain/Sign").GetComponent<Animator> ();
		stars = transform.Find ("Environment").GetComponent<Animator> ();
		currentButton = 0;
		main = transform.Find ("UIMain/Buttons").GetComponent<Canvas> ();
		slide = GetComponent<AudioSource> ();

		beat = 0;
		sign.SetInteger ("Beat", 1);
		stars.SetInteger ("Beat", 1);
		menu.SetBool ("InMenu", false);
		song.Play ();
		buttonEntered (0);
	}

	void Update ()
	{
		if (Input.anyKeyDown && !menu.GetBool ("InMenu") && !slide.isPlaying)
		{
			slide.Play ();
			toMenu ();
		}

		float songpos = song.time * delay;
		if (songpos > beat + 1)
		{
			beat++;
			sign.SetInteger ("Beat", (beat % 4) + 1);
			stars.SetInteger ("Beat", (beat % 4) + 1);
			for (int i = 0; i < buttons.Count; i++)
			{
				buttons [i].GetComponent<Animator> ().SetTrigger ("Beat");
			}
		}
		else if (songpos < beat)
		{
			beat = 0;
			sign.SetInteger ("Beat", 1);
			stars.SetInteger ("Beat", 1);
			for (int i = 0; i < buttons.Count; i++)
			{
				buttons [i].GetComponent<Animator> ().SetTrigger ("Beat");
			}
		}
	}

	public void buttonEntered(int button)
	{
		for (int i = 0; i < buttons.Count; i++)
		{
			buttons [i].GetComponent<Animator> ().SetBool ("Selected", i == button);
		}
	}

	public void toHome()
	{
		slide.Play ();
		menu.SetBool ("InMenu", false);
		events.SetSelectedGameObject (null);
	}

	public void toMenu()
	{
		main.enabled = true;
		//buttonEntered (0);
		menu.SetBool ("InMenu", true);
		help.SetActive (false);
		credits.SetActive (false);
		events.SetSelectedGameObject (buttons [0].gameObject);
	}

	public void openCredits()
	{
		main.enabled = false;
		credits.SetActive (true);
		events.SetSelectedGameObject (credits.transform.Find("BackArrow").gameObject);
	}

	public void openHelp()
	{
		main.enabled = false;
		help.SetActive (true);
		events.SetSelectedGameObject (help.transform.Find("BackArrow").gameObject);
	}
}
