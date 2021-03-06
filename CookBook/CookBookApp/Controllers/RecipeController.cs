using CookBookApp.Dtos;
using CookBookApp.Entities;
using CookBookApp.Helpers;
using CookBookApp.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookBookApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecipeController : ControllerBase
    {
        private readonly IRecipeRepository _recipeRepository;
        public RecipeController(IRecipeRepository recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }

        [HttpGet("test")]
        public async Task<IActionResult> Test()
        {
            var recipe1 = new Recipe("Recipe1", "Description1", null);
            var recipe2 = new Recipe("Recipe2", "Description2", recipe1.Id);
            var recipe3 = new Recipe("Recipe3", "Description3", recipe1.Id);
            var recipe4 = new Recipe("Recipe4", "Description4", recipe2.Id);
            var recipe5 = new Recipe("Recipe5", "Description5", recipe2.Id);
            var recipe6 = new Recipe("Recipe6", "Description6", recipe4.Id);
            var recipe7 = new Recipe("Recipe7", "Description7", recipe6.Id);

            recipe1.AddChildren(new List<Recipe> { recipe2, recipe3 });
            recipe2.AddChildren(new List<Recipe> { recipe4, recipe5 });
            recipe4.AddChildren(new List<Recipe> { recipe6 });
            recipe6.AddChildren(new List<Recipe> { recipe7 });

            var listToSortHierarchically = new List<Recipe> { recipe1, recipe2, recipe3, recipe4, recipe5, recipe6, recipe7 };

            return Ok(listToSortHierarchically);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllRecipes()
        {
            return Ok(_recipeRepository.GetAllRecipes());
        }

        [HttpGet("roots")]
        public async Task<IActionResult> GetRoots()
        {
            return Ok(_recipeRepository.GetRoots());
        }

        [HttpGet("root/{id:Guid}")]
        public async Task<IActionResult> GetRoot([FromRoute] Guid id)
        {
            return Ok(_recipeRepository.GetRoot(x=>x.Id.Equals(id)));
        }

        [HttpGet("node/{id:Guid}")]
        public async Task<IActionResult> GetNode([FromRoute] Guid id)
        {
            return Ok(_recipeRepository.GetNodeById(id));
        }

        [HttpPut("node")]
        public async Task<IActionResult> UpdateNode([FromBody] UpdateRecipeDto recipe)
        {
            _recipeRepository.UpdateRecipe(recipe);
            return Ok();
        }

        /*[HttpDelete("node/{id:Guid}")]
        public async Task<IActionResult> DeleteNode([FromRoute] Guid id)
        {
            _recipeRepository.RemoveRecipe(id);
            return Ok();
        }*/

        [HttpGet("{id:Guid}/children")]
        public async Task<IActionResult> GetChildreNode([FromRoute] Guid id)
        {
            return Ok(_recipeRepository.GetChildRecipes(id));
        }

        [HttpPost("{id:Guid}")]
        public async Task<IActionResult> ForkRecipe([FromRoute] Guid id, [FromBody] CreateRecipeDto recipe)
        {
            _recipeRepository.ForkRecipe(id, recipe);
            return Ok();
        }
    }
}
