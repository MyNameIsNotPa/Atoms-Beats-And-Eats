using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleRecipe : Recipe
{
	//protected List<Event> events;

	public TripleRecipe(double startSongTime) : base(startSongTime)
	{
		image = Resources.Load<Sprite> ("RecipeIcons/Triple");
		events.Add (new SoundEvent(startSongTime));
		events.Add (new SoundEvent(startSongTime + 1));
		events.Add (new SoundEvent(startSongTime + 2));
		events.Add (new HitEvent (startSongTime + 4));
		events.Add (new HitEvent (startSongTime + 5));
		events.Add (new HitEvent (startSongTime + 6));
	}
}