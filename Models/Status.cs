using System;
using System.Collections.Generic;

namespace Cupidon.Models;

public partial class Status
{
    public int StatusId { get; set; }

    public string? Title { get; set; }

    public virtual ICollection<Client> Clients { get; set; } = new List<Client>();
}
