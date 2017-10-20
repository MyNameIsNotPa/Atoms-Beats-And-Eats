using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Results : MonoBehaviour {

	private Text perfect;
	private Text barely;
	private Text miss;
	private Text scorePercent;
	private Text customers;

	private Animator animator;

	private GameObject main;

	private ScoreManager scoreManager;

	private RectTransform fullBar;

	private int score;

	void Start () {
		perfect = transform.Find ("Main/Scores/Hits/Perfect/Text").GetComponent<Text> ();
		barely = transform.Find ("Main/Scores/Hits/Barely/Text").GetComponent<Text> ();
		miss = transform.Find ("Main/Scores/Hits/Miss/Text").GetComponent<Text> ();
		scorePercent = transform.Find ("Main/ScoreBar/Text").GetComponent<Text> ();
		customers = transform.Find ("Main/Scores/PerfectOrders/Text").GetComponent<Text> ();
		fullBar = transform.Find ("Main/ScoreBar/FullBar").GetComponent<RectTransform> ();
		scoreManager = GameObject.FindWithTag("ScoreManager").GetComponent<ScoreManager> ();
		animator = GetComponent<Animator> ();
	}

	private void displayScores(int orders, int perfects, int barelys, int misses, int score)
	{
		this.score = score;
		customers.text = orders + "";
		perfect.text = perfects + "";
		barely.text = barelys + "";
		miss.text = misses + "";
		scorePercent.text = score + "%";
		fullBar.localScale = new Vector3 (score / 100f, 1, 1);
	}

	IEnumerator showResults()
	{
		int orders = scoreManager.getPerfectOrders ();
		int perfects = scoreManager.getTotalHits ();
		int barelys = scoreManager.getTotalBarelys ();
		int misses = scoreManager.getTotalMisses ();
		int score = scoreManager.getScorePercentage ();
		displayScores (orders, perfects, barelys, misses, score);
		yield return new WaitForSeconds (3);
		animator.SetTrigger ("ShowResults");
	}

	public void onResultsCallback()
	{
		if (score >= 70)
		{
			transform.Find ("Main/GoodJob").gameObject.SetActive (true);
		}
		else
		{
			transform.Find ("Main/TryAgain").gameObject.SetActive (true);
		}
	}

	public void onSongEnd()
	{
		StartCoroutine (showResults());
	}
}
