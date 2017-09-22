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
        events.Add(new HitEvent(startSongTime + 2.5));
    }
}
