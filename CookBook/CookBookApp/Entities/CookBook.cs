using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookBookApp.Entities
{
    public class CookBook
    {
        public List<Recipe> Recipes { get; private set; }

        public CookBook()
        {
            Recipes = new List<Recipe>();
        }

        public void AddRecipe(Recipe recipe)
        {
            if (Recipes.Any(x => (x.Name == recipe.Name || x.Id == recipe.Id)))
                throw new Exception($"Recipe already exist in this recipebook. Id = {recipe.Id}, Name = {recipe.Name}");

            Recipes.Add(recipe);
        }

        public void RemoveRecipe(Guid recipeId)
        {
            if (Recipes.Any(x => x.Id == recipeId))
                throw new Exception($"Recipe doesnt exist in this recipebook. Id = {recipeId}");
        }
    }
}
