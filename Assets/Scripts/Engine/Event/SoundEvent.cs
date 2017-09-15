using UnityEngine;

public class SoundEvent : Event
{
	private double hitSongTime;
	private AudioClip clip;

	public SoundEvent(double songTime, AudioClip clip)
	{
		startSongTime = songTime - 0.5;
		this.hitSongTime = songTime;
		this.clip = clip;
	}

	public SoundEvent (double songTime)  : this (songTime, Resources.Load<AudioClip> ("Sounds/blip"))
	{
	}

	public double getSongTime()
	{
		return hitSongTime;
	}

	// Update this event each song tick
	override public void update(Engine engine)
	{
		if (engine.getSecondTime() >= engine.toSecondTime(startSongTime))
		{
			double now = AudioSettings.dspTime;
			double wait = engine.toSecondTime(hitSongTime) - engine.getSecondTime ();
			engine.playSound (clip, now + wait);
			done = true;
		}
	}
}