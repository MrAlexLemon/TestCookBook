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
