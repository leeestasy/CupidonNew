using System;
using System.Collections.Generic;

namespace Cupidon.Models;

public partial class Client
{
    public int RegistrNum { get; set; }

    public string Surname { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Patronymic { get; set; }

    public string? Gender { get; set; }

    public DateOnly? Birthday { get; set; }

    public int? Height { get; set; }

    public int? Weight { get; set; }

    public string? Country { get; set; }

    public string? City { get; set; }

    public bool? Children { get; set; }

    public string? Description { get; set; }

    public DateOnly? RegistrDate { get; set; }

    public int? ZodiacSignId { get; set; }

    public int? EducationId { get; set; }

    public int? StatusId { get; set; }

    public bool? Flag { get; set; }

    public string Login { get; set; } = null!;

    public string Pwd { get; set; } = null!;

    public virtual Education? Education { get; set; }

    public virtual Status? Status { get; set; }

    public virtual ZodiacSign? ZodiacSign { get; set; }
}
