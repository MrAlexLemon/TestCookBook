using CookBook.Domain.Entities;
using CookBook.Domain.Entities.Tree;
using CookBook.Domain.Test.v3;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace CookBook.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly Tree<TestClass> _testClasses;
        public TestController(Tree<TestClass> testClasses)
        {
            _testClasses = testClasses;
        }

        [HttpGet("root")]
        public async Task<IActionResult> GetRootInfo()
        {
            var res = _testClasses.GetRootInfo();
            return Ok(res);
        }

        [HttpPost("post")]
        public async Task<IActionResult> GetNodeInfo([FromBody] TestClass body)
        {
            var res = _testClasses.GetChildrenNodeDto(body);
            return Ok(res);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRecipes()
        {
            return Ok(_testClasses);
        }

        [HttpGet("test")]
        public async Task<IActionResult> Test()
        {
            CookBook.Domain.Test.TestClass obj = new Domain.Test.TestClass();




            CookBook.Domain.Test.v2.Document testobj = new Domain.Test.v2.Document();
            var documents = new[]
            {
                new CookBook.Domain.Test.v2.Document() { id = 1, ParentId = null, name = "fruits" },
                new CookBook.Domain.Test.v2.Document() { id = 2, ParentId = null, name = "vegetables" },
                new CookBook.Domain.Test.v2.Document() { id = 3, ParentId = 2, name = "tomatoes" },
                new CookBook.Domain.Test.v2.Document() { id = 4, ParentId = 1, name = "apples" },
                new CookBook.Domain.Test.v2.Document() { id = 5, ParentId = 4, name = "golden apples" },
                new CookBook.Domain.Test.v2.Document() { id = 6, ParentId = 4, name = "ared apples" },
            };

            var lookup = documents.ToLookup(x => x.ParentId);

            Func<int?, IEnumerable<CookBook.Domain.Test.v2.Document>> heirarchySort = null;
            heirarchySort = pid =>
                lookup[pid].SelectMany(x => new[] { x }.Concat(heirarchySort(x.id)));

            IEnumerable<CookBook.Domain.Test.v2.Document> sorted = heirarchySort(null);
            Console.WriteLine(sorted);





            var A = new CookBook.Domain.Test.v3.TreeItem { Id = 1, Name = "A", ParentId=null };
            var B = new CookBook.Domain.Test.v3.TreeItem { Id = 22, Name = "F", ParentId = 1 };
            var C = new CookBook.Domain.Test.v3.TreeItem { Id = 33, Name = "C", ParentId = 1 };
            var D = new CookBook.Domain.Test.v3.TreeItem { Id = 4, Name = "D", ParentId = null };
            var E = new CookBook.Domain.Test.v3.TreeItem { Id = 5, Name = "E", ParentId = 4 };

            // populate children for the example.
            // My actual code is automatic thanks to EF Inverse Relationship.
            A.Children = new List<CookBook.Domain.Test.v3.TreeItem> { B, C };
            D.Children = new List<CookBook.Domain.Test.v3.TreeItem> { E };

            var listToSortHierarchically = new List<CookBook.Domain.Test.v3.TreeItem> { D, C, B, E, A };
            //listToSortHierarchically.Remove(A);
            // I want the result of the hierarchical sort to be A B C D E

            //var orderedList = listToSortHierarchically.Ordered<CookBook.Domain.Test.v3.TreeItem>().ToList();
            var orderedList1 = TreeItemExtension.SortedList(listToSortHierarchically).ToList();

            /*var ordered = listToSortHierarchically.OrderBy(x => x.Name).ToList();
            ordered.ForEach(TreeItemExtension.OrderChildren);*/


            var qq = orderedList1.Find(x=>x.Id == 33);

            return Ok(orderedList1);
        }

        /*[HttpPut("node")]
        public async Task<IActionResult> UpdateRecipe([FromBody] TestClass body)
        {
            return Ok(_testClasses.UpdateNode());
        }

        [HttpPost("newnode")]
        public async Task<IActionResult> CreateNewRecipe([FromBody] TestClass body)
        {
            return Ok(_testClasses);
        }

        [HttpDelete("node")]
        public async Task<IActionResult> DeleteRecipes([FromBody] TestClass body)
        {
            return Ok(_testClasses);
        }*/
    }
}
