using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleRecipe : Recipe
{
	//protected List<Event> events;

	public SingleRecipe(double startSongTime) : base(startSongTime)
	{
		image = Resources.Load<Sprite> ("RecipeIcons/Single");
		events.Add (new TextEvent (startSongTime, "Single!"));
		events.Add (new TextEvent (startSongTime + 1, ""));
		events.Add (new HitEvent (startSongTime + 2));
	}
}