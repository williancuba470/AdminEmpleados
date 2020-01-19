using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using AdminEmpleados.PL;

namespace AdminEmpleados
{
    public partial class Form1 : Form
    {
        private frmDepartamentos ofrmDepartamentos;
        private frmEmpleados ofrmEmpleados;
        public Form1()
        {
            ofrmDepartamentos = new frmDepartamentos();
            ofrmEmpleados = new frmEmpleados();
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ofrmDepartamentos.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ofrmEmpleados.ShowDialog();
        }
    }
}
