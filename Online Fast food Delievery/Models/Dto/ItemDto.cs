namespace Online_Fast_food_Delievery.Models.Dto
{
    public class ItemDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }
         public IFormFile Image { get; set; }


    }
}
