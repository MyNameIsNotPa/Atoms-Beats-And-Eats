

public class TextEvent : Event {

	private string text;

	public TextEvent(double songTime, string text)
	{
		startSongTime = songTime;
		this.text = text;
	}

	override public void finish(Engine engine)
	{
		engine.displayText (text);
	}

	override public void update(Engine engine)
	{
		done = true;
	}
}
