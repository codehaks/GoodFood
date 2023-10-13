using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace GoodFood.Infrastructure.Persistence.Models;
public class ApplicationUser : IdentityUser
{
    [MaxLength(50)]
    public string? FirstName { get; set; }

    [MaxLength(50)]
    public string? LastName { get; set; }

    [NotMapped]
    public string FullName => FirstName + " " + LastName;
}
