using System.ComponentModel.DataAnnotations;

namespace leca_api.Models;

public class User
{
    [Key]
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
}