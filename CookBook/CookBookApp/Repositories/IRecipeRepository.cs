using CookBookApp.Dtos;
using CookBookApp.Entities;
using System;
using System.Collections.Generic;

namespace CookBookApp.Repositories
{
    public interface IRecipeRepository
    {
        // Get all recipe roots from cookboo==List<Recipe>(); List == trees
        IList<Recipe> GetRoots();

        // Get recipe root of cookbook by predicate
        Recipe GetRoot(Func<Recipe, bool> predicate);

        // Get all sorted recipe trees for cookbook
        IList<Recipe> GetAllRecipes();

        // Get sorted child nodes for some recipe
        IList<Recipe> GetChildRecipes(Guid parentId);

        // Find recipe by id in all trees
        Recipe GetNodeById(Guid nodeId);

        // Fork from existed recipe
        void ForkRecipe(Guid rootId, CreateRecipeDto recipe);

        // Update existed recipe
        void UpdateRecipe(UpdateRecipeDto updatedRecipe);

        // Remove existed recipe
        void RemoveRecipe(Guid recipeId);

        // Find recipe in tree|subtree
        Recipe FindRecipeInTree(Recipe node, Func<Recipe, bool> predicate);
    }
}
