﻿using System.ComponentModel.DataAnnotations;

namespace ASP_Ecommerce.Models;

public class ProductModel
{
    public int Id { get; set; }
    
    [Required, StringLength(100)]
    public string Name { get; set; }
    
    [Required, StringLength(200)]
    public string ShortDescription { get; set; }
    
    [StringLength(500)]
    public string? LongDescription { get; set; }
    
    [StringLength(500)]
    public string? TechDescription { get; set; }
    
    [StringLength(500)]
    public string? WarrantyDescription { get; set; }
    
    public decimal Price { get; set; }
    
    [Required, StringLength(200)]
    public required string ImageUrl { get; set; }
    
    [Required, StringLength(100)]
    public required string CategoryPath { get; set; }
    
    public int? MaintainerId { get; set; }
    public UserModel? Maintainer { get; set; }

    public List<OrderItemModel> OrderItems { get; set; } = [];
    
    public List<ProductReviewModel> ProductReviews { get; set; } = [];
}