using System;
using System.Collections.Generic;

namespace TP2.Models;

public partial class Movie
{
    public Guid Id { get; set; }

    public Guid GenreId { get; set; }

    public string? Name { get; set; }

    public virtual Genre Genre { get; set; } = null!;
}
