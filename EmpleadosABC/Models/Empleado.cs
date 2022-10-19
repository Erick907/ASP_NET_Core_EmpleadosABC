using System;
using System.Collections.Generic;

namespace EmpleadosABC.Models
{
    public partial class Empleado
    {
        public int EmpleadoId { get; set; }
        public string Nombre { get; set; } = null!;
        public string ApellidoPaterno { get; set; } = null!;
        public string ApellidoMaterno { get; set; } = null!;
        public DateTime FechaNacimiento { get; set; }
        public int EstatusId { get; set; }

        public virtual Estatus Estatus { get; set; } = null!;
    }
}
