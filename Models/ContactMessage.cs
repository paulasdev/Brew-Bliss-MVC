using System;
using System.Collections.Generic;

namespace BrewBlissApp.Models;

public partial class ContactMessage
{
    public int Id { get; set; }

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Message { get; set; } = null!;

    public DateTime SubmittedAt { get; set; }
}
