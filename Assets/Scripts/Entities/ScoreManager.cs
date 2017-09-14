using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour {

	private List<HIT_RESULT> hitResults;
	private int perfectOrders;

	public int getScorePercentage()
	{
		float total = 0;
		foreach (HIT_RESULT hit in hitResults)
		{
			if (hit == HIT_RESULT.HIT)
			{
				total++;
			}
			else if (hit == HIT_RESULT.BARELY)
			{
				total += 0.5f;
			}
		}
		return Mathf.FloorToInt((total / hitResults.Count) * 100);
	}

	public int getPerfectOrders()
	{
		return perfectOrders;
	}

	public int getTotalHits()
	{
		int total = 0;
		foreach (HIT_RESULT hit in hitResults)
		{
			if (hit == HIT_RESULT.HIT) {
				total++;
			}
		}
		return total;
	}

	public int getTotalBarelys()
	{
		int total = 0;
		foreach (HIT_RESULT hit in hitResults)
		{
			if (hit == HIT_RESULT.BARELY) {
				total++;
			}
		}
		return total;
	}

	public int getTotalMisses()
	{
		int total = 0;
		foreach (HIT_RESULT hit in hitResults)
		{
			if (hit == HIT_RESULT.MISS) {
				total++;
			}
		}
		return total;
	}

	public void addHitResult(HIT_RESULT hit)
	{
		hitResults.Add(hit);
	}

	public void addOrderResult(ORDER_RESULT order)
	{
		if (order == ORDER_RESULT.SUCCESS)
		{
			perfectOrders++;
		}
	}

	void Start ()
	{
		hitResults = new List<HIT_RESULT> ();
	}
}
