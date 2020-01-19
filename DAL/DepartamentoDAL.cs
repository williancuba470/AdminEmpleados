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
    class DepartamentoDAL
    {
        conexionDAL conexion;

        public DepartamentoDAL() {
            conexion = new conexionDAL();
        }

        public bool Agregar(DepartamentoBLL oDepartamentosBLL) {
            return conexion.ejecutarComandoSinRetornoDatos("INSERT INTO Departamentos (id,departamento) VALUES('"+oDepartamentosBLL.ID+"','"+oDepartamentosBLL.Departamento+"')");
        }

        public bool Modificar(DepartamentoBLL oDepartamentosBLL, int id) {
            return conexion.ejecutarComandoSinRetornoDatos("UPDATE Departamentos SET id = '"+oDepartamentosBLL.ID+"', departamento = '"+ oDepartamentosBLL.Departamento+ "' WHERE(id = '"+id+"')");
        }

        public bool Borrar(int id) {
            return conexion.ejecutarComandoSinRetornoDatos("DELETE FROM Departamentos WHERE (id = '" + id + "')");
        }

        public List<DepartamentoBLL> CargarDataGridView()
        {
            List<DepartamentoBLL> ListaDepartamentos = new List<DepartamentoBLL>();
            SqlDataReader lector = conexion.ejecutarComandoConRetornoDatos("SELECT id,departamento FROM Departamentos");
            while (lector.Read())
            {
                DepartamentoBLL oDepartamentoBLL = new DepartamentoBLL();
                oDepartamentoBLL.ID = System.Convert.ToInt32(lector[0]);
                oDepartamentoBLL.Departamento = lector[1].ToString();
                ListaDepartamentos.Add(oDepartamentoBLL);
            }
            return ListaDepartamentos;
        }

    }
}
