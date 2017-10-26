using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongPlayer
{
	private Song song;

	private double startTime;
	private List<HIT_RESULT> results;

	private double repeatOffset;

	public AudioSource source;

	public SongPlayer(Song song, AudioSource source)
	{
		this.song = song;
		this.source = source;
		this.results = new List<HIT_RESULT> ();
		source.clip = song.getClip();
		repeatOffset = 0;
	}

	public void start(double startBeat)
	{
		source.time = (float) startBeat;
		source.PlayScheduled (AudioSettings.dspTime + 7f);
		startTime = AudioSettings.dspTime;
	}

	public void stop()
	{
		source.Stop ();
	}

	public void pause()
	{
		source.Pause ();
	}

	public void unPause()
	{
		source.UnPause ();
	}

	public bool isPlaying()
	{
		return source.isPlaying && getSongTime() > 0;
	}

	public Song getSong()
	{
		return song;
	}

	public void addResult(HIT_RESULT result)
	{
		results.Add (result);
	}

	public List<HIT_RESULT> getResults()
	{
		return results;
	}

	public double getSongTime()
	{
		return song.toSongTime (getSecondTime ());
	}

	public double getSecondTime()
	{
		double secondTime = source.time - song.getOffset();
		secondTime += song.toMillisecondTime (repeatOffset);
		return secondTime;
	}

	public void update(Engine engine)
	{
		if (engine.getSongTime () >= song.getEndBeat ())
		{
			engine.finishSong ();
			return;
		}

		if (engine.getSongTime () >= song.getRepeatBeat () + repeatOffset)
		{
			source.Stop ();
			source.Play ();
			repeatOffset += song.getRepeatBeat ();
			return;
		}

		engine.updateBeat ();

		// Update hits
		int toRemove = 0;
		foreach(Order order in song.getOrders ())
		{
			// If the order was just completed
			if (order.isDone())
			{
				if (!order.isFinished ())
				{
					order.finish (engine);
				}
				if (getSongTime () > order.getEndSongTime ())
				{
					order.destroy ();
					toRemove++;
				}
				else
				{
					order.update (engine);
				}
			}
			else
			{
				// Update hits that are within 4 beats of becoming activated
				if (getSongTime() + 4 > order.getStartSongTime())
				{
					if (!order.isStarted ())
					{
						order.start (engine);
					}

					order.update (engine);

					if (order.isDone ())
					{
						if (!order.isFinished ())
						{
							order.finish (engine);
						}
					}
				}
				else
				{
					break;
				}
			}
		}

		song.removeOrders (toRemove);
	}
}
