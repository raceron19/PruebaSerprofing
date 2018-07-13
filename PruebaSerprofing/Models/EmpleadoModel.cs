using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaSerprofing.Models
{
    public class EmpleadoModel
    {
        public int idEmpleado { get; set; }
        public string nombre { get; set; }
        public int dui { get; set; }
        public string direccion { get; set; }
        public int telefono { get; set; }
        public DateTime fechaIngreso { get; set; }
        public int activo { get; set; }
        public int idOficina { get; set; }
    }
}
