using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheeseRecipe : Recipe
{
    //protected List<Event> events;

    public CheeseRecipe(double startSongTime) : base(startSongTime)
    {
        image = Resources.Load<Sprite>("RecipeIcons/Cheese");
        events.Add(new SoundEvent(startSongTime));
		events.Add(new SoundEvent(startSongTime + 2));
		events.Add(new SoundEvent(startSongTime + 4));
        events.Add(new HitEvent(startSongTime + 5));
		events.Add (new SoundEvent(startSongTime + 5, Resources.Load<AudioClip>("Sounds/chop")));
    }
}
