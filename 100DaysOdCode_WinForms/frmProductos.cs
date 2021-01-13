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

namespace _100DaysOdCode_WinForms
{
    public partial class frmProductos : Form
    {
        string rutaImagen = Directory.GetCurrentDirectory();

        public frmProductos()
        {
            InitializeComponent();

            rutaImagen = rutaImagen.Replace("bin\\Debug","Productos");
            if (!Directory.Exists(rutaImagen)) Directory.CreateDirectory(rutaImagen);
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (sonValidos())
            {
                string nombreImagen = ofdSubirImagen.SafeFileName;
                string[] datos = new string[]
                {
                txtCodigo.Text.Trim(),
                txtNombre.Text.Trim(),
                txtCantidad.Text.Trim(),
                txtPrecio.Text.Trim(),
                nombreImagen == "NoDisponible" ? nombreImagen : rutaImagen + "\\" + nombreImagen
                };
                dgvRegistros.Rows.Add(datos);

                if (nombreImagen != "NoDisponible")
                    pbxImagen.Image.Save(rutaImagen + "\\" + nombreImagen);
                limpiarForm();
            }
            else
            {
                MessageBox.Show("Por favor, complete todos los campos.");
            }
        }
        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (sonValidos())
            {
                string nombreImagen = ofdSubirImagen.SafeFileName;

            dgvRegistros.Rows[id].Cells[0].Value = txtCodigo.Text.Trim();
            dgvRegistros.Rows[id].Cells[1].Value = txtNombre.Text.Trim();
            dgvRegistros.Rows[id].Cells[2].Value = txtCantidad.Text;
            dgvRegistros.Rows[id].Cells[3].Value = txtPrecio.Text;
            dgvRegistros.Rows[id].Cells[4].Value = nombreImagen == "NoDisponible" ? nombreImagen : rutaImagen + "\\" + nombreImagen;

            if (nombreImagen != "NoDisponible")
                pbxImagen.Image.Save(rutaImagen + "\\" + nombreImagen);
            limpiarForm();
            }
            else
            {
                MessageBox.Show("Por favor, complete todos los campos.");
            }
        }
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            dgvRegistros.Rows.RemoveAt(id);
            string imagen = ofdSubirImagen.FileName;
            limpiarForm();
            File.Delete(imagen);
        }
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (btnBuscar.Text == "Buscar")
            {
                for (int i = 0; i < dgvRegistros.Rows.Count; i++)
                {
                    if (dgvRegistros.Rows[i].Cells[0].Value.ToString().Contains(txtCodigo.Text.Trim()) &&
                        dgvRegistros.Rows[i].Cells[1].Value.ToString().Contains(txtNombre.Text.Trim()) &&
                        dgvRegistros.Rows[i].Cells[2].Value.ToString().Contains(txtCantidad.Text.Trim()) &&
                        dgvRegistros.Rows[i].Cells[3].Value.ToString().Contains(txtPrecio.Text.Trim()))
                    {
                        dgvRegistros.Rows[i].Visible = true;
                    }
                    else
                    {
                        dgvRegistros.Rows[i].Visible = false;
                    }
                }
            }
            else
            {
                limpiarForm();
                btnBuscar.Text = "Buscar";
            }
            
        }

        private void btnSubirImagen_Click(object sender, EventArgs e)
        {
            ofdSubirImagen.ShowDialog();
            if (ofdSubirImagen.FileName != "NoDisponible")
            {
                lblNoDisponible.Visible = false;
                pbxImagen.Image = Image.FromFile(ofdSubirImagen.FileName);
            }
            else
            {
                lblNoDisponible.Visible = true;
            }
        }
        int id = 0;

        private void dgvRegistros_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            id = e.RowIndex;

            if(id > -1)
            {
                txtCodigo.Text = dgvRegistros.Rows[id].Cells[0].Value.ToString();
                txtNombre.Text = dgvRegistros.Rows[id].Cells[1].Value.ToString();
                txtCantidad.Text = dgvRegistros.Rows[id].Cells[2].Value.ToString();
                txtPrecio.Text = dgvRegistros.Rows[id].Cells[3].Value.ToString();
                ofdSubirImagen.FileName = dgvRegistros.Rows[id].Cells[4].Value.ToString();

                if (ofdSubirImagen.FileName != "NoDisponible")
                {
                    lblNoDisponible.Visible = false;
                    pbxImagen.Image = Image.FromFile(ofdSubirImagen.FileName);
                }
                else
                {
                    lblNoDisponible.Visible = true;
                }

                btnModificar.Enabled = true;
                btnEliminar.Enabled = true;
            }
            btnBuscar.Text = "Limpiar";
            btnAgregar.Enabled = false;
        }

        public bool sonValidos()
        {
            return txtCodigo.Text.Trim() != "" &&
                txtNombre.Text.Trim() != "" &&
                txtCantidad.Text.Trim() != "" &&
                txtPrecio.Text.Trim() != "";
        }
        public void limpiarForm()
        {
            txtCodigo.Clear();
            txtNombre.Clear();
            txtCantidad.Clear();
            txtPrecio.Clear();
            lblNoDisponible.Visible = false;
            btnAgregar.Enabled = true;
            btnModificar.Enabled = false;
            btnEliminar.Enabled = false;
            ofdSubirImagen.FileName = "NoDisponible";
            if (pbxImagen.Image != null)
                pbxImagen.Image.Dispose();
            pbxImagen.Image = null;
            id = 0;
        }

    }
}
