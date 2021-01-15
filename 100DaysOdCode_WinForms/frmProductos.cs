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
        model_productos mProducto = new model_productos();
        string rutaImagen = Directory.GetCurrentDirectory();

        public frmProductos()
        {
            InitializeComponent();

            rutaImagen = rutaImagen.Replace("bin\\Debug", "Productos");
            if (!Directory.Exists(rutaImagen)) Directory.CreateDirectory(rutaImagen);
            dgvRegistros.DataSource = mProducto.BuscarProductos(new producto("", "", 0, 0, ""));
            dgvRegistros.AutoGenerateColumns = false;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (sonValidos())
            {
                string nombreImagen = ofdSubirImagen.SafeFileName;
                producto oProducto = new producto(
                    txtCodigo.Text.Trim(),
                txtNombre.Text.Trim(),
                Convert.ToInt32(txtCantidad.Text.Trim()),
                Convert.ToDouble(txtPrecio.Text.Trim()),
                nombreImagen == "NoDisponible" ? nombreImagen : rutaImagen + "\\" + nombreImagen
                );

                if (mProducto.AgregarProducto(oProducto) < 1)
                    Mensaje.Show("No se pudo guardar el producto.");

                if (nombreImagen != "NoDisponible")
                    pbxImagen.Image.Save(rutaImagen + "\\" + nombreImagen);
                limpiarForm();
            }
            else
            {
                Mensaje.Show("Por favor, complete todos los campos.");
            }
        }
        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (sonValidos())
            {
                int idProducto = Convert.ToInt32(dgvRegistros.Rows[id].Cells[0].Value);
                string nombreImagen = ofdSubirImagen.SafeFileName;

                producto oProducto = new producto(
                         txtCodigo.Text.Trim(),
                     txtNombre.Text.Trim(),
                     Convert.ToInt32(txtCantidad.Text.Trim()),
                     Convert.ToDouble(txtPrecio.Text.Trim()),
                     nombreImagen == "NoDisponible" ? nombreImagen : rutaImagen + "\\" + nombreImagen
                     );
                oProducto.id = idProducto;
                if (mProducto.ModificarProducto(oProducto) < 1)
                    Mensaje.Show("No se pudo modificar el producto.");


                if (nombreImagen != "NoDisponible")
                    pbxImagen.Image.Save(rutaImagen + "\\" + nombreImagen);
                limpiarForm();
            }
            else
            {
                Mensaje.Show("Por favor, complete todos los campos.");
            }
        }
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            DialogResult resultado = Mensaje.Show("¿Desea eliminar el registro?",1);
            if (resultado == DialogResult.OK)
            {
                int idProducto = Convert.ToInt32(dgvRegistros.Rows[id].Cells[0].Value);
                if (mProducto.EliminarProducto(idProducto) < 1)
                    Mensaje.Show("No se pudo eliminar el producto.");


                string imagen = ofdSubirImagen.FileName;
                limpiarForm();
                File.Delete(imagen);
            }

        }
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (btnBuscar.Text == "Buscar")
            {
                string cantidad = txtCantidad.Text.Trim() != "" ? txtCantidad.Text.Trim() : "0";
                string precio = txtPrecio.Text.Trim() != "" ? txtPrecio.Text.Trim() : "0";

                producto oProducto = new producto(
                         txtCodigo.Text.Trim(),
                     txtNombre.Text.Trim(),
                     Convert.ToInt32(cantidad),
                     Convert.ToDouble(precio),
                     "");
                dgvRegistros.DataSource = mProducto.BuscarProductos(oProducto);
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
                txtCodigo.Text = dgvRegistros.Rows[id].Cells[1].Value.ToString();
                txtNombre.Text = dgvRegistros.Rows[id].Cells[2].Value.ToString();
                txtCantidad.Text = dgvRegistros.Rows[id].Cells[3].Value.ToString();
                txtPrecio.Text = dgvRegistros.Rows[id].Cells[4].Value.ToString();
                ofdSubirImagen.FileName = dgvRegistros.Rows[id].Cells[5].Value.ToString();

                if (ofdSubirImagen.FileName != "NoDisponible")
                {
                    lblNoDisponible.Visible = false;
                    pbxImagen.Image = Image.FromFile(ofdSubirImagen.FileName);
                }
                else
                {
                    if (pbxImagen.Image != null)
                        pbxImagen.Image.Dispose();
                    pbxImagen.Image = null;
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
            return txtNombre.Text.Trim() != "" &&
                txtCantidad.Text.Trim() != "" &&
                txtPrecio.Text.Trim() != "";
        }
        public void limpiarForm()
        {
            dgvRegistros.DataSource = mProducto.BuscarProductos(new producto("", "", 0, 0, ""));
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

        private void frmProductos_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult resultado = Mensaje.Show("¿Desea salir del programa?", 1);
            if (resultado == DialogResult.No || resultado == DialogResult.Cancel)
            {
                e.Cancel = true;
            }
        }
    }
}
