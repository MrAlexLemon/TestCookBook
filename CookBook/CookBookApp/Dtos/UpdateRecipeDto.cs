using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookBookApp.Dtos
{
    public class UpdateRecipeDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public UpdateRecipeDto()
        {
        }

        public UpdateRecipeDto(string name, string description, Guid id)
        {
            Name = name;
            Description = description;
            Id = id;
        }
    }
}
