using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TP2.Models;

public class BaseClass
{
    public Guid Id { get; set; }
    [Required]
    [MinLength(3,ErrorMessage = "Name must be at least 3 characters long")]
    public string Name { get; set; }
}


[Table("Customers")]
public class Customer:BaseClass
{
    [ValidateNever]
    public ICollection<Movie> Movies { get; set; }
    [NotMapped]
    public List<int>? MoviesListId { get; set; }
}

[Table("Genres")]
public class Genre:BaseClass
{
    
    public List<Movie> Movies { get; set; }
    [NotMapped]
    public SelectList MoviesList { get; set; }
}

[Table("Movies")]
public class Movie:BaseClass
{
    public Guid GenreId { get; set; }
    public Genre Genre { get; set; }
}