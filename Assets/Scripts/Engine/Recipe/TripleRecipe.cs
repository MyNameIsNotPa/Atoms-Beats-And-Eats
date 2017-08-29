using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleRecipe : Recipe
{
	//protected List<Event> events;

	public TripleRecipe(double startSongTime) : base(startSongTime)
	{
		image = Resources.Load<Sprite> ("RecipeIcons/Double");
		events.Add (new TextEvent (startSongTime, "Triple!"));
		events.Add (new TextEvent (startSongTime + 1, ""));
		events.Add (new HitEvent (startSongTime + 2));
		events.Add (new HitEvent (startSongTime + 3));
		events.Add (new HitEvent (startSongTime + 4));
	}
}
