namespace ASP_Ecommerce.Models.ViewModels;

public class IndividualMaintainerViewModel
{
    public required UserModel Maintainer { get; set; }
    public required ProductsViewModel ProductsViewModel { get; set; }
}