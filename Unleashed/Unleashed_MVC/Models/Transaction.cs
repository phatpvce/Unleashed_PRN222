﻿using System;
using System.Collections.Generic;

namespace Unleashed_MVC.Models;

public partial class Transaction
{
    public int TransactionId { get; set; }

    public int? StockId { get; set; }

    public int? VariationId { get; set; }

    public int? ProviderId { get; set; }

    public string? InchargeEmployeeId { get; set; }

    public int? TransactionTypeId { get; set; }

    public int? TransactionQuantity { get; set; }

    public DateOnly? TransactionDate { get; set; }

    public decimal? TransactionProductPrice { get; set; }

    public virtual User? InchargeEmployee { get; set; }

    public virtual Provider? Provider { get; set; }

    public virtual Stock? Stock { get; set; }

    public virtual TransactionType? TransactionType { get; set; }

    public virtual Variation? Variation { get; set; }
}
