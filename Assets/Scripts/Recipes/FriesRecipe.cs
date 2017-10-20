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
		events.Add (new SoundEvent(startSongTime + 2, Resources.Load<AudioClip>("Sounds/sizzle")));
		events.Add (new SoundEvent(startSongTime + 4, Resources.Load<AudioClip>("Sounds/sizzle")));
		events.Add (new HitEvent(startSongTime + 5));
		events.Add (new SoundEvent(startSongTime + 5, Resources.Load<AudioClip>("Sounds/chop")));
		events.Add (new HitEvent(startSongTime + 6));
		events.Add (new SoundEvent(startSongTime + 6, Resources.Load<AudioClip>("Sounds/chop")));
	}
}


