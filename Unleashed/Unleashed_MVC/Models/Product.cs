﻿using System;
using System.Collections.Generic;

namespace Unleashed_MVC.Models;

public partial class Product
{
    public string ProductId { get; set; } = null!;

    public int? BrandId { get; set; }

    public int? ProductStatusId { get; set; }

    public string? ProductName { get; set; }

    public string? ProductCode { get; set; }

    public string? ProductDescription { get; set; }

    public DateTimeOffset? ProductCreatedAt { get; set; }

    public DateTimeOffset? ProductUpdatedAt { get; set; }

    public virtual Brand? Brand { get; set; }

    public virtual ProductStatus? ProductStatus { get; set; }

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual ICollection<Variation> Variations { get; set; } = new List<Variation>();

    public virtual ICollection<Category> Categories { get; set; } = new List<Category>();

    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
