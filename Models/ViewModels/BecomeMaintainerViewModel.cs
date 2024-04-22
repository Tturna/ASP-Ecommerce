using System.ComponentModel.DataAnnotations;

namespace ASP_Ecommerce.Models.ViewModels;

public class BecomeMaintainerViewModel
{
    [Required, StringLength(50)]
    public string? MaintainerAddress { get; set; }
    
    [Required, StringLength(50)]
    public string? City { get; set; }
    
    [Required, StringLength(50)]
    public string? Country { get; set; }
    
    [Required, DataType(DataType.PostalCode)]
    public int? PostalCode { get; set; }
}