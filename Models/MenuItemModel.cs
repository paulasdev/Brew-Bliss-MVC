namespace BrewBlissApp.Models
{
    public class MenuItemModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; } 
        public decimal Price { get; set; }
        public string Image { get; set; } 
        public IFormFile ImageFile { get; set; } 
    }
}