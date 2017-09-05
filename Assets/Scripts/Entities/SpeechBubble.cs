using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechBubble : MonoBehaviour
{

	private int speechHideStack;
	private Animator animator;
	private SpriteRenderer recipe;

	private Sprite success;
	private Sprite failure;

	private AudioSource audioSource;

	void Awake ()
	{
		speechHideStack = 0;
		recipe = transform.Find("Image").GetComponent<SpriteRenderer> ();
		animator = GetComponent<Animator> ();
		audioSource = GetComponent<AudioSource> ();

		success = Resources.Load<Sprite> ("RecipeIcons/Success");
		failure = Resources.Load<Sprite> ("RecipeIcons/Failure");
	}

	public void orderStart()
	{
		speechHideStack++;
		gameObject.SetActive (true);
	}

	public void orderEnd(ORDER_RESULT result)
	{
		speechHideStack--;
		speechHideStack = Mathf.Max (0, speechHideStack);
		if (speechHideStack == 0)
		{
			gameObject.SetActive (false);
		}
	}

	public void recipeStart(Sprite image)
	{
		recipe.sprite = image;
		animator.SetTrigger ("Pop");
	}

	public void recipeEnd(bool didSucceed)
	{
		recipe.sprite = didSucceed ? success : failure;
		if (didSucceed)
		{
			audioSource.Play ();
		}
		animator.SetTrigger ("Pop");
	}
}
