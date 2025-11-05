#region Using Directives
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
#endregion

namespace Projet_Finale.Model; 
public class Car
{
    [Key]
    public Guid Id_car {get;set;} = new Guid();

    
    private string brand {get; set;}

     
    private string model {get; set;}

    
    private int years {get; set;}

    
    private float pre_tax_prices {get; set;}

     
    private float price_including_tax {get; set;}

     
    private string color {get; set;}


    private bool is_selling {get; set;}
    
    
    [ForeignKey("fk_client_id")]
    
    public Guid? id_client {get; set;}
    
    public Client? Client{get; set;}

    #region Accesors
    
    public string Brand
    {
        get => brand;
        set => brand = value ?? throw new ArgumentNullException(nameof(value));
    }
    [Required] 
    public string Model
    {
        get => model;
        set => model = value ?? throw new ArgumentNullException(nameof(value));
    }
    [Required] 
    public int Years
    {
        get => years;
        set => years = value;
    }
    [Required] 
    public float PreTaxPrices
    {
        get => pre_tax_prices;
        set => pre_tax_prices = value;
    }
    [Required] 
    public float PriceIncludingTax
    {
        get => price_including_tax;
        set => price_including_tax = value;
    }
    [Required] 
    public string Color
    {
        get => color;
        set => color = value ?? throw new ArgumentNullException(nameof(value));
    }
    [Required] 
    public bool IsSelling
    {
        get => is_selling;
        set => is_selling = value;
    }
    #endregion
}
