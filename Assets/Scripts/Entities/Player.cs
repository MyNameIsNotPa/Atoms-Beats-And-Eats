using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

	private int lastMeasure;
	private Animator animator;

	void Start ()
	{
		animator = GetComponent<Animator> ();
		lastMeasure = -1;
	}

	public void recipeEnd(bool didSucceed)
	{
		if (didSucceed)
		{
			animator.SetTrigger ("Success");
		}
		else
		{
			animator.SetTrigger ("Fail");
		}
	}

	// Animate the player to the beat
	public void updateBeat(float songTime)
	{
		int measure = (int) Mathf.Floor (songTime);
		if (songTime >= 0 && measure != lastMeasure && measure % 2 == 0)
		{
			lastMeasure = measure;
			animator.SetTrigger ("Bounce");
		}
	}
}