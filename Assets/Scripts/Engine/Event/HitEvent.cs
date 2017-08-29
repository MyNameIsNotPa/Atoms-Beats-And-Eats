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
		engine.addHit (getResult ());
		engine.showHitResult (getResult ());
	}

	override public void update(Engine engine)
	{
		if (engine.getSecondTime() - engine.toSecondTime(hitSongTime) > Leniency.BARELY_TIME)
		{
			result = HIT_RESULT.MISS;
			done = true;
			return;
		}

		if (engine.isKeyDown())
		{
			double interval = engine.getSecondTime() - engine.toSecondTime(hitSongTime);
			//Debug.Log (interval > 0 ? "Increase offset." : "Decrease offset.");

			interval = UnityEngine.Mathf.Abs ((float) interval);

			if (interval < Leniency.HIT_TIME)
				result = HIT_RESULT.HIT;
			else if (interval < Leniency.BARELY_TIME)
				result = HIT_RESULT.BARELY;
			else
				return;

			done = true;
			return;
		}
	}
}