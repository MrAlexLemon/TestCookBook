using System;
using System.Collections.Generic;
using System.Text;

namespace CookBook.Domain.Entities
{
    public class Recipe : IComparable
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }

        public Recipe(string name, string description, DateTime createdDate)
        {
            Name = name;
            Description = description;
            CreatedDate = createdDate;
        }

        public int CompareTo(object obj)
        {
            var qq = obj.GetType().Name;
            Recipe p = obj as Recipe;
            if (p is null)
                throw new Exception("Can't compare two objects.");

            int result = this.Name.CompareTo(p.Name);


            if (result > 0)
                return 1;
            else if (result < 0)
                return -1;


            if (result == 0)
                result = this.Description.CompareTo(p.Description);

            if (result > 0)
                return 1;
            else if (result < 0)
                return -1;

            if (result == 0)
                result = this.CreatedDate.CompareTo(p.CreatedDate);

            if (result > 0)
                return 1;
            else if (result < 0)
                return -1;

            return result;
        }

    }
}
