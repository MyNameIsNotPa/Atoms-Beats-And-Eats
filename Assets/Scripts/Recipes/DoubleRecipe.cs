using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleRecipe : Recipe
{
	//protected List<Event> events;

	public DoubleRecipe(double startSongTime) : base(startSongTime)
	{
		image = Resources.Load<Sprite> ("RecipeIcons/Double");
		events.Add (new SoundEvent(startSongTime));
		events.Add (new SoundEvent(startSongTime + 1));
		events.Add (new HoldEvent(startSongTime + 2, startSongTime + 3));
	}
}
