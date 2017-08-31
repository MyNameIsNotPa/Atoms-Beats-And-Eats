using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// An Order holds a list of recipes and an offset.

public class Order
{
	private double startSongTime;
	private double endSongTime;

	private bool done;
	private bool started;
	private bool shown;

	private bool finished;

	private Customer customer;

	private ORDER_RESULT result;

	private List<Recipe> recipes;

	public Order(double startSongTime, List<Recipe> recipes)
	{
		this.startSongTime = startSongTime;
		this.recipes = recipes;
		done = false;
		started = false;
		finished = false;
		shown = false;
		result = ORDER_RESULT.SUCCESS;
	}

	public bool isDone()
	{
		return done;
	}

	public bool isStarted()
	{
		return started;
	}

	public bool isFinished()
	{
		return finished;
	}

	public double getStartSongTime()
	{
		return startSongTime;
	}

	public double getEndSongTime()
	{
		return endSongTime;
	}

	public void start(Engine engine)
	{
		started = true;
		customer = new Customer (engine, startSongTime - 4f, startSongTime);
	}

	public void finish(Engine engine)
	{
		customer.finishOrder (engine.getNearestBeat () + 1, result == ORDER_RESULT.SUCCESS);
		endSongTime = engine.getNearestBeat () + 5;
		finished = true;
	}

	public void destroy()
	{
		customer.destroy ();
	}

	public void update(Engine engine)
	{
		// Update customer visuals
		customer.update(engine);

		// Update order visuals
		if (!shown && engine.getSongTime () >= startSongTime)
		{
			shown = true;
			engine.showStartOrder ();
		}

		if (done)
		{
			return;
		}

		// Update recipes
		bool allDone = true;
		foreach(Recipe r in recipes)
		{
			// If the Recipe has not been completed
			if (!r.isDone ())
			{
				allDone = false;
				if (engine.getSongTime() >= r.getStartSongTime())
				{
					if (!r.isStarted ())
					{
						r.start (engine);
					}

					r.update (engine);

					if (r.isDone ())
					{
						r.finish (engine);
						if (!r.didSucceed ())
						{
							result = ORDER_RESULT.FAILURE;
						}
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