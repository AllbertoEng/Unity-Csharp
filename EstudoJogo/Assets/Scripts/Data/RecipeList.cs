using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Data/RecipeList")]
public class RecipeList : ScriptableObject
{
    public List<CraftingRecipe> recipes;
}
