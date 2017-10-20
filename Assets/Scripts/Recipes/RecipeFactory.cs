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
		case "Triple":
			return new TripleRecipe (startTime);
		case "Lettuce":
			return new LettuceRecipe (startTime);
        case "Onions":
            return new OnionsRecipe (startTime);
        case "Mustard":
            return new MustardRecipe (startTime);
        case "Cheese":
            return new CheeseRecipe(startTime);
        case "Ketchup":
            return new KetchupRecipe(startTime);
        case "Pickles":
            return new PicklesRecipe(startTime);
        case "Tomatoes":
            return new TomatoesRecipe(startTime);
		case "Milkshake":
			return new MilkshakeRecipe(startTime);
		case "Fries":
			return new FriesRecipe(startTime);
        default:
			return null;
		}
	}
}
