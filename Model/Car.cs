using System.ComponentModel.DataAnnotations.Schema;

namespace Projet_Finale.Model; 
using System.ComponentModel.DataAnnotations;
public class Car
{
    [Key]
    public Guid Id_car {get;set;} = new Guid();

    [Required] 
    private string brand {get; set;}

    [Required] 
    private string model {get; set;}

    [Required] 
    private int years {get; set;}

    [Required] 
    private float pre_tax_prices {get; set;}

    [Required] 
    private float price_including_tax {get; set;}

    [Required] 
    private string color {get; set;}

    [Required] 
    private bool is_selling {get; set;}
    
    
    [ForeignKey("fk_client_id")]
    
    public Guid id_client {get; set;}
    
    public Client Client{get; set;}

    #region Accesors
    
    public string Brand
    {
        get => brand;
        set => brand = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string Model
    {
        get => model;
        set => model = value ?? throw new ArgumentNullException(nameof(value));
    }

    public int Years
    {
        get => years;
        set => years = value;
    }

    public float PreTaxPrices
    {
        get => pre_tax_prices;
        set => pre_tax_prices = value;
    }

    public float PriceIncludingTax
    {
        get => price_including_tax;
        set => price_including_tax = value;
    }

    public string Color
    {
        get => color;
        set => color = value ?? throw new ArgumentNullException(nameof(value));
    }

    public bool IsSelling
    {
        get => is_selling;
        set => is_selling = value;
    }
    #endregion
}
