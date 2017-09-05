using UnityEngine;

public class HitEvent : Event
{
	private double hitSongTime;

	private HIT_RESULT result;

	public HitEvent(double songTime)
	{
		startSongTime = songTime - 2;
		this.hitSongTime = songTime;
		this.result = HIT_RESULT.NONE;
	}

	public double getSongTime()
	{
		return hitSongTime;
	}

	public HIT_RESULT getResult()
	{
		return result;
	}

	override public void finish(Engine engine)
	{
		engine.showHitResult (getResult ());
	}

	// Update this hit event each song tick
	override public void update(Engine engine)
	{
		// If no input was received in time, the player missed this event.
		if (engine.getSecondTime() - engine.toSecondTime(hitSongTime) > Leniency.BARELY_TIME)
		{
			result = HIT_RESULT.MISS;
			done = true;
			return;
		}

		// If the player presses the space bar or clicks the mouse:
		if (engine.isKeyDown())
		{
			double interval = engine.getSecondTime() - engine.toSecondTime(hitSongTime);
			//Debug.Log (interval > 0 ? "Increase offset." : "Decrease offset.");

			// If the button was clicked close enough to the event:
			interval = UnityEngine.Mathf.Abs ((float) interval);

			// Change the hit result accordingly...
			if (interval < Leniency.HIT_TIME)
				result = HIT_RESULT.HIT;
			else if (interval < Leniency.BARELY_TIME)
				result = HIT_RESULT.BARELY;
			else
				return;

			// And we are done with this event.
			done = true;
			return;
		}
	}
}