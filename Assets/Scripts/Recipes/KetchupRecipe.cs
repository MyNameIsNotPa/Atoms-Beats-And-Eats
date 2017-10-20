using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KetchupRecipe : Recipe
{
    //protected List<Event> events;

    public KetchupRecipe(double startSongTime) : base(startSongTime)
    {
        image = Resources.Load<Sprite>("RecipeIcons/Ketchup");
        events.Add(new SoundEvent(startSongTime));
        events.Add(new HitEvent(startSongTime + 2));
		events.Add (new SoundEvent(startSongTime + 2, Resources.Load<AudioClip>("Sounds/squeeze")));
    }
}
