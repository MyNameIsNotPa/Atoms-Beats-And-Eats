using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnionsRecipe : Recipe
{
    //protected List<Event> events;

    public OnionsRecipe(double startSongTime) : base(startSongTime)
    {
        image = Resources.Load<Sprite> ("RecipeIcons/Onions");
        events.Add (new SoundEvent(startSongTime));
        events.Add (new SoundEvent(startSongTime + 0.5));
        events.Add (new SoundEvent(startSongTime + 1));
        events.Add (new HitEvent (startSongTime + 2));
        events.Add (new HitEvent (startSongTime + 2.5));
        events.Add (new HitEvent (startSongTime + 3));
    }
}