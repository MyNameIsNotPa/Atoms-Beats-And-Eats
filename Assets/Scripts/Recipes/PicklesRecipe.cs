using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PicklesRecipe : Recipe
{
    //protected List<Event> events;

    public PicklesRecipe(double startSongTime) : base(startSongTime)
    {
        image = Resources.Load<Sprite>("RecipeIcons/Pickles");
        events.Add(new SoundEvent(startSongTime));
		events.Add(new SoundEvent(startSongTime + 1));
        events.Add(new HitEvent(startSongTime + 2));
		events.Add (new SoundEvent(startSongTime + 2, Resources.Load<AudioClip>("Sounds/chop")));
        events.Add(new HitEvent(startSongTime + 3));
		events.Add (new SoundEvent(startSongTime + 3, Resources.Load<AudioClip>("Sounds/chop")));
        events.Add(new HitEvent(startSongTime + 4));
		events.Add (new SoundEvent(startSongTime + 4, Resources.Load<AudioClip>("Sounds/chop")));
        events.Add(new HitEvent(startSongTime + 4.5));
		events.Add (new SoundEvent(startSongTime + 4.5, Resources.Load<AudioClip>("Sounds/chop")));
        events.Add(new HitEvent(startSongTime + 5.5));
		events.Add (new SoundEvent(startSongTime + 5.5, Resources.Load<AudioClip>("Sounds/chop")));
    }
}
