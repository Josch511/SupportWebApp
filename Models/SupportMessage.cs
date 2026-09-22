using System.ComponentModel.DataAnnotations;

namespace SupportWebApp.Models;

public class SupportMessage
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Required(ErrorMessage = "Navn er påkrævet.")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email er påkrævet.")]
    [EmailAddress(ErrorMessage = "Indtast en gyldig emailadresse.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Telefonnummer er påkrævet.")]
    public string Phone { get; set; } = string.Empty;

    [Required(ErrorMessage = "Beskrivelse er påkrævet.")]
    public string Description { get; set; } = string.Empty;

    [Required(ErrorMessage = "Kategori er påkrævet.")]
    public string Category { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}