using System;
using System.Collections.Generic;

namespace BrewBlissApp.Models;

public partial class MenuItem
{
    public int MenuItemId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public string? ImagePath { get; set; }

    public int? CategoryId { get; set; }

    public byte[]? ImageData { get; set; }

    public virtual Category? Category { get; set; }
}
