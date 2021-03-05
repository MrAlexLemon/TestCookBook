using CookBook.Domain.Entities.Tree;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public void Update(Guid treeId)
        {
            return recipes.AsQueryable().Where(x => x.GetRootInfo().FirstOrDefault().Id == treeId).FirstOrDefault();
        }
    }
}
