using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

public class SongBehavior : MonoBehaviour
{

	private bool playing = false;
	private bool ended = false;
	private Song song;
	private SongPlayer player;
	private Engine engine;

	public string songName;

	[TextArea(5, 50)]
	public string map;

	public double bpm;

	[Tooltip("Offset in milliseconds for the audio track. " +
		"A larger positive offset plays the song later than its events, " +
		"and a larger negative offset plays the song earlier than its events. " +
		"For example, if the player character is bouncing too late, lower the offset. " +
		"The song will play earlier, so the character will bounce earlier in the song.")]
	public double offset;

	[Header("Debug")]
	public int startBeat = 0;

	void Start ()
	{
		GetComponent<AudioSource> ().clip = Resources.Load<AudioClip> ("Music/" + songName);
		song = new Song(bpm, offset / 1000f, map, GetComponent<AudioSource>().clip);
		player = new SongPlayer (song, GetComponent<AudioSource>());

		engine = GameObject.FindWithTag("Engine").GetComponent<Engine> ();
		engine.songEnd.AddListener (onSongEnd);
		engine.setPlayer (player);
		engine.startGame ();
	}

	public void onSongEnd()
	{
		player.stop ();
		ended = true;
	}

	void Update()
	{
		if (ended)
		{
			return;
		}

		if (playing)
		{
			if (engine.gamePaused)
			{
				player.pause ();
			}
			else
			{
				player.unPause ();
				player.update (engine);
			}
		}

		if (!playing)
		{
			playing = true;
			player.start (engine.toSecondTime(startBeat));
		}
	}
}
