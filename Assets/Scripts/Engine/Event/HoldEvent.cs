using UnityEngine;

public class HoldEvent : Event
{
	private double hitSongTime;
	private double endSongTime;
	private bool shown;
	private bool pressed;

	private HIT_RESULT result;

	public HoldEvent(double songTime, double endTime)
	{
		shown = false;
		pressed = false;
		startSongTime = songTime - 2;
		endSongTime = endTime;
		this.hitSongTime = songTime;
		this.result = HIT_RESULT.NONE;
	}

    public double getSongTime()
    {
        return hitSongTime;
    }

    public double getEndTime()
    {
        return endSongTime;
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
		if (engine.getSecondTime() - engine.toSecondTime(endSongTime) > Leniency.BARELY_TIME)
		{
			result = HIT_RESULT.MISS;
			done = true;
			return;
		}

		if (engine.getKeyHeld()) {
				if (engine.getSecondTime () - engine.toSecondTime (hitSongTime) < -(Leniency.BARELY_TIME)) {
					result = HIT_RESULT.MISS;
					done = true;
					return;
				}
				if (engine.getSecondTime () - engine.toSecondTime (endSongTime) > Leniency.BARELY_TIME) {
					result = HIT_RESULT.MISS;
					done = true;
					return;
				}
				pressed = true;
		} else {
			if (!pressed && engine.getSecondTime () - engine.toSecondTime (hitSongTime) > Leniency.BARELY_TIME) {
				result = HIT_RESULT.MISS;
				done = true;
				return;
			} else if (pressed) {
				double interval = engine.getSecondTime () - engine.toSecondTime (endSongTime);
				//Debug.Log (interval > 0 ? "Increase offset." : "Decrease offset.");

				// If the button was clicked close enough to the event:
				interval = UnityEngine.Mathf.Abs ((float)interval);

				// Change the hit result accordingly...
				if (interval < Leniency.HIT_TIME)
					result = HIT_RESULT.HIT;
				else if (interval < Leniency.BARELY_TIME)
					result = HIT_RESULT.BARELY;
				else
					result = HIT_RESULT.MISS;

				// And we are done with this event.
				done = true;
				return;
			}
		}
	}
}

