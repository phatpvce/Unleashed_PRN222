﻿using System;
using System.Collections.Generic;

namespace Unleashed_MVC.Models;

public partial class Stock
{
    public int StockId { get; set; }

    public string? StockName { get; set; }

    public string? StockAddress { get; set; }

    public virtual ICollection<StockVariation> StockVariations { get; set; } = new List<StockVariation>();

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
