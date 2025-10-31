using System.ComponentModel.DataAnnotations;

namespace Projet_Finale.Model;

public class Client
{
    [key]
    public Guid id_client { get; set; } = new.guid();
    
    [Required]
    private string FirstName {get; set;}
    
    [Required]
    private string LastName {get; set;}
    
    [Required]
    private DateTime BirthDate {get; set;}
    
    [Required]
    private int PhoneNumber {get; set;}
    
    [Required]
    private string Email {get; set;}
    
}