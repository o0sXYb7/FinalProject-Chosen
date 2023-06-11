using System;
using System.Collections.Generic;

namespace FinalProject.Models
{
    public partial class Employee
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int Permission { get; set; }
    }
}
