using System.ComponentModel.DataAnnotations;

namespace PlaygroundDotNetAPI.Models;

public class Employee
{
    [Key]
    public int Id  { get; set; }

    [Required]
    [StringLength(maximumLength: 100, MinimumLength = 2)]
    public required string Name { get; set; }
}