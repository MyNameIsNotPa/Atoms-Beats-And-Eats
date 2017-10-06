using System.Collections;
using System.Collections.Generic;

// A base Recipe class that serves as data storage for Events,
// And knows how to update its internal Events.
// An Order is made of Recipes, which are made of Events.
// Implementations of this class construct different Recipes.

using UnityEngine;

public class Recipe
{
	protected List<Event> events;
	protected bool done;
	protected bool succeeded;

	protected Sprite image;

	private bool started;

	protected double startSongTime;

	public Recipe(double startSongTime)
	{
		this.startSongTime = startSongTime;
		events = new List<Event> ();
		done = false;
		succeeded = true;
		started = false;
	}

	public List<double> getHitTimes()
	{
		List<double> hitTimes = new List<double> ();
		foreach(Event e in events)
		{
			if (e.GetType() == typeof(HitEvent))
			{
				hitTimes.Add (e.getStartSongTime () + 2);
			}
		}
		return hitTimes;
	}

	public Sprite getSprite()
	{
		return image;
	}

	public void start(Engine engine)
	{
		started = true;
		engine.showRecipeStart (this, image);
	}

	public void finish(Engine engine)
	{
		engine.showRecipeResult (succeeded);
	}

	public double getStartSongTime()
	{
		return startSongTime;
	}

	public bool isDone()
	{
		return done;
	}

	public bool isStarted()
	{
		return started;
	}

	public bool didSucceed()
	{
		return succeeded;
	}

	public void update(Engine engine)
	{
		// Update hits
		bool allDone = true;
		foreach(Event e in events)
		{
			// If the Hit has been missed or hit
			if (!e.isDone ())
			{
				allDone = false;
				if (engine.getSongTime() >= e.getStartSongTime())
				{
					e.update (engine);

					if (e.isDone ())
					{
						if (e.GetType () == typeof(HitEvent))
						{
							if (((HitEvent)e).getResult () == HIT_RESULT.MISS)
							{
								succeeded = false;
							}
						}
						e.finish (engine);
					}
				}
			}
		}

		if (allDone)
		{
			done = true;
		}
	}
}
