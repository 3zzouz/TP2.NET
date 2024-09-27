using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TP2.Models;

public class Customer
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public ICollection<Movie>? Movies { get; set; }
    [NotMapped]
    public List<int>? MoviesList { get; set; }
}

public class Genre
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<Movie>? Movies { get; set; }
}

public class Movie
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public Guid GenreId { get; set; }
    public Genre? Genre { get; set; }
    public ICollection<Customer> Customers { get; set; }
}

public class GenreViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<int>? SelectedMovieIds { get; set; }    
    public List<SelectListItem>? Movies { get; set; }
}