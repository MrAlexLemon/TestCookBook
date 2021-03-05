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
