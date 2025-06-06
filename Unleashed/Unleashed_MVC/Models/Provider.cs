﻿using System;
using System.Collections.Generic;

namespace Unleashed_MVC.Models;

public partial class Provider
{
    public int ProviderId { get; set; }

    public string? ProviderName { get; set; }

    public string? ProviderImageUrl { get; set; }

    public string? ProviderEmail { get; set; }

    public string? ProviderPhone { get; set; }

    public string? ProviderAddress { get; set; }

    public DateTimeOffset? ProviderCreatedAt { get; set; }

    public DateTimeOffset? ProviderUpdatedAt { get; set; }

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
