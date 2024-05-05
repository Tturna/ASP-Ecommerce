namespace ASP_Ecommerce.Models.ViewModels;

public class HomeViewModel
{
    public required UserModel[] FeaturedMaintainers { get; set; }
    public required ProductModel[] FeaturedProducts { get; set; }
    public required ProductModel[] FeaturedMaintainerProducts { get; set; }
}