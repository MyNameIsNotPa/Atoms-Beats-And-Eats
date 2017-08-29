// Abstract Event class that serves as the parent of all Events.
// All Events can be updated and are done or not done.

public class Event
{
	// Is this event done processing?
	protected bool done;

	// When to start processing this event in Song time
	protected double startSongTime;

	public double getStartSongTime()
	{
		return startSongTime;
	}

	public bool isDone()
	{
		return done;
	}

	public virtual void finish(Engine engine)
	{
		return;
	}

	public virtual void update(Engine engine)
	{
		return;
	}
}