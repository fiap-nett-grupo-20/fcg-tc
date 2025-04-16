// Models/ApplicationUser.cs
using Microsoft.AspNetCore.Identity;

public class User : IdentityUser
{
    // Add your custom properties here
    public required string NameUser { get; set; }

    // public string? FirstName { get; set; }
    // public string? LastName { get; set; }
    // public DateTime? DateOfBirth { get; set; }
}