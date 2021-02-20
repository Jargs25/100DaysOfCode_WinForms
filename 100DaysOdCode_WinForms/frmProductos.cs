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
using _100DaysOdCode_WinForms.WCFProductos;

namespace _100DaysOdCode_WinForms
{
    public partial class frmProductos : Form
    {
        WCFProductoClient svcProducto = new WCFProductoClient();

        public frmProductos()
        {
            InitializeComponent();

            dgvRegistros.AutoGenerateColumns = false;
            dgvRegistros.DataSource = svcProducto.BuscarProductos(GetProducto());
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (sonValidos())
            {
                string nombreImagen = ofdSubirImagen.SafeFileName;

                Producto oProducto = GetProducto(
                txtNombre.Text.Trim(),
                Convert.ToInt32(txtCantidad.Text.Trim()),
                Convert.ToDouble(txtPrecio.Text.Trim()));

                oProducto.rutaImagen = nombreImagen;
                oProducto.imagen = GetByteImage(pbxImagen.Image);

                Mensaje.Show(svcProducto.AgregarProducto(oProducto));

                limpiarForm();
            }
            else
            {
                Mensaje.Show("Por favor, complete todos los campos.",0,2);
            }
        }
        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (sonValidos())
            {
                int idProducto = Convert.ToInt32(dgvRegistros.Rows[id].Cells[0].Value);
                string nombreImagen = ofdSubirImagen.SafeFileName;

                Producto oProducto = GetProducto(
                txtNombre.Text.Trim(),
                Convert.ToInt32(txtCantidad.Text.Trim()),
                Convert.ToDouble(txtPrecio.Text.Trim()));

                oProducto.id = idProducto;
                oProducto.rutaImagen = nombreImagen;
                oProducto.imagen = GetByteImage(pbxImagen.Image);

                Mensaje.Show(svcProducto.ModificarProducto(oProducto));

                limpiarForm();
            }
            else
            {
                Mensaje.Show("Por favor, complete todos los campos.",0,2);
            }
        }
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            DialogResult resultado = Mensaje.Show("¿Desea eliminar el registro?",1,2);
            if (resultado == DialogResult.OK)
            {
                Producto oProducto = GetProducto();
                oProducto.id = Convert.ToInt32(dgvRegistros.Rows[id].Cells[0].Value);
                oProducto.rutaImagen = ofdSubirImagen.FileName;

                Mensaje.Show(svcProducto.EliminarProducto(oProducto));

                limpiarForm();
            }

        }
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (btnBuscar.Text == "Buscar")
            {
                string cantidad = txtCantidad.Text.Trim() != "" ? txtCantidad.Text.Trim() : "0";
                string precio = txtPrecio.Text.Trim() != "" ? txtPrecio.Text.Trim() : "0";

                Producto oProducto = GetProducto(
                     txtNombre.Text.Trim(),
                     Convert.ToInt32(cantidad),
                     Convert.ToDouble(precio));
                oProducto.codigo = txtCodigo.Text.Trim();

                dgvRegistros.DataSource = svcProducto.BuscarProductos(oProducto);
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

                if (ofdSubirImagen.FileName != "NoDisponible" && dgvRegistros.Rows[id].Cells[6].Value != null)
                {
                    lblNoDisponible.Visible = false;
                    MemoryStream ms = new MemoryStream((byte[])dgvRegistros.Rows[id].Cells[6].Value);
                    pbxImagen.Image = Image.FromStream(ms);
                }
                else
                {
                    if (pbxImagen.Image != null)
                        pbxImagen.Image.Dispose();
                    pbxImagen.Image = null;
                    lblNoDisponible.Visible = true;
                }

                txtCodigo.Enabled = false;
                btnModificar.Enabled = true;
                btnEliminar.Enabled = true;
                btnBuscar.Text = "Limpiar";
                btnAgregar.Enabled = false;
            }
        }

        public bool sonValidos()
        {
            return txtNombre.Text.Trim() != "" &&
                txtCantidad.Text.Trim() != "" &&
                txtPrecio.Text.Trim() != "";
        }
        public void limpiarForm()
        {
            dgvRegistros.DataSource = svcProducto.BuscarProductos(new Producto());
            txtCodigo.Clear();
            txtCodigo.Enabled = true;
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
            DialogResult resultado = Mensaje.Show("¿Desea salir del programa?", 1, 1);
            if (resultado == DialogResult.No || resultado == DialogResult.Cancel)
            {
                e.Cancel = true;
            }
        }

        private Producto GetProducto()
        {
            Producto oProducto = new Producto();
            oProducto.id = 0;
            oProducto.codigo = "";
            oProducto.nombre = "";
            oProducto.cantidad = 0;
            oProducto.precio = 0;
            oProducto.rutaImagen = "NoDisponible";

            return oProducto;
        }
        private Producto GetProducto(string nombre, int cantidad, double precio)
        {
            Producto oProducto = new Producto();
            oProducto.id = 0;
            oProducto.codigo = "";
            oProducto.nombre = nombre;
            oProducto.cantidad = cantidad;
            oProducto.precio = precio;
            oProducto.rutaImagen = "NoDisponible";

            return oProducto;
        }
        private byte[] GetByteImage(Image img)
        {
            byte[] imagen;

            ImageConverter convertidor = new ImageConverter();
            imagen = (byte[])convertidor.ConvertTo(img, typeof(byte[]));

            return imagen;
        }
    }
}
