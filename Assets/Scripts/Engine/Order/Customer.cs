using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer
{
	private GameObject prefab;
	private GameObject customer;

	private double startSongTime;
	private double endSongTime;
	private bool hasOrdered;
	private ORDER_RESULT orderResult;
	private bool shownResult;

	private Vector3 startPos = new Vector3 (-150f, -20f, 0);
	private Vector3 endPos = new Vector3 (-40f, -20f, 0);

	private Animator animator;

	private SpriteRenderer renderer;

	public Customer(double startSongTime, double endSongTime)
	{
		prefab = Resources.Load<GameObject> ("Prefabs/Customer");
		customer = CustomerManager.addCustomer (prefab);
		customer.transform.localPosition = startPos;
		hasOrdered = false;
		this.startSongTime = startSongTime;
		this.endSongTime = endSongTime;
		shownResult = false;
		animator = customer.GetComponent<Animator> ();
		renderer = customer.GetComponent<SpriteRenderer> ();
	}

	public void finishOrder(double currentBeat, ORDER_RESULT result)
	{
		startSongTime = currentBeat;
		endSongTime = currentBeat + 4;
		hasOrdered = true;
		orderResult = result;
	}

	public void destroy()
	{
		GameObject.DestroyImmediate (customer);
	}

	public void update(Engine engine)
	{
		double songTime = engine.getSongTime ();
		float beat;

		if (songTime > endSongTime)
		{
			beat = 0;
		}
		else
		{
			beat = Mathf.Abs(Mathf.Sin ((float) songTime * 4)) * 1.5f;
		}

		if (songTime >= startSongTime)
		{
			float interval = (float) ((songTime - startSongTime) / (endSongTime - startSongTime));
			if (interval >= 1)
			{
				renderer.sortingOrder = -1;
				animator.SetBool ("Order", true);
			}
			interval = Mathf.Max(0, Mathf.Min (1, interval));
			if (hasOrdered)
			{
				animator.SetBool ("WalkOut", true);
				if (!shownResult)
				{
					shownResult = true;
					engine.showOrderResult (orderResult);
				}
				customer.transform.localPosition = Vector3.Lerp (endPos, startPos, interval) + Vector3.up * beat; 
			}
			else
			{
				customer.transform.localPosition = Vector3.Lerp (startPos, endPos, interval) + Vector3.up * beat; 
			}
		}
	}
}
