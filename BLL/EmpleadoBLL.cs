﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminEmpleados.BLL
{
    class EmpleadoBLL
    {
        public int ID { get; set; }
        public string NombreEmpleado { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public string Correo { get; set; }
        public string Departamento { get; set; }
        public byte[] fotoEmpleado { get; set; }

    }
}
