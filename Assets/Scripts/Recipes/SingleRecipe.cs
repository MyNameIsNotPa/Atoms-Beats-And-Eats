using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleRecipe : Recipe
{
	//protected List<Event> events;

	public SingleRecipe(double startSongTime) : base(startSongTime)
	{
		image = Resources.Load<Sprite> ("RecipeIcons/Single");
		events.Add (new SoundEvent(startSongTime));
		events.Add (new HitEvent (startSongTime + 2));
	}
}