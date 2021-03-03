using CookBook.Domain.Entities.Tree;
using System;
using System.Collections.Generic;
using System.Text;

namespace CookBook.Domain.Entities
{
    public class CookBook<T> where T : IComparable
    {
        private SortedSet<Tree<T>> recipes;

        public CookBook()
        {
            recipes = new SortedSet<Tree<T>>();
        }

        public SortedSet<Tree<T>> GetRecipes()
        {
            return recipes;
        }

        public void AddRecipe(Tree<T> recipe)
        {
            recipes.Add(recipe);
        }
    }
}
