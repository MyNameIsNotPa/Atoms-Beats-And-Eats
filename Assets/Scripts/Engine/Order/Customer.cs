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
	private bool orderResult;
	private bool shownResult;

	private Vector3 startPos = new Vector3 (-150f, -15f, 0);
	private Vector3 endPos = new Vector3 (-25f, -15f, 0);

	public Customer(Engine engine, double startSongTime, double endSongTime)
	{
		prefab = Resources.Load<GameObject> ("Prefabs/Customer");
		customer = engine.addCustomer (prefab);
		customer.transform.localPosition = startPos;
		hasOrdered = false;
		this.startSongTime = startSongTime;
		this.endSongTime = endSongTime;
		shownResult = false;
	}

	public void finishOrder(double currentBeat, bool result)
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
			interval = Mathf.Max(0, Mathf.Min (1, interval));
			if (hasOrdered)
			{
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
