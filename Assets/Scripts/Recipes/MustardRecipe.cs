using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MustardRecipe : Recipe
{
    //protected List<Event> events;

    public MustardRecipe(double startSongTime) : base(startSongTime)
    {
        image = Resources.Load<Sprite>("RecipeIcons/Mustard");
        events.Add(new SoundEvent(startSongTime));
		events.Add (new SoundEvent(startSongTime + 3, Resources.Load<AudioClip>("Sounds/squeeze")));
    }
}
