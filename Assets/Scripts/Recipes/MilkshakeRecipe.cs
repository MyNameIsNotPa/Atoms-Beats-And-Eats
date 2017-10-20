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
		events.Add (new SoundEvent(startSongTime + 2, Resources.Load<AudioClip>("Sounds/pour1")));
		events.Add (new SoundEvent(startSongTime + 4, Resources.Load<AudioClip>("Sounds/pour2")));
		events.Add (new SoundEvent(startSongTime + 6, Resources.Load<AudioClip>("Sounds/clink")));
	}
}
