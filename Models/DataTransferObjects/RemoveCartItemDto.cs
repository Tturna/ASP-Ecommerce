namespace ASP_Ecommerce.Models.DataTransferObjects;

public class RemoveCartItemDto
{
    public int? ProductId { get; set; }
    public int? Quantity { get; set; }
    public string? LocalStorageItemDictJson { get; set; }
}