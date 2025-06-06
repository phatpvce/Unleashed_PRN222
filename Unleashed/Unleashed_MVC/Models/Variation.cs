﻿using System;
using System.Collections.Generic;

namespace Unleashed_MVC.Models;

public partial class Variation
{
    public int VariationId { get; set; }

    public string? ProductId { get; set; }

    public int? SizeId { get; set; }

    public int? ColorId { get; set; }

    public string? VariationImage { get; set; }

    public decimal? VariationPrice { get; set; }

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual Color? Color { get; set; }

    public virtual Product? Product { get; set; }

    public virtual Size? Size { get; set; }

    public virtual ICollection<StockVariation> StockVariations { get; set; } = new List<StockVariation>();

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
