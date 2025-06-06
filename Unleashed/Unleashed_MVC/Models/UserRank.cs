using System;
using System.Collections.Generic;

namespace Unleashed_MVC.Models;

public partial class UserRank
{
    public string UserId { get; set; } = null!;

    public int RankId { get; set; }

    public decimal? MoneySpent { get; set; }

    public short RankStatus { get; set; }

    public DateOnly RankExpireDate { get; set; }

    public DateTimeOffset RankCreatedDate { get; set; }

    public DateTimeOffset RankUpdatedDate { get; set; }

    public virtual Rank Rank { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
