namespace ASP_Ecommerce.Models.DataTransferObjects;

public class AddCartItemDto
{
    public int? ProductId { get; set; }
    public string? LocalStorageItemDictJson { get; set; }
}