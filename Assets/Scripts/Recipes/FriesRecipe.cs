using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FriesRecipe : Recipe
{
	public FriesRecipe(double startSongTime) : base(startSongTime)
	{
		image = Resources.Load<Sprite> ("RecipeIcons/Fries");
		events.Add (new SoundEvent (startSongTime));
		events.Add (new HoldEvent(startSongTime + 2, startSongTime + 4));
	}
}


