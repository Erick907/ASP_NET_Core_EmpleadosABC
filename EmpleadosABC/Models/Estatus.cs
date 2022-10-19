using System;
using System.Collections.Generic;

namespace EmpleadosABC.Models
{
    public partial class Estatus
    {
        public Estatus()
        {
            Empleados = new HashSet<Empleado>();
        }

        public int EstatusId { get; set; }
        public string Descripción { get; set; } = null!;

        public virtual ICollection<Empleado> Empleados { get; set; }
    }
}
