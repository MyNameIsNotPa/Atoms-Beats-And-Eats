using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Practice : MonoBehaviour
{
	[System.Serializable]
	public class NewRecipeEvent : UnityEvent<Recipe>{};

	[TextArea(5, 50)]
	public string recipes;

	public NewRecipeEvent newRecipe;

	private List<string> recipeList;
	private int successes;

	private Engine engine;

	private int nextOrder;

	private RecipeFactory recipeFactory;

	public static Recipe firstRecipe;

	void Start ()
	{
		if (GameObject.FindGameObjectWithTag("PracticeRecipes"))
		{
			GameObject practiceRecipes = GameObject.FindGameObjectWithTag ("PracticeRecipes");
			recipes = practiceRecipes.GetComponent<PracticeRecipes> ().getRecipes ();
			GameObject.Destroy (practiceRecipes);
		}
		recipeList = new List<string>(recipes.Split ('\n'));
		nextOrder = 0;
		successes = 0;
		engine = GameObject.FindGameObjectWithTag ("Engine").GetComponent<Engine> ();
		recipeFactory = new RecipeFactory ();
		firstRecipe = recipeFactory.create (recipeList [0], 0);
	}

	public void onBeatUpdate(float songTime)
	{
		int now = Mathf.FloorToInt (songTime);
		if (now > 0 && now % 8 == 0 && now > nextOrder)
		{
			nextOrder = now + 8;
			List<Recipe> recipes = new List<Recipe> ();

			recipes.Add(recipeFactory.create(recipeList[0], nextOrder));

			Order order = new Order (nextOrder, recipes);

			engine.addOrder (order);
		}
	}

	public void onOrderEnd(ORDER_RESULT result)
	{
		if (result != ORDER_RESULT.FAILURE)
		{
			successes++;
			if (successes == 3)
			{
				recipeList.RemoveAt (0);
				if (recipeList.Count == 0)
				{
					// Done practicing
					engine.finishSong();
				}
				else
				{
					newRecipe.Invoke (recipeFactory.create (recipeList [0], 0));
				}
				successes = 0;
			}
		}
	}
}
