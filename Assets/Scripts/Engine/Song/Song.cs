using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Storage class that holds a song's hits, bpm, and audioclip
// Has utility functions for converting between song time and ms time

public class Song
{
	// Beats per minute
	private double bpm;

	// Offset in milliseconds
	private double offset;

	// Precalculated beats per second
	private double delay;

	// Ending beat of the song
	private double end;

	// Time to repeat song
	private double repeat;

	private List<Order> orders;

	private AudioClip clip;

	public Song(double bpm, double offset, string map, AudioClip clip)
	{
		this.bpm = bpm;
		this.offset = offset;
		delay = bpm / 60f;
		this.clip = clip;
		this.orders = processMap (map);
	}

	public double getBPM()
	{
		return bpm;
	}

	public double getOffset()
	{
		return offset;
	}

	public double getEndBeat()
	{
		return end;
	}

	public double getRepeatBeat()
	{
		return repeat;
	}

	public List<Order> getOrders()
	{
		return orders;
	}

	public AudioClip getClip()
	{
		return clip;
	}

	public void clearOrders()
	{
		orders.Clear ();
	}

	public void removeOrders(int range)
	{
		orders.RemoveRange (0, range);
	}

	public void addOrder(Order order)
	{
		orders.Add (order);
	}

	public double toMillisecondTime(double songTime, bool addOffset = false)
	{
		return songTime / delay - (addOffset ? offset : 0);
	}

	public double toSongTime(double secondTime, bool addOffset = false)
	{
		return (secondTime - (addOffset ? offset : 0)) * delay;
	}

	private List<Order> processMap(string map)
	{
		List<Order> orders = new List<Order>();
		List<Recipe> recipes = new List<Recipe> ();
		string[] lines = map.Split ('\n');
		double orderOffset = Mathf.Infinity;
		RecipeFactory factory = new RecipeFactory ();

		foreach (string line in lines)
		{
			if (line.Equals(""))
			{
				if (recipes.Count > 0)
				{
					orders.Add (new Order(orderOffset, recipes));
					recipes = new List<Recipe> ();
				}
				orderOffset = Mathf.Infinity;
				continue;
			}

			if (line.Equals ("END"))
			{
				end = orderOffset;
				continue;
			}

			if (line.Equals ("REPEAT"))
			{
				repeat = orderOffset;
				continue;
			}

			if (orderOffset == Mathf.Infinity)
			{
				orderOffset = double.Parse (line);
			}
			else
			{
				string[] instructions = line.Split (',');
				double startTime = double.Parse (instructions [1].Trim ()) + orderOffset;
				recipes.Add(factory.create (instructions [0], startTime));
			}
		}

		if (recipes.Count > 0)
		{
			orders.Add (new Order(orderOffset, recipes));
			recipes = new List<Recipe> ();
		}

		return orders;
	}
}