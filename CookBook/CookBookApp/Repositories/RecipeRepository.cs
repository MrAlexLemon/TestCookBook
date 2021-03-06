using CookBookApp.Dtos;
using CookBookApp.Entities;
using CookBookApp.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookBookApp.Repositories
{
    public class RecipeRepository : IRecipeRepository
    {
        private IList<Recipe> recipes;
        public RecipeRepository()
        {
            var recipe1 = new Recipe("Recipe1", "Description1", null);
            var recipe2 = new Recipe("Recipe2", "Description2", recipe1.Id);
            var recipe3 = new Recipe("Recipe3", "Description3", recipe1.Id);
            var recipe4 = new Recipe("Recipe4", "Description4", recipe2.Id);
            var recipe5 = new Recipe("Recipe5", "Description5", recipe2.Id);
            var recipe6 = new Recipe("Recipe6", "Description6", recipe4.Id);
            var recipe7 = new Recipe("Recipe7", "Description7", recipe6.Id);

            recipe1.AddChildren(new List<Recipe> { recipe2, recipe3 });
            recipe2.AddChildren(new List<Recipe> { recipe4, recipe5 });
            recipe4.AddChildren(new List<Recipe> { recipe6 });
            recipe6.AddChildren(new List<Recipe> { recipe7 });

            recipes = new List<Recipe> { recipe1, recipe2, recipe3, recipe4, recipe5, recipe6, recipe7 };
        }


        public IList<Recipe> GetRoots()
        {
            return recipes.Where(x => x.ParentId is null).ToList();
        }

        public Recipe GetRoot(Func<Recipe, bool> predicate)
        {
            return recipes.Where(x => x.ParentId is null).Where(predicate).FirstOrDefault();
        }

        public IList<Recipe> GetAllRecipes()
        {
            return recipes.Ordered().ToList();
        }

        public IList<Recipe> GetChildRecipes(Guid parentId)
        {
            var res = GetNodeById(parentId).Children.OrderBy(x=>x.Name).ToList();
            return res;
        }


        public Recipe GetNodeById(Guid nodeId)
        {
            var node = GetRoots().FirstOrDefault(item => !(FindRecipeInTree(item, x => x.Id.Equals(nodeId)) is null));
            if(node is null)
                throw new Exception($"Recipe with Id {nodeId} wasnt found.");

            return node;
        }


        public void ForkRecipe(Guid rootId, CreateRecipeDto recipe)
        {
            var currentRecipe = GetNodeById(rootId);
            if(currentRecipe.Children.Any(x => (x.Name == recipe.Name)))
                throw new Exception($"Recipe already exsts. Name: {recipe.Name}");

            currentRecipe.Children.Add(new Recipe(recipe.Name, recipe.Description, rootId));
        }

        public void RemoveRecipe(Guid recipeId)
        {
            var currentRecipe = GetNodeById(recipeId);
            if (currentRecipe.Children.Count()>0)
                throw new Exception($"Recipe with Id: {recipeId} cant be deleted. Has children recipes.");

            Recipe root = null;

            if (!currentRecipe.ParentId.HasValue)
                root = GetRoot(x => x.Id == recipeId);
            else
                root = GetNodeById(currentRecipe.ParentId.Value);

            recipes.Remove(currentRecipe);
        }

        public void UpdateRecipe(UpdateRecipeDto updatedRecipe)
        {
            GetNodeById(updatedRecipe.Id).UpdateData(updatedRecipe.Name, updatedRecipe.Description);
        }

        public Recipe FindRecipeInTree(Recipe node, Func<Recipe, bool> predicate)
        {
            if (predicate(node))
                return node;

            foreach (var n in node.Children.AsParallel())
            {
                var found = FindRecipeInTree(n, predicate);
                if (found != default(Recipe))
                    return found;
            }
            return default(Recipe);
        }
    }
}
