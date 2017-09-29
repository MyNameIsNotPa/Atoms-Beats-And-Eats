using UnityEngine;

public class ReleaseEvent : HitEvent
{
	private double releaseSongTime;

	public ReleaseEvent(double songTime)
	{
		startSongTime = songTime - 2;
		this.releaseSongTime = songTime;
		this.setResult(HIT_RESULT.NONE);
	}

	override public void finish(Engine engine)
	{
		engine.showHitResult (getResult ());
	}

	// Update this hit event each song tick
	override public void update(Engine engine)
	{
		// If no input was received in time, the player missed this event.
		if (engine.getSecondTime() - engine.toSecondTime(releaseSongTime) > Leniency.BARELY_TIME)
		{
			setResult (HIT_RESULT.MISS);
			done = true;
			return;
		}

		// If the player presses the space bar or clicks the mouse:
		if (engine.getFrameUp() != -1)
		{
			double interval = engine.getFrameUp() - engine.toSecondTime(releaseSongTime);
			//Debug.Log (interval > 0 ? "Increase offset." : "Decrease offset.");

			// If the button was clicked close enough to the event:
			interval = UnityEngine.Mathf.Abs ((float) interval);

			// Change the hit result accordingly...
			if (interval < Leniency.HIT_TIME)
				setResult (HIT_RESULT.HIT);
			else if (interval < Leniency.BARELY_TIME)
				setResult (HIT_RESULT.BARELY);
			else
				return;

			// And we are done with this event.
			done = true;
			return;
		}
	}
}

