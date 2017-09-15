// Class used to make recipes.

public class RecipeFactory
{
	public Recipe create(string name, double startTime)
	{
		switch (name)
		{
		case "Single":
			return new SingleRecipe (startTime);
		case "Double":
			return new DoubleRecipe (startTime);
		case "Lettuce":
			return new LettuceRecipe (startTime);
		default:
			return null;
		}
	}
}
