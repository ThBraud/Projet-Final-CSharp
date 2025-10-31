using System.ComponentModel.DataAnnotations;

namespace Projet_Finale.Model;

public class Client
{
    [Key]
    public Guid id_client { get; set; } = new Guid();

    [Required] 
    private string firstName { get; set; }

    [Required] 
    private string lastName { get; set; }

    [Required] 
    private DateTime birthDate { get; set; }

    [Required] 
    private string phoneNumber { get; set; }

    [Required] 
    private string email { get; set; }

    
    #region Accessors
    
    public string FirstName
    {
        get => firstName;
        set => firstName = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string LastName
    {
        get => lastName;
        set => lastName = value ?? throw new ArgumentNullException(nameof(value));
    }

    public DateTime BirthDate
    {
        get => birthDate;
        set => birthDate = value;
    }

    public string PhoneNumber
    {
        get => phoneNumber;
        set => phoneNumber = value;
    }

    public string Email
    {
        get => email;
        set => email = value ?? throw new ArgumentNullException(nameof(value));
    }
    #endregion
}