using System.ComponentModel.DataAnnotations.Schema;

namespace Projet_Finale.Model; 
using System.ComponentModel.DataAnnotations;
public class Car
{
    [Key]
    public Guid Id_car {get;set;} = new Guid();
    
    [Required]
    private string brand{get;set;}
    
    [Required]
    private string model {get;set;}
    
    [Required]
    private int years {get;set;} 
    
    [Required]
    private float pre_tax_prices {get;set;}

    [Required] 
    private float price_including_tax {get;set;}
    
    [Required]
    private string color {get;set;}
    
    [Required]
    private bool is_selling {get;set;}
    
    [Required]
    private bool is_buying {get;set;}
    
    [ForeignKey("fk_client_id")]
    
    public Guid id_client {get; set;}
    
    public Client customer{get; set;}
    
    
    
}