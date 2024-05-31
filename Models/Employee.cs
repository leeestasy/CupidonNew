using System;
using System.Collections.Generic;

namespace Cupidon.Models;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public string Surname { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Patronymic { get; set; }

    public string Login { get; set; } = null!;

    public string Pwd { get; set; } = null!;
}
