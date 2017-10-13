using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MilkshakeRecipe : Recipe
{
	//protected List<Event> events;

	public MilkshakeRecipe(double startSongTime) : base(startSongTime)
	{
		image = Resources.Load<Sprite> ("RecipeIcons/Milkshake");
		events.Add (new SoundEvent(startSongTime));
		events.Add (new HoldEvent(startSongTime + 2, startSongTime + 6));
	}
}
