using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CookBook.Domain.Test
{
    public class TestClass
    {
        public TestClass()
        {
            var categories = new List<Category>() {
            new Category(1, "Sport", 0),
            new Category(2, "Balls", 1),
            new Category(3, "Shoes", 1),
            new Category(4, "Electronics", 0),
            new Category(5, "Cameras", 4),
            new Category(6, "Lenses", 5),
            new Category(7, "Tripod", 5),
            new Category(8, "Computers", 4),
            new Category(9, "Laptops", 8),
            new Category(10, "Empty", 0),
            new Category(-1, "Broken", 999)
            };

            var root = categories.GenerateTree(c => c.Id, c => c.ParentId);

            Test(root);
        }

        public void Test(IEnumerable<TreeItem<Category>> categories, int deep = 0)
        {
            foreach (var c in categories)
            {
                Console.WriteLine(new String('\t', deep) + c.Item.Name);
                Test(c.Children, deep + 1);
            }
        }
    }
}
