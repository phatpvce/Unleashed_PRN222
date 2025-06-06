﻿using System;
using System.Collections.Generic;

namespace Unleashed_MVC.Models;

public partial class VariationSingle
{
    public int VariationSingleId { get; set; }

    public string? VariationSingleCode { get; set; }

    public bool? IsVariationSingleBought { get; set; }

    public virtual ICollection<OrderVariationSingle> OrderVariationSingles { get; set; } = new List<OrderVariationSingle>();
}
