﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
	private Animator anim;

	IEnumerator loadScene(string name)
	{
		anim.SetTrigger ("FadeOut");
		yield return new WaitForSeconds (1);
		SceneManager.LoadScene (name);
	}

	private void fadeOut()
	{
		anim.SetTrigger ("FadeOut");
	}

	public void loadLevel(string name)
	{
		StartCoroutine (loadScene (name));
	}

	public void loadPractice(string recipes)
	{
		recipes = recipes.Replace (", ", "\n");
		GameObject practice = GameObject.Instantiate(Resources.Load<GameObject> ("Prefabs/PracticeRecipes"));
		DontDestroyOnLoad (practice);
		practice.GetComponent<PracticeRecipes> ().setRecipes (recipes);
		loadLevel ("Practice");
	}

	void Start ()
	{
		anim = GetComponent<Animator> ();
		anim.SetTrigger ("FadeIn");
	}
}