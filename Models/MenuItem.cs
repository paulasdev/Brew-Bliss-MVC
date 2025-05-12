using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace BrewBlissApp.Models;

public partial class MenuItem
{
    public int MenuItemId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public int? CategoryId { get; set; }


    public string? ImagePath { get; set; }

    public virtual Category? Category { get; set; }


    [NotMapped]
    public IFormFile? ImageFile { get; set; }
}