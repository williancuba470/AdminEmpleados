using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using AdminEmpleados.DAL; //Para utilizar la conexion
using AdminEmpleados.BLL; //Para poder utilizar EmpleadoBLL.cs y DepartamentoBLL.cs

namespace AdminEmpleados.PL
{
    public partial class frmDepartamentos : Form
    {
        DepartamentoDAL oDepartamentosDAL;
        public frmDepartamentos()
        {
            InitializeComponent();
            dgvDepartamentos.ReadOnly = true;
            oDepartamentosDAL = new DepartamentoDAL();
        }

        private void actualizarBotones(bool btnModificar, bool btnEliminar) {
            this.btnBorrar.Enabled = btnEliminar;
            this.btnModificar.Enabled = btnModificar;
        }
        private DepartamentoBLL RecuperarInformacion()
        {
            DepartamentoBLL oDepartamentoBLL = new DepartamentoBLL();
            int ID = 0;
            int.TryParse(txtID.Text, out ID);
            oDepartamentoBLL.ID = ID;
            oDepartamentoBLL.Departamento = txtDepartamento.Text;
            return oDepartamentoBLL;
        }
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (txtID.Text.Length > 0 && txtDepartamento.Text.Length > 0)
            {
                DepartamentoBLL oDepartamentoBLL = new DepartamentoBLL();
                DepartamentoDAL oDepartamentoDAL = new DepartamentoDAL();
                oDepartamentoBLL = RecuperarInformacion();
                dgvDepartamentos.DataSource = null;
                if (oDepartamentosDAL.Agregar(oDepartamentoBLL))
                {
                    if (dgvDepartamentos.Rows.Count == 1)
                        actualizarBotones(true, true);
                    dgvDepartamentos.DataSource = oDepartamentoDAL.CargarDataGridView();
                    MessageBox.Show("El registro se agregó correctamente", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show("ERROR\nEl registro no se puede agregar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("ERROR\nEl registro no se puede agregar,\nel campo 'ID' o 'Departamento' está vacío", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private DepartamentoBLL RecuperarInformacion1() {
            DepartamentoBLL oDepartamentoBLL = new DepartamentoBLL();
            oDepartamentoBLL.ID = System.Convert.ToInt32(dgvDepartamentos.CurrentRow.Cells[0].Value);
            oDepartamentoBLL.Departamento = dgvDepartamentos.CurrentRow.Cells[1].Value.ToString();
            return oDepartamentoBLL;
        }
        private void btnModificar_Click(object sender, EventArgs e)
        {
            DepartamentoBLL oDepartamentoBLL = new DepartamentoBLL(), oDepartamentoBLL1 = new DepartamentoBLL();
            DepartamentoDAL oDepartamentoDAL = new DepartamentoDAL();
            oDepartamentoBLL = RecuperarInformacion();
            oDepartamentoBLL1 = RecuperarInformacion1();
            if (txtID.Text.Length > 0 && txtDepartamento.Text.Length > 0 && dgvDepartamentos.SelectedRows.Count == 1 && (oDepartamentoBLL.ID != oDepartamentoBLL1.ID || oDepartamentoBLL.Departamento != oDepartamentoBLL1.Departamento))
            {
                dgvDepartamentos.DataSource = null;
                if (oDepartamentosDAL.Modificar(oDepartamentoBLL, oDepartamentoBLL1.ID))
                {
                    dgvDepartamentos.DataSource = oDepartamentoDAL.CargarDataGridView();
                    MessageBox.Show("El registro se modificó correctamente", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }                    
                else
                    MessageBox.Show("ERROR\nEl registro no se pudo modificar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("ERROR\nEl registro no se pudo modificar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }            
        }

        private void txtID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!System.Char.IsDigit(e.KeyChar) && e.KeyChar != (char)(System.ConsoleKey.Backspace))
                e.Handled = true;            
        }

        private void frmDepartamentos_Load(object sender, EventArgs e)
        {
            DepartamentoDAL oDepartamentoDAL = new DepartamentoDAL();
            dgvDepartamentos.AutoGenerateColumns = false;
            dgvDepartamentos.DataSource = oDepartamentoDAL.CargarDataGridView();
            foreach (DataGridViewColumn columna in dgvDepartamentos.Columns)
            {
                columna.Width = 221;
            }            
            if (dgvDepartamentos.Rows.Count > 1)
                actualizarBotones(true, true);
        }

        private void dgvDepartamentos_SelectionChanged(object sender, EventArgs e)
        {
            txtID.Text = dgvDepartamentos.CurrentRow.Cells[0].Value.ToString();
            txtDepartamento.Text = dgvDepartamentos.CurrentRow.Cells[1].Value.ToString();
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            DepartamentoDAL oDepartamentoDAL = new DepartamentoDAL();
            if (dgvDepartamentos.SelectedRows.Count > 0)
            {
                dgvDepartamentos.DataSource = null;
                for (int i = 0; i < dgvDepartamentos.SelectedRows.Count - 1; i++) {
                    
                    oDepartamentoDAL.Borrar(System.Convert.ToInt32(dgvDepartamentos.Rows[dgvDepartamentos.SelectedRows[i].Index].Cells[0].Value));
                } 
                dgvDepartamentos.DataSource = oDepartamentoDAL.CargarDataGridView();
                MessageBox.Show("Los registros fueron eliminados correctamente", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else {
                MessageBox.Show("ERROR\nNo hay registro seleccionado para ser eliminado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
