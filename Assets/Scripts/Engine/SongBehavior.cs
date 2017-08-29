using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

public class SongBehavior : MonoBehaviour {

	private bool playing = false;
	private Song song;
	private SongPlayer player;
	private Engine engine;
	private bool paused;

	public string songName;

	[TextArea(5, 50)]
	public string map;

	public double bpm;

	[Tooltip("Offset in milliseconds for the audio track." +
		"A larger positive offset plays the song later than its events," +
		"and a larger negative offset plays the song earlier than its events.")]
	public double offset;

	// Use this for initialization
	void Start ()
	{
		song = new Song(bpm, offset / 1000f, map, GetComponent<AudioSource>().clip);
		player = new SongPlayer (song, GetComponent<AudioSource>());

		engine = GetComponentInParent<Engine> ();
		engine.setPlayer (player);

		paused = false;
	}

	void Update()
	{
		if (engine.isPauseKeyDown ())
		{
			paused = !paused;
		}

		if (playing)
		{
			if (paused)
			{
				player.pause ();
			}
			else
			{
				player.unPause ();
				player.update (Input.GetMouseButtonDown (0), engine);
			}
		}

		if (!playing)
		{
			playing = true;
			player.start ();
		}
	}
}
