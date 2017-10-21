using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
	public List<string> levels;
	public List<string> descriptions;
	public List<string> practices;

	public int currentLevel;

	private Text levelName;
	private Text levelDesc;
	private Text highScore;

	public Button leftButton;
	public Button rightButton;

	public LevelChanger levelChanger;

	private Animator anim;

	void Start ()
	{
		anim = GetComponent<Animator> ();
		levelName = transform.Find("Info/LevelName").GetComponent<Text>();
		levelDesc = transform.Find("Info/LevelDesc").GetComponent<Text>();
		highScore = transform.Find("Info/HighScore").GetComponent<Text>();
        highScore.text = SaveData.getHighScore(levels[currentLevel]).ToString() + "/100";
        if (PlayerPrefs.GetInt("Number of Levels Unlocked") == 0)
        {
            PlayerPrefs.SetInt("Number of Levels Unlocked", 1);
        }
		if (currentLevel == 0)
		{
			leftButton.gameObject.SetActive (false);
		}
		if (currentLevel == levels.Count - 1 || currentLevel == PlayerPrefs.GetInt("Number of Levels Unlocked") - 1)
		{
			rightButton.gameObject.SetActive (false);
		}
	}

	public void startPractice()
	{
		levelChanger.loadPractice(practices[currentLevel]);
	}

	public void startPlay()
	{
		levelChanger.loadLevel(levels [currentLevel]);
	}

	public void changeTicket()
	{
		levelName.text = levels [currentLevel];
		levelDesc.text = descriptions [currentLevel];
        highScore.text = SaveData.getHighScore(levels[currentLevel]).ToString() + "/100";
        leftButton.gameObject.SetActive (true);
		rightButton.gameObject.SetActive (true);
		if (currentLevel == 0)
		{
			leftButton.gameObject.SetActive (false);
		}
		if (currentLevel == levels.Count - 1 || currentLevel == PlayerPrefs.GetInt("Number of Levels Unlocked") - 1)
		{
			rightButton.gameObject.SetActive (false);
		}
	}

	public void scrollLeft()
	{
		if (currentLevel > 0)
		{
			currentLevel--;
			anim.SetTrigger ("Switch");
		}
	}

	public void scrollRight()
	{
		if (currentLevel < levels.Count - 1)
		{
			currentLevel++;
			anim.SetTrigger ("Switch");
		}
	}
}
