using System;
using System.Collections.Generic;
using System.Text;

namespace CookBook.Domain.Test
{
    public class Category
    {
        public int Id;
        public int ParentId;
        public string Name;

        public List<Category> Subcategories;

        public Category()
        {

        }

        public Category(int Id, string Name, int ParentId)
        {
            this.Id = Id;
            this.Name = Name;
            this.ParentId = ParentId;
        }
    }
}
