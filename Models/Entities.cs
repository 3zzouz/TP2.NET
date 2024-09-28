using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TP2.Models;

public class BaseClass
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
}


public class Customer:BaseClass
{
    [ValidateNever]
    public ICollection<Movie> Movies { get; set; }
    [NotMapped]
    public List<int>? MoviesListId { get; set; }
}

public class Genre:BaseClass
{
    public Guid Id { get; set; }
    [ValidateNever]
    public List<Movie> Movies { get; set; }
}

public class Movie:BaseClass
{
    public Guid GenreId { get; set; }
    public Genre? Genre { get; set; }
    [ValidateNever]
    public ICollection<Customer> Customers { get; set; }
}