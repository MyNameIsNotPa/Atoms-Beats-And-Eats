using System.IO;
using UnityEngine;

public static class SongLoader
{
	public static Song loadSong(string name)
	{
		double bpm = 120;
		double offset = 0;
		string map = "";
		AudioClip clip = null;

		//string loadString = File.ReadAllText(Application.dataPath + "/Resources/Music/" + name + ".json");

		clip = Resources.Load<AudioClip> ("Music/" + name);
		Debug.Log (clip);

		return new Song (bpm, offset, map, clip);
	}
}