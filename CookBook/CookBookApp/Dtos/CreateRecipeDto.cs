using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookBookApp.Dtos
{
    public class CreateRecipeDto
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public CreateRecipeDto()
        {
        }

        public CreateRecipeDto(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}
