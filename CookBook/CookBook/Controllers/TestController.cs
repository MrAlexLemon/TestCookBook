using CookBook.Domain.Entities;
using CookBook.Domain.Entities.Tree;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookBook.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            Tree<TestClass> obj = new Tree<TestClass>();
            var root = new TestClass(1, 1, 1);
            obj.Insert(root, root);
            var node1 = new TestClass(3, 3, 3);
            var node2 = new TestClass(12, 12, 12);
            var node3 = new TestClass(5, 5, 5);
            var node4 = new TestClass(2, 2, 2);
            var node5 = new TestClass(0, 0, 0);


            var node6 = new TestClass(10, 10, 10);
            var node7 = new TestClass(20, 20, 20);
            var node8 = new TestClass(21, 21, 21);
            var node9 = new TestClass(22, 22, 22);
            var node10 = new TestClass(30, 30, 30);

            obj.Insert(root, node1);
            obj.Insert(root, node2);
            obj.Insert(node2, node3);
            obj.Insert(node2, node4);
            obj.Insert(node1, node5);


            obj.Insert(root, node6);
            obj.Insert(root, node7);
            obj.Insert(node7, node8);
            obj.Insert(node7, node9);
            obj.Insert(node7, node10);

            obj.Delete(node4);
            obj.Delete(node3);
            obj.Delete(node2);

            obj.Delete(node6);
            obj.Delete(node8);
            obj.Delete(node10);

            var testNode = new TestClass(4, 4, 4);
            obj.Insert(root, testNode);


            //obj.UpdateNode(root, new TestClass(222, 222, 222));
            //obj.UpdateNode(node1, new TestClass(222,222,222));

            //obj.GetAllPreviousRecipes(node9);
            //obj.GetAllPreviousRecipes(root);
            //var z = obj.Count();

            return Ok(obj);
            //return Ok("Test.");
        }
    }
}
