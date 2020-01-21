using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using AdminEmpleados.BLL;
using AdminEmpleados.DAL;

namespace AdminEmpleados.PL
{
    public partial class frmEmpleados : Form
    {
        EmpleadoDAL oEmpleadosDAL;
        public frmEmpleados()
        {
            InitializeComponent();
            dgvEmpleados.ReadOnly = true;
            oEmpleadosDAL = new EmpleadoDAL();
        }
        private void actualizarBotones(bool btnModificar, bool btnEliminar)
        {
            this.btnBorrar.Enabled = btnEliminar;
            this.btnModificar.Enabled = btnModificar;
        }
        private EmpleadoBLL RecuperarInformacion()
        {
            EmpleadoBLL oEmpleadoBLL = new EmpleadoBLL();
            int ID = 0;
            int.TryParse(txtID.Text, out ID);
            oEmpleadoBLL.ID = ID;
            oEmpleadoBLL.Nombres = txtNombre.Text;
            oEmpleadoBLL.PrimerApellido = txtPrimerApellido.Text;
            oEmpleadoBLL.SegundoApellido = txtSegundoApellido.Text;
            oEmpleadoBLL.Correo = txtCorreo.Text;
            oEmpleadoBLL.Departamento = cbxDepartamento.Text;
            byte[] file = null;
            Stream myStream = openFileDialog1.OpenFile();
            using (MemoryStream ms = new MemoryStream()) {
                myStream.CopyTo(ms);
                file = ms.ToArray();
            }
            
            oEmpleadoBLL.Foto = file;
            return oEmpleadoBLL;
        }
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (txtID.Text.Length > 0 && txtNombre.Text.Length > 0 && txtPrimerApellido.Text.Length > 0 && txtSegundoApellido.Text.Length > 0 && txtCorreo.Text.Length > 0 && cbxDepartamento.Text.Length > 0 && picfoto.Image != null)
            {
                EmpleadoBLL oEmpleadoBLL = new EmpleadoBLL();
                EmpleadoDAL oEmpleadoDAL = new EmpleadoDAL();
                oEmpleadoBLL = RecuperarInformacion();
                dgvEmpleados.DataSource = null;
                if (oEmpleadoDAL.Agregar(oEmpleadoBLL))
                {
                    dgvEmpleados.DataSource = oEmpleadoDAL.CargarDataGridView();
                    if (dgvEmpleados.Rows.Count > 0)
                        actualizarBotones(true, true);
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

        private void btnExaminar_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "Archivos jpg (*.jpg)(*.jpeg)|*.jpg;*.jpeg|Archivos png (*.png)|*.png|Archivos gif (*.gif)|*.gif";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
               picfoto.ImageLocation = openFileDialog1.FileName;
            }
            else {
                MessageBox.Show("ERROR\nNo se seleccionó ninguna imagen", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!System.Char.IsDigit(e.KeyChar) && e.KeyChar != (char)(System.ConsoleKey.Backspace))
                e.Handled = true;
        }

        private void dgvEmpleados_SelectionChanged(object sender, EventArgs e)
        {
            txtID.Text = dgvEmpleados.CurrentRow.Cells[0].Value.ToString();
            txtNombre.Text = dgvEmpleados.CurrentRow.Cells[1].Value.ToString();
            txtPrimerApellido.Text = dgvEmpleados.CurrentRow.Cells[2].Value.ToString();
            txtSegundoApellido.Text = dgvEmpleados.CurrentRow.Cells[3].Value.ToString();
            txtCorreo.Text = dgvEmpleados.CurrentRow.Cells[4].Value.ToString();
            picfoto.ImageLocation = ((byte[])(dgvEmpleados.CurrentRow.Cells[5].Value)).ToString();//cargar imagen
            cbxDepartamento.Text = dgvEmpleados.CurrentRow.Cells[6].Value.ToString();
        }

        private void frmEmpleados_Load(object sender, EventArgs e)
        {
            EmpleadoDAL oEmpleadoDAL = new EmpleadoDAL();
            dgvEmpleados.AutoGenerateColumns = false;
            dgvEmpleados.DataSource = oEmpleadoDAL.CargarDataGridView();
            
            foreach (DataGridViewColumn columna in dgvEmpleados.Columns)
            {
                columna.Width = 88;
            }
            if (dgvEmpleados.Rows.Count > 0)
                actualizarBotones(true, true);
        }
    }
}
