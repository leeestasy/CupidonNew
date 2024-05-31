using System;
using System.Collections.Generic;

namespace Cupidon.Models;

public partial class ZodiacSign
{
    public int ZodiacSignId { get; set; }

    public string? Title { get; set; }

    public virtual ICollection<Client> Clients { get; set; } = new List<Client>();
}
