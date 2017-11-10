using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class LevelSelect : MonoBehaviour
{
	public List<string> levels;
	public List<string> descriptions;
	public List<string> practices;

	private int currentLevel;

	private Text levelName;
	private Text levelDesc;
	private Text highScore;

	public Button leftButton;
	public Button rightButton;

	public LevelChanger levelChanger;

	public Animator musicFader;

	private Animator anim;

	private AudioSource source;

	private EventSystem events;

	void Start ()
	{
		currentLevel = SelectedLevel.selectedLevel;
		events = GameObject.FindObjectOfType<EventSystem> ();
		Cursor.visible = true;
		source = GetComponent<AudioSource> ();
		anim = GetComponent<Animator> ();
		levelName = transform.Find("Info/LevelName").GetComponent<Text>();
		levelDesc = transform.Find("Info/LevelDesc").GetComponent<Text>();
		highScore = transform.Find("Info/HighScore").GetComponent<Text>();
        if (PlayerPrefs.GetInt("Number of Levels Unlocked") == 0)
        {
            PlayerPrefs.SetInt("Number of Levels Unlocked", 1);
        }
		changeTicket ();
	}

	void Update()
	{
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;
	}

	public void startPractice()
	{
		musicFader.SetTrigger ("FadeOut");
		levelChanger.loadPractice(practices[currentLevel]);
	}

	public void startPlay()
	{
		musicFader.SetTrigger ("FadeOut");
		levelChanger.loadLevel(levels [currentLevel]);
	}

	public void mainMenu()
	{
		musicFader.SetTrigger ("FadeOut");
		levelChanger.loadLevel("MainMenu");
	}

	public void changeTicket()
	{
		levelName.text = levels [currentLevel];
		levelDesc.text = descriptions [currentLevel];
        highScore.text = SaveData.getHighScore(levels[currentLevel]).ToString() + "/100";
        leftButton.gameObject.SetActive (true);
		rightButton.gameObject.SetActive (true);
	}

	public void scrollLeft()
	{
		if (currentLevel > 0)
		{
			currentLevel--;
			anim.SetTrigger ("Switch");
			source.Play ();
		}
		else
		{
			currentLevel = PlayerPrefs.GetInt("Number of Levels Unlocked") - 1;
			anim.SetTrigger ("Switch");
			source.Play ();
		}
		SelectedLevel.selectedLevel = currentLevel;
	}

	public void scrollRight()
	{
		if (currentLevel < PlayerPrefs.GetInt ("Number of Levels Unlocked") - 1)
		{
			currentLevel++;
			anim.SetTrigger ("Switch");
			source.Play ();
		}
		else
		{
			currentLevel = 0;
			anim.SetTrigger ("Switch");
			source.Play ();
		}
		SelectedLevel.selectedLevel = currentLevel;
	}
}
