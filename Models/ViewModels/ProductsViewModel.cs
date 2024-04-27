namespace ASP_Ecommerce.Models.ViewModels;

public class ProductsViewModel
{
    public ProductModel[] Products { get; set; } = [];
    public CategoryModel[] Categories { get; set; } = [];
}