using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace EmpleadosABC.ModelsRequests
{
    public class Employee
    {
        [AllowNull]
        public int? EmployeeId { get; set; }

        [Required, StringLength(50)]
        public string? Name { get; set; }

        [Required, StringLength(50)]
        public string? LastName1 { get; set; }

        [Required, StringLength(50)]
        public string? LastName2 { get; set; }

        [Required, MinLength(10), MaxLength(10)]
        public string? BirthDate { get; set; }

        [Required]
        public int? EstatusId { get; set; }
    }
}
