using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;
using AdminEmpleados.BLL;

namespace AdminEmpleados.DAL
{
    class EmpleadoDAL
    {
        
         conexionDAL conexion;

        public EmpleadoDAL() {
            conexion = new conexionDAL();
        }
        
        public bool Agregar(EmpleadoBLL oEmpleadoBLL) {
            return conexion.ejecutarComandoSinRetornoDatos("INSERT INTO Empleados (id, nombres, primerapellido, segundoapellido, correo, foto, departamento) VALUES ('"+oEmpleadoBLL.ID+"', '"+ oEmpleadoBLL.NombreEmpleado+ "', '"+ oEmpleadoBLL.PrimerApellido+ "', '"+ oEmpleadoBLL.SegundoApellido+ "', '"+ oEmpleadoBLL.Correo+ "', '"+ oEmpleadoBLL.fotoEmpleado+"','"+oEmpleadoBLL.Departamento+"')");
        }
        /*
        public bool Modificar(DepartamentoBLL oDepartamentosBLL, int id) {
            return conexion.ejecutarComandoSinRetornoDatos("UPDATE Departamentos SET id = '"+oDepartamentosBLL.ID+"', departamento = '"+ oDepartamentosBLL.Departamento+ "' WHERE(id = '"+id+"')");
        }

        public bool Borrar(int id) {
            return conexion.ejecutarComandoSinRetornoDatos("DELETE FROM Departamentos WHERE (id = '" + id + "')");
        }
        */
        public List<EmpleadoBLL> CargarDataGridView()
        {
            List<EmpleadoBLL> ListaEmpleados = new List<EmpleadoBLL>();
            SqlDataReader lector = conexion.ejecutarComandoConRetornoDatos("SELECT id,nombres, primerapellido,segundoapellido,correo,foto FROM Empleados");
            while (lector.Read())
            {
                EmpleadoBLL oEmpleadoBLL = new EmpleadoBLL();
                oEmpleadoBLL.ID = System.Convert.ToInt32(lector[0]);
                oEmpleadoBLL.NombreEmpleado = lector[1].ToString();
                oEmpleadoBLL.PrimerApellido = lector[2].ToString();
                oEmpleadoBLL.SegundoApellido = lector[3].ToString();
                oEmpleadoBLL.Correo = lector[4].ToString();
                oEmpleadoBLL.fotoEmpleado = (byte[])lector[5];
                oEmpleadoBLL.Departamento = lector[6].ToString();
                ListaEmpleados.Add(oEmpleadoBLL);
            }
            return ListaEmpleados;
        }         
         
    }
}
