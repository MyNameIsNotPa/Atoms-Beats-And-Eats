using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PracticeRecipes : MonoBehaviour
{
	private string recipes;

	public void setRecipes(string recipes)
	{
		this.recipes = recipes;
	}

	public string getRecipes()
	{
		return recipes;
	}
}
