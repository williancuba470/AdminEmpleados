﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;
using AdminEmpleados.BLL;

namespace AdminEmpleados.DAL
{
    class conexionDAL
    {
        private string CadenaConexion = "Data Source=DESKTOP-2689E6R; Initial Catalog=dbSistema; Integrated Security=True";
        SqlConnection conexion;

        public SqlConnection EstablecerConexion() {
            this.conexion = new SqlConnection(this.CadenaConexion);
            return this.conexion;
        }
        /*Método para INSERT, DELETE y UPDATE*/
        public bool ejecutarComandoSinRetornoDatos(string strComando)
        {
            try
            {
                
                SqlCommand comando = new SqlCommand();
                comando.CommandText = strComando;
                comando.Connection = this.EstablecerConexion();
                conexion.Open();
                comando.ExecuteNonQuery();
                conexion.Close();
                return true;
            }
            catch 
            {
                return false;
            }
        }

        /*SELECT para retorno de datos*/
        public SqlDataReader ejecutarComandoConRetornoDatos(string strComando) {
            SqlCommand comando = new SqlCommand();
            comando.CommandText = strComando;
            comando.Connection = this.EstablecerConexion();
            conexion.Open();
            SqlDataReader lector = comando.ExecuteReader();
            return lector;
        }
            
    }    
}
