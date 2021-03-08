using CookBookApp.Dtos;
using CookBookApp.Entities;
using CookBookApp.Helpers;
using CookBookApp.Repositories;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace CookBook.Tests
{
    public class RecipeTests
    {
        private readonly RecipeRepository _recipeRepository;
        private readonly Mock<IRecipeRepository> _recipeRepositoryMock = new Mock<IRecipeRepository>();

        public RecipeTests()
        {
            _recipeRepository = new RecipeRepository();

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

            var recipes = new List<Recipe> { recipe1, recipe2, recipe3, recipe4, recipe5, recipe6, recipe7 };

            _recipeRepository.Recipes = recipes;

            _recipeRepositoryMock.Setup(x => x.Recipes).Returns(recipes);
        }

        [Fact]
        public void GetChildrenRecipesShouldBeOrdered()
        {
            foreach (var item in _recipeRepository.Recipes)
            {
                var children = _recipeRepository.GetChildRecipes(item.Id);
                children.Should().BeInAscendingOrder(x => x.Name);
                children.Should().NotBeNull();
                children.Should().OnlyHaveUniqueItems();
            }
        }

        [Fact]
        public void GetChildrenRecipesShouldThrowException()
        {
            var id = Guid.NewGuid();
            _recipeRepository.Invoking(x => x.GetChildRecipes(id)).Should().Throw<Exception>().WithMessage($"Recipe with Id {id} wasnt found.");
        }

        [Fact]
        public void GetRootsShouldBeOrdered()
        {
            var roots = _recipeRepository.GetRoots();

            roots.Should().BeInAscendingOrder();
            roots.Should().NotBeNull();
            roots.Should().OnlyHaveUniqueItems();
        }

        [Fact]
        public void GetRootShouldBeNotNull()
        {
            var rootId = _recipeRepository.GetAllRecipes()[0].Id;

            var root = _recipeRepository.GetRoot(x=>x.Id.Equals((rootId)));

            root.ParentId.Should().BeNull();
            root.Should().NotBeNull();
            root.Id.Should().Equals(rootId);
        }

        [Fact]
        public void GetRootShouldBeNull()
        {
            var root = _recipeRepository.GetRoot(x => x.Id.Equals((Guid.NewGuid())));
            root.Should().BeNull();
        }

        [Fact]
        public void GetAllRecipesShouldBeNotNull()
        {
            var recipes = _recipeRepository.GetAllRecipes();

            recipes.Should().NotBeNull();
            recipes.Should().NotBeEmpty();
        }


        [Fact]
        public void GetNodeByIdShouldThrowException()
        {
            var id = Guid.NewGuid();
            _recipeRepository.Invoking(x => x.GetNodeById(id)).Should().Throw<Exception>().WithMessage($"Recipe with Id {id} wasnt found.");
        }

        [Fact]
        public void GetNodeByIdShouldReturnExistedNode()
        {
            var id = _recipeRepository.Recipes[0].Id;

            var recipe = _recipeRepository.GetNodeById(id);

            recipe.Should().NotBeNull();
            recipe.Id.Should().Equals(id);
        }

        [Fact]
        public void ForkRecipeShouldThrowException()
        {
            var id = _recipeRepository.Recipes[0].Id;
            var newRecipe = new CreateRecipeDto(_recipeRepository.Recipes[0].Name, _recipeRepository.Recipes[0].Description);

            _recipeRepository.Invoking(x => x.ForkRecipe(id, newRecipe)).Should().Throw<Exception>().WithMessage($"Recipe already exsts. Name: {newRecipe.Name}");
        }

        [Fact]
        public void ForkRecipeShouldBeCreated()
        {
            var id = _recipeRepository.Recipes[0].Id;
            var name = "testRecipe";
            var description = "testDescription";
            var newRecipe = new CreateRecipeDto(name, description);

            _recipeRepository.ForkRecipe(id, newRecipe);

            _recipeRepository.GetAllRecipes().Should().Contain(x=>x.Name == name && x.Description == description);
        }

        [Fact]
        public void RemoveRecipeShouldThrowException()
        {
            var id = _recipeRepository.Recipes[0].Id;

            _recipeRepository.Invoking(x => x.RemoveRecipe(id)).Should().Throw<Exception>().WithMessage($"Recipe with Id: {id} cant be deleted. Has children recipes.");
        }

        [Fact]
        public void RemoveRecipeShouldThrowNotFoundException()
        {
            var id = Guid.NewGuid();

            _recipeRepository.Invoking(x => x.RemoveRecipe(id)).Should().Throw<Exception>().WithMessage($"Recipe with Id {id} wasnt found.");
        }

        [Fact]
        public void UpdateRecipeShouldModifyNode()
        {
            var id = _recipeRepository.Recipes[0].Id;
            var updatedName = "updatedName";
            var updatedDescription = "updatedDescription";
            var updatedRecipe = new UpdateRecipeDto(updatedName, updatedDescription, id);

            _recipeRepository.Invoking(x => x.UpdateRecipe(updatedRecipe)).Should().NotThrow();
            _recipeRepository.GetAllRecipes().Should().Contain(x => x.Name == updatedName && x.Description == updatedDescription);
        }

        [Fact]
        public void UpdateRecipeShouldThrowException()
        {
            var id = _recipeRepository.Recipes[0].Id;
            var updatedName = "";
            var updatedDescription = "updatedDescription";
            var updatedRecipe = new UpdateRecipeDto(updatedName, updatedDescription, id);

            _recipeRepository.Invoking(x => x.UpdateRecipe(updatedRecipe)).Should().Throw<Exception>().WithMessage($"Validation exception. Name: {updatedName}, Description: {updatedDescription}");
        }

        [Fact]
        public void FindRecipeInTreeShouldReturnNull()
        {
            var recipe = _recipeRepository.Recipes[0];
            var testId = Guid.NewGuid();

            _recipeRepository.Invoking(x => x.FindRecipeInTree(recipe, x => x.Id == testId).Should().BeNull());
        }

        [Fact]
        public void FindRecipeInTreeShouldReturnNullExistedRecipe()
        {
            var recipe = _recipeRepository.Recipes[0];

            _recipeRepository.Invoking(x => x.FindRecipeInTree(recipe, x => x.Id == recipe.Id)).Should().NotBeNull();
        }
    }
}
