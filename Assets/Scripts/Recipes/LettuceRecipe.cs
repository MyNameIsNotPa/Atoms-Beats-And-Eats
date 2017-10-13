using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LettuceRecipe : Recipe
{
	//protected List<Event> events;

	public LettuceRecipe(double startSongTime) : base(startSongTime)
	{
		image = Resources.Load<Sprite> ("RecipeIcons/Lettuce");
		events.Add (new SoundEvent(startSongTime));
		events.Add (new SoundEvent(startSongTime + 1.5));
		events.Add (new HitEvent (startSongTime + 2));
		events.Add (new HitEvent (startSongTime + 3.5));
	}
}
