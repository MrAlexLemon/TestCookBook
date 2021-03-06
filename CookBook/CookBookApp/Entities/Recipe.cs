using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace CookBookApp.Entities
{
    public class Recipe
    {
        public Guid Id { get; private set; }
        public Guid? ParentId { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public IList<Recipe> Children { get; private set; }

        public Recipe()
        {
            Id = Guid.NewGuid();
            Name = "";
            Description = "";
            CreatedDate = DateTime.UtcNow;
            ParentId = null;
            Children = new List<Recipe>();
        }

        public Recipe(string name, string description, Guid? parrentId)
        {
            Id = Guid.NewGuid();
            UpdateData(name, description);
            CreatedDate = DateTime.UtcNow;
            ParentId = parrentId;
            Children = new List<Recipe>();
        }

        public void AddChildren(List<Recipe> recipes)
        {
            foreach (var item in recipes)
                Children.Add(item);
        }

        public void UpdateData(string name, string description)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(description))
                throw new Exception($"Validation exception. Name: {name}, Description: {description}");
            Name = name;
            Description = description;
        }
    }
}
