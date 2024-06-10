using FastFoodModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace Online_Fast_food_Delievery.Models.Dto
{
    public class SubCategoryDto
    {
        public int Id { get; set; }
 
        public string Title { get; set; }
        public int CategoryId { get; set; }
    }
}
